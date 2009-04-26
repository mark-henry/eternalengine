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
         eng.Map.Entities.Add(new PropEntity());
         eng.Map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
         eng.Map.Entities[0].Vertices.Add(new Vertex(0, -1));
         eng.Map.Entities[0].Vertices.Add(new Vertex(28, -44));
         eng.Map.Entities[0].Location = new PointF(50, 20);
         eng.Map.Entities[0].Material = Material.Steel;

         eng.Physics.Gravity = 0;
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
         }
         e.Graphics.DrawEllipse(new Pen(Brushes.OrangeRed),
            new RectangleF(eng.Camera.WorldtoScreen(eng.Map.Entities[0].CenterofMass + new SizeF(eng.Map.Entities[0].Location)), new SizeF(1,1)));
         foreach (Vertex v in eng.Map.Entities[0].Vertices) //Ghosts
         {
            e.Graphics.DrawEllipse(new Pen(Brushes.Blue, 2), eng.Camera.WorldtoScreen(eng.Map.Entities[0].Ghost(v)).X - .5f + eng.Map.Entities[0].Location.X,
                eng.Camera.WorldtoScreen(eng.Map.Entities[0].Ghost(v)).Y - .5f + eng.Map.Entities[0].Location.Y, 1, 1);
         }
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
         eng.Map.Entities[0].Push(eng.Camera.ScreenToWorld(down), new SizeF((up.X - down.X) * 5, (up.Y - down.Y) * 5));
         up = new Point();
         down = new Point();
      }

      private void timer1_Tick(object sender, EventArgs e)
      {
         //eng.Map.Entities[0].AngularVelocity = 0;
         eng.OnTick();
         eng.Camera.Location = eng.Map.Entities[0].CenterofMass + new SizeF(eng.Map.Entities[0].Location);
         Invalidate();
      }
   }
}
