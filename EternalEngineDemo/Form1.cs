using System.Drawing;
using System.Windows.Forms;
using EternalEngine;
using System.Diagnostics;
using System;

namespace EternalEngineDemo
{
   public partial class Form1 : Form
   {
      private Camera cam = new Camera(100, -150);
      private Map map = new Map();
      private int ticker = 0;
      private Physics phys;
      private GUI gui;

      public Form1()
      {
         InitializeComponent();

         InitializeEnts1();

         phys = new Physics(map.Entities);

         gui = new GUI(this.ClientSize);
         gui.UnPause += new EventHandler(this.GUI_UnPause);
      }

      private void InitializeEnts1()
      {
         map.Entities.Clear();

         map.Entities.Add(new PropEntity());

         //map.Entities[0].Vertices.Add(new Vertex(0, 0));
         //map.Entities[0].Vertices.Add(new Vertex(50, 0));
         //map.Entities[0].Vertices.Add(new Vertex(50, 50));
         //map.Entities[0].Vertices.Add(new Vertex(0, 50));
         //map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
         //map.Entities[0].Lines.Add(new Line(1, 2, Color.Green, 2f));
         //map.Entities[0].Lines.Add(new Line(2, 3, Color.Green, 2f));
         //map.Entities[0].Lines.Add(new Line(3, 0, Color.Green, 2f));
         //map.Entities[0].Location = new Point(0, -100);
         //map.Entities[0].Material = Material.Steel;
         //map.Entities[0].Velocity = new SizeF(0, -10);
         //map.Entities[0].AngularVelocity = .1f;

         map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
         map.Entities[0].Vertices.Add(new Vertex(0, -1));
         map.Entities[0].Vertices.Add(new Vertex(28, -44));
         map.Entities[0].Location = new PointF(50, 20);
         map.Entities[0].Material = Material.Steel;

         map.Entities.Add(new BrushEntity());
         map.Entities[1].Lines.Add(new Line(0, 1, Color.Firebrick, 2f));
         map.Entities[1].Vertices.Add(new Vertex(-100, 0));
         map.Entities[1].Vertices.Add(new Vertex(150, 0));
         map.Entities[1].Location = new PointF(0, 50);
         map.Entities[1].Material = Material.Steel;
      }
      private void InitializeEnts2()
      {
         map.Entities.Clear();

         map.Entities.Add(new BrushEntity());
         map.Entities[0].Lines.Add(new Line(0, 1, Color.Firebrick, 2f));
         map.Entities[0].Vertices.Add(new Vertex(0, 0));
         map.Entities[0].Vertices.Add(new Vertex(0, 500));
         map.Entities[0].Location = new PointF(100, -250);
         map.Entities[0].Material = Material.Steel;

         map.Entities.Add(new PropEntity());
         map.Entities[1].Vertices.Add(new Vertex(0, 0));
         map.Entities[1].Vertices.Add(new Vertex(50, 0));
         map.Entities[1].Vertices.Add(new Vertex(50, 50));
         map.Entities[1].Vertices.Add(new Vertex(0, 50));
         map.Entities[1].Lines.Add(new Line(0, 1, Color.Green, 2f));
         map.Entities[1].Lines.Add(new Line(1, 2, Color.Green, 2f));
         map.Entities[1].Lines.Add(new Line(2, 3, Color.Green, 2f));
         map.Entities[1].Lines.Add(new Line(3, 0, Color.Green, 2f));
         map.Entities[1].Location = new Point(-100, 0);
         map.Entities[1].Material = Material.Steel;
         map.Entities[1].Velocity = new SizeF(10, -10);
         map.Entities[1].AngularVelocity = .1f;
      }

      private void Form1_Paint(object sender, PaintEventArgs e)
      {
         Graphics g = e.Graphics;
         g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
         if (gui.IsPaused)
         { }
         else
         {
            foreach (Entity ent in map.Entities)
            {
               foreach (Line l in ent.Lines)
               {
                  g.DrawLine(new Pen(l.Color, l.Width),
                      WorldtoScreen(new PointF(ent.Vertices[l.Index1].Location.X + ent.Location.X, ent.Vertices[l.Index1].Location.Y + ent.Location.Y)),
                      WorldtoScreen(new PointF(ent.Vertices[l.Index2].Location.X + ent.Location.X, ent.Vertices[l.Index2].Location.Y + ent.Location.Y)));
                  //Debug.WriteLine(WorldtoScreen(ent.Vertices[l.Index1].Location).ToString() + "\n" + WorldtoScreen(ent.Vertices[l.Index2].Location).ToString());
               }
            }
            //g.DrawEllipse(new Pen(Color.Indigo, 2), WorldtoScreen(map.Entities[0].CenterofMass).X - .5f + map.Entities[0].Location.X,
            //    WorldtoScreen(map.Entities[0].CenterofMass).Y - .5f + map.Entities[0].Location.Y, 1, 1);
            RectangleF r = new RectangleF(WorldtoScreen(map.Entities[0].PhysBox.Location), map.Entities[0].PhysBox.Size);
            RectangleF r2 = new RectangleF(WorldtoScreen(map.Entities[1].PhysBox.Location), map.Entities[1].PhysBox.Size);
            g.DrawRectangle(new Pen(Brushes.Coral, 2f), r.X, r.Y, r.Width, r.Height);
            g.DrawRectangle(new Pen(Brushes.Coral, 2f), r2.X, r2.Y, r2.Width, r2.Height);
            //foreach (Vertex v in map.Entities[0].Vertices)
            //{
            //    g.DrawEllipse(new Pen(Brushes.Coral, 2), WorldtoScreen(map.Entities[0].Ghost(v)).X - .5f + map.Entities[0].Location.X,
            //        WorldtoScreen(map.Entities[0].Ghost(v)).Y - .5f + map.Entities[0].Location.Y, 1, 1);
            //}
            //g.DrawPath(new Pen(Brushes.Blue, 2f), phys.DebugBuffer);
            //phys.DebugBuffer.Reset();
         }
         gui.Draw(g);
         //g.FillRegion(Brushes.Purple, gui.GetInvalidatedRegion());
         g.DrawString(ticker.ToString(), new Font(FontFamily.GenericMonospace, 10), Brushes.Gray, this.Width - 50, this.Height - 50);
      }

      public PointF ScreenToWorld(PointF p)
      {
         PointF retp = new PointF(p.X, p.Y);
         retp.X = p.X + cam.Location.X - (this.ClientSize.Width / 2);
         retp.Y = p.Y + cam.Location.Y - (this.ClientSize.Height / 2);
         return retp;
      }
      public PointF WorldtoScreen(PointF p)
      {
         PointF retp = new PointF(p.X, p.Y);
         retp.X = p.X - cam.Location.X + (this.ClientSize.Width / 2);
         retp.Y = p.Y - cam.Location.Y + (this.ClientSize.Height / 2);
         return retp;
      }

      private void timer1_Tick(object sender, System.EventArgs e)
      {
         ticker++;
         Debug.WriteLine("Frame: " + ticker);
         //Invalidate(new Rectangle(0, 0, 40, 15));
         foreach (ActorEntity ae in phys.Entities.FindAll(ee => ee is ActorEntity))
         {
            ae.Animation.GoTo(ae.Vertices, ae.Animation.CurrentFrame + 1);
         }
         phys.ApplyGravityandAirResistance();
         phys.CollisionDetection();
         //phys.Entities[0].Push(phys.Entities[0].CenterofMass + new SizeF(5,-5), new SizeF(.01f, .01f));
         phys.ApplyVelocities();
         //map.Entities[0].Push(new PointF(0, 0), new SizeF(1, 0));
         Region r = new Region();
         r.Exclude(gui.GetInvalidatedRegion());
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
            case Keys.S:
               timer1.Enabled = false;
               timer1_Tick(this, new EventArgs());
               break;
            case Keys.Q:
               Application.Exit();
               break;
            case Keys.Escape:
               timer1.Enabled = !gui.PauseToggle();
               Invalidate(gui.GetInvalidatedRegion());
               break;
         }
      }

      private void GUI_UnPause(object sender, System.EventArgs e)
      {
         timer1.Enabled = true;
      }

      private void Form1_Resize(object sender, System.EventArgs e)
      {
         gui.ClientSize = this.ClientSize;
         Invalidate(gui.GetInvalidatedRegion());
      }

      private void Form1_MouseClick(object sender, MouseEventArgs e)
      {
         gui.OnClick(sender, e.Location, (int)e.Button);
         Invalidate(gui.GetInvalidatedRegion());
      }

      private void Form1_MouseMove(object sender, MouseEventArgs e)
      {
         gui.MouseMove(e.Location);
         Invalidate(gui.GetInvalidatedRegion());
      }
   }
}