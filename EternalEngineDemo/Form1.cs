using System.Drawing;
using System.Windows.Forms;
using EternalEngine;
using System.Diagnostics;
using System.Drawing;

namespace EternalEngineDemo
{
    public partial class Form1 : Form
    {
        private Camera cam = new Camera();
        private Map map = new Map();
        private int ticker = 0;
        private Physics phys;

        public Form1()
        {
            InitializeComponent();

            map.Entities.Add(new PropEntity());
            map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
            map.Entities[0].Vertices.Add(new Vertex(50, -100));
            map.Entities[0].Vertices.Add(new Vertex(100, -50));
            map.Entities[0].Material = Material.Steel;

            map.Entities.Add(new BrushEntity());
            map.Entities[1].Lines.Add(new Line(0, 1, Color.Firebrick, 2f));
            map.Entities[1].Vertices.Add(new Vertex(0, 0));
            map.Entities[1].Vertices.Add(new Vertex(150, 0));
            map.Entities[1].Material = Material.Steel;

            phys = new Physics(map.Entities);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
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
            g.DrawString(ticker.ToString(), new Font(FontFamily.GenericMonospace, 10), Brushes.Black, new PointF(0, 0));
            //g.DrawEllipse(new Pen(Color.Indigo, 2), WorldtoScreen(map.Entities[1].CenterofMass).X - .5f, WorldtoScreen(map.Entities[1].CenterofMass).Y - .5f, 1, 1);
            RectangleF rf = new RectangleF(WorldtoScreen(map.Entities[0].PhysBox.Location), map.Entities[0].PhysBox.Size);
            g.DrawRectangle(new Pen(Brushes.Coral, 2f), rf.X, rf.Y, rf.Width, rf.Height);
        }

        public PointF ScreenToWorld(PointF p)
        {
            PointF retp = new PointF(p.X, p.Y);
            retp.X = p.X + cam.Location.X - (this.Width / 2);
            retp.Y = p.Y + cam.Location.Y - (this.Height / 2);
            return retp;
        }
        public PointF WorldtoScreen(PointF p)
        {
            PointF retp = new PointF(p.X, p.Y);
            retp.X = p.X - cam.Location.X + (this.Width / 2);
            retp.Y = p.Y - cam.Location.Y + (this.Height / 2);
            return retp;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            ticker++;
            //Invalidate(new Rectangle(0, 0, 40, 15));
            phys.CollisionDetection();
            phys.ApplyGravityandAirResistance();
            phys.ApplyInertia();
            //map.Entities[1].Rotate(1);
            Invalidate();
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
            }
        }
    }
}