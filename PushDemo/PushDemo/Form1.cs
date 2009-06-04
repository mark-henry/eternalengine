using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EternalEngine;
using System.Diagnostics;

namespace PushDemo
{
   public partial class Form1 : Form
   {
      private Engine eng;
      private Point down;
      private Point up;

      public Form1()
      {
         InitializeComponent();

         eng = new Engine(this.ClientSize);
         eng.Map.Entities.Add(new BrushEntity());
         eng.Map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
         eng.Map.Entities[0].Vertices.Add(new Vertex(0, 0));
         eng.Map.Entities[0].Vertices.Add(new Vertex(100, 0));
         eng.Map.Entities[0].Material = Material.Steel;

         eng.Camera.Location = eng.Map.Entities[0].CenterofMass + new SizeF(eng.Map.Entities[0].Location);
      }

      private void Form1_MouseDown(object sender, MouseEventArgs e)
      {
         down = e.Location;
      }

      private void Form1_Paint(object sender, PaintEventArgs e)
      {
         eng.Draw(e.Graphics);
         if (!down.IsEmpty && !up.IsEmpty)
         {
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2f), down, up);
            e.Graphics.DrawEllipse(new Pen(Brushes.Red, 2f), new RectangleF(eng.Camera.WorldtoScreen(eng.Physics.Intersection(
               eng.Camera.ScreenToWorld(down),
               eng.Camera.ScreenToWorld(up),
               eng.Map.Entities[0].Vertices[0].Location + new SizeF(eng.Map.Entities[0].Location),
               eng.Map.Entities[0].Vertices[1].Location + new SizeF(eng.Map.Entities[0].Location))), new SizeF(1f, 1f)));

            PropEntity p = new PropEntity();
            p.Vertices.Add(new Vertex(0, 0));
            p.Vertices.Add(new Vertex(1, 1));
            p.Lines.Add(new Line(0, 1, Color.Green, 5f));
            p.Location = eng.Camera.ScreenToWorld(down);
            p.Velocity = new SizeF(up) - new SizeF(down);
            p.Material = Material.Steel;
            //Debug.WriteLine("Before collide: " + p.Velocity);
            eng.Physics.Collide(p, eng.Map.Entities[0]);
            //Debug.WriteLine("After: " + p.Velocity);

            e.Graphics.DrawEllipse(new Pen(Brushes.Blue), new RectangleF(eng.Camera.WorldtoScreen(p.Ghost(0) + new SizeF(p.Location)), new SizeF(2f, 2f)));
         }

         //e.Graphics.DrawEllipse(new Pen(Brushes.OrangeRed),
         //   new RectangleF(eng.Camera.WorldtoScreen(eng.Map.Entities[0].CenterofMass + new SizeF(eng.Map.Entities[0].Location)), new SizeF(1,1)));
         //foreach (Vertex v in eng.Map.Entities[0].Vertices) //Ghosts
         //{
         //   e.Graphics.DrawEllipse(new Pen(Brushes.Blue, 2), eng.Camera.WorldtoScreen(eng.Map.Entities[0].Ghost(v)).X - .5f + eng.Map.Entities[0].Location.X,
         //       eng.Camera.WorldtoScreen(eng.Map.Entities[0].Ghost(v)).Y - .5f + eng.Map.Entities[0].Location.Y, 1, 1);
         //}
      }

      private void Form1_Resize(object sender, EventArgs e)
      {
         eng.Camera.ScreenSize = this.ClientSize;
      }

      private void Form1_MouseMove(object sender, MouseEventArgs e)
      {
         up = e.Location;
      }

      private void Form1_MouseUp(object sender, MouseEventArgs e)
      {
         up = e.Location;
         //eng.Map.Entities[0].Push(eng.Camera.ScreenToWorld(down), new SizeF((up.X - down.X) * 5, (up.Y - down.Y) * 5));

         PropEntity p = new PropEntity();
         p.Vertices.Add(new Vertex(0, 0));
         p.Vertices.Add(new Vertex(3, 0));
         p.Vertices.Add(new Vertex(0, 3));
         p.Vertices.Add(new Vertex(3, 3));
         p.Lines.Add(new Line(0, 1, Color.Green, 1f));
         p.Lines.Add(new Line(1, 2, Color.Green, 1f));
         p.Lines.Add(new Line(2, 3, Color.Green, 1f));
         p.Lines.Add(new Line(3, 0, Color.Green, 1f));
         p.Location = eng.Camera.ScreenToWorld(down);
         p.Velocity = new SizeF(up) - new SizeF(down);
         p.Material = Material.Steel;
         lock (eng.Map.Entities)
         {
            eng.Map.Entities.Add(p);
         }

         up = new Point();
         down = new Point();
      }

      private void timer1_Tick(object sender, EventArgs e)
      {
         //eng.Map.Entities[0].AngularVelocity = 0;
         try
         {
            foreach (Entity ee in eng.Map.Entities)
            {
               if (ee.Location.Y > 75)
               {
                  eng.Map.Entities.Remove(ee);
               }
            }
         }
         catch { }

         eng.OnTick();
         Invalidate();
      }
   }
}
