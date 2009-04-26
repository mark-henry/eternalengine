using System.Drawing;
using System.Windows.Forms;
using EternalEngine;
using System.Diagnostics;
using System;

namespace EternalEngineDemo
{
   public partial class Form1 : Form
   {
      private int ticker = 0;
      private Engine eng;

      public Form1()
      {
         InitializeComponent();

         eng = new Engine(new PointF(100, -150), this.ClientSize);
         eng.Camera.Location = new PointF(-100, -200);
         eng.GUI.UnPause += new EventHandler(this.gui_UnPause);

         InitializeEnts1();
      }

      private void InitializeEnts1()
      {
         eng.Map.Entities.Clear();

         eng.Map.Entities.Add(new PropEntity());

         //eng.Map.Entities[0].Vertices.Add(new Vertex(0, 0));
         //eng.Map.Entities[0].Vertices.Add(new Vertex(50, 0));
         //eng.Map.Entities[0].Vertices.Add(new Vertex(50, 50));
         //eng.Map.Entities[0].Vertices.Add(new Vertex(0, 50));
         //eng.Map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
         //eng.Map.Entities[0].Lines.Add(new Line(1, 2, Color.Green, 2f));
         //eng.Map.Entities[0].Lines.Add(new Line(2, 3, Color.Green, 2f));
         //eng.Map.Entities[0].Lines.Add(new Line(3, 0, Color.Green, 2f));
         //eng.Map.Entities[0].Location = new Point(0, -100);
         //eng.Map.Entities[0].Material = Material.Steel;
         //eng.Map.Entities[0].Velocity = new SizeF(0, -10);
         //eng.Map.Entities[0].AngularVelocity = .1f;

         eng.Map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
         eng.Map.Entities[0].Vertices.Add(new Vertex(0, -1));
         eng.Map.Entities[0].Vertices.Add(new Vertex(28, -44));
         eng.Map.Entities[0].Location = new PointF(50, 20);
         eng.Map.Entities[0].Material = Material.Steel;

         eng.Map.Entities.Add(new BrushEntity());
         eng.Map.Entities[1].Lines.Add(new Line(0, 1, Color.Firebrick, 2f));
         eng.Map.Entities[1].Vertices.Add(new Vertex(-100, 0));
         eng.Map.Entities[1].Vertices.Add(new Vertex(150, 0));
         eng.Map.Entities[1].Location = new PointF(0, 50);
         eng.Map.Entities[1].Material = Material.Steel;
      }
      private void InitializeEnts2()
      {
         eng.Map.Entities.Clear();

         eng.Map.Entities.Add(new BrushEntity());
         eng.Map.Entities[0].Lines.Add(new Line(0, 1, Color.Firebrick, 2f));
         eng.Map.Entities[0].Vertices.Add(new Vertex(0, 0));
         eng.Map.Entities[0].Vertices.Add(new Vertex(0, 500));
         eng.Map.Entities[0].Location = new PointF(100, -250);
         eng.Map.Entities[0].Material = Material.Steel;

         eng.Map.Entities.Add(new PropEntity());
         eng.Map.Entities[1].Vertices.Add(new Vertex(0, 0));
         eng.Map.Entities[1].Vertices.Add(new Vertex(50, 0));
         eng.Map.Entities[1].Vertices.Add(new Vertex(50, 50));
         eng.Map.Entities[1].Vertices.Add(new Vertex(0, 50));
         eng.Map.Entities[1].Lines.Add(new Line(0, 1, Color.Green, 2f));
         eng.Map.Entities[1].Lines.Add(new Line(1, 2, Color.Green, 2f));
         eng.Map.Entities[1].Lines.Add(new Line(2, 3, Color.Green, 2f));
         eng.Map.Entities[1].Lines.Add(new Line(3, 0, Color.Green, 2f));
         eng.Map.Entities[1].Location = new Point(-100, 0);
         eng.Map.Entities[1].Material = Material.Steel;
         eng.Map.Entities[1].Velocity = new SizeF(10, -10);
         eng.Map.Entities[1].AngularVelocity = .15f;
      }
      private void InitializeEnts3()
      {
         eng.Map.Entities.Clear();

         eng.Map.Entities.Add(new PropEntity()); //box
         eng.Map.Entities[0].Vertices.Add(new Vertex(0, 0));
         eng.Map.Entities[0].Vertices.Add(new Vertex(50, 0));
         eng.Map.Entities[0].Vertices.Add(new Vertex(50, 50));
         eng.Map.Entities[0].Vertices.Add(new Vertex(0, 50));
         eng.Map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
         eng.Map.Entities[0].Lines.Add(new Line(1, 2, Color.Green, 2f));
         eng.Map.Entities[0].Lines.Add(new Line(2, 3, Color.Green, 2f));
         eng.Map.Entities[0].Lines.Add(new Line(3, 0, Color.Green, 2f));
         eng.Map.Entities[0].Location = new Point(-50, 10);
         eng.Map.Entities[0].Material = Material.Steel;
         eng.Map.Entities[0].AngularVelocity = 0.05f;

         eng.Map.Entities.Add(new BrushEntity()); //floor
         eng.Map.Entities[1].Lines.Add(new Line(0, 1, Color.Firebrick, 3f));
         eng.Map.Entities[1].Vertices.Add(new Vertex(-100, 0));
         eng.Map.Entities[1].Vertices.Add(new Vertex(150, 0));
         eng.Map.Entities[1].Location = new PointF(0, 100);
         eng.Map.Entities[1].Material = Material.Steel;
         
         eng.Map.Entities.Add(new PropEntity()); //top box
         eng.Map.Entities[2].Vertices.Add(new Vertex(0, 0));
         eng.Map.Entities[2].Vertices.Add(new Vertex(50, 0));
         eng.Map.Entities[2].Vertices.Add(new Vertex(50, 50));
         eng.Map.Entities[2].Vertices.Add(new Vertex(0, 50));
         eng.Map.Entities[2].Lines.Add(new Line(0, 1, Color.Green, 2f));
         eng.Map.Entities[2].Lines.Add(new Line(1, 2, Color.Green, 2f));
         eng.Map.Entities[2].Lines.Add(new Line(2, 3, Color.Green, 2f));
         eng.Map.Entities[2].Lines.Add(new Line(3, 0, Color.Green, 2f));
         eng.Map.Entities[2].Location = new Point(-50, -60);
         eng.Map.Entities[2].Material = Material.Steel;
         eng.Map.Entities[2].AngularVelocity = -0.1f;

         eng.Map.Entities.Add(new BrushEntity()); //wall
         eng.Map.Entities[3].Vertices.Add(new Vertex(-100, 100));
         eng.Map.Entities[3].Vertices.Add(new Vertex(-100, -300));
         eng.Map.Entities[3].Lines.Add(new Line(0, 1, Color.Firebrick, 3f));
         eng.Map.Entities[3].Location = new PointF(0, 0);
         eng.Map.Entities[3].Material = Material.Steel;
      }

      private void Form1_Paint(object sender, PaintEventArgs e)
      {
         eng.Draw(e.Graphics);

         RectangleF r = new RectangleF(eng.Camera.WorldtoScreen(eng.Map.Entities[0].PhysBox.Location), eng.Map.Entities[0].PhysBox.Size);
         RectangleF r2 = new RectangleF(eng.Camera.WorldtoScreen(eng.Map.Entities[1].PhysBox.Location), eng.Map.Entities[1].PhysBox.Size);
         //foreach (Vertex v in eng.Map.Entities[0].Vertices) //Ghosts
         //{
         //   e.Graphics.DrawEllipse(new Pen(Brushes.Violet, 2), eng.Camera.WorldtoScreen(eng.Map.Entities[0].Ghost(v)).X - .5f + eng.Map.Entities[0].Location.X,
         //       eng.Camera.WorldtoScreen(eng.Map.Entities[0].Ghost(v)).Y - .5f + eng.Map.Entities[0].Location.Y, 1, 1);
         //}
         //foreach (Vertex v in eng.Map.Entities[1].Vertices) //Ghosts
         //{
         //   e.Graphics.DrawEllipse(new Pen(Brushes.Violet, 2), eng.Camera.WorldtoScreen(eng.Map.Entities[1].Ghost(v)).X - .5f + eng.Map.Entities[1].Location.X,
         //       eng.Camera.WorldtoScreen(eng.Map.Entities[1].Ghost(v)).Y - .5f + eng.Map.Entities[1].Location.Y, 1, 1);
         //}

         e.Graphics.DrawString(ticker.ToString(), new Font(FontFamily.GenericMonospace, 10), Brushes.Gray, this.Width - 50, this.Height - 50);
      }

      private void timer1_Tick(object sender, System.EventArgs e)
      {
         ticker++;
         Debug.WriteLine("Frame: " + ticker);
         eng.OnTick();

         //Invalidate(new Rectangle(0, 0, 40, 15));
         //eng.Physics.Entities[0].Push(eng.Physics.Entities[0].CenterofMass + new SizeF(5,-5), new SizeF(.01f, .01f));
         //eng.Map.Entities[0].Push(new PointF(0, 0), new SizeF(1, 0));

         Region r = new Region();
         r.Exclude(eng.GUI.GetInvalidatedRegion());
         Invalidate(r);
      }

      private void Form1_KeyDown(object sender, KeyEventArgs e)
      {
         switch (e.KeyCode)
         {
            case Keys.Space:
               timer1.Enabled = !timer1.Enabled;
               break;
            case Keys.D1:
               timer1.Interval = 40;
               break;
            case Keys.D2:
               timer1.Interval = 1000;
               break;
            case Keys.F1:
               InitializeEnts1();
               Invalidate();
               break;
            case Keys.F2:
               InitializeEnts2();
               Invalidate();
               break;
            case Keys.F3:
               InitializeEnts3();
               Invalidate();
               break;
            case Keys.S:
               timer1.Enabled = false;
               timer1_Tick(this, new EventArgs());
               break;
            case Keys.Q:
               Application.Exit();
               break;
            case Keys.Escape:
               timer1.Enabled = !eng.GUI.PauseToggle();
               Invalidate(eng.GUI.GetInvalidatedRegion());
               break;
         }
      }

      private void gui_UnPause(object sender, System.EventArgs e)
      {
         timer1.Enabled = true;
         eng.OnTick();
      }

      private void Form1_Resize(object sender, System.EventArgs e)
      {
         eng.GUI.ClientSize = this.ClientSize;
         Invalidate(eng.GUI.GetInvalidatedRegion());
      }

      private void Form1_MouseClick(object sender, MouseEventArgs e)
      {
         eng.GUI.OnClick(sender, e.Location, (int)e.Button);
         Invalidate(eng.GUI.GetInvalidatedRegion());
      }

      private void Form1_MouseMove(object sender, MouseEventArgs e)
      {
         eng.GUI.MouseMove(e.Location);
         Invalidate(eng.GUI.GetInvalidatedRegion());
      }
   }
}