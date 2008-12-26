﻿using System.Drawing;
using System.Windows.Forms;
using EternalEngine;
using System.Diagnostics;

namespace EternalEngineDemo
{
    public partial class Form1 : Form
    {
        private Camera cam = new Camera();
        private Map map = new Map();
        private Physics phys = new Physics();
        int ticker = 0;

        public Form1()
        {
            InitializeComponent();

            map.Entities.Add(new PropEntity());
            map.Entities[0].Lines.Add(new Line(0, 1, Color.Green, 2f));
            map.Entities[0].Vertices.Add(new Vertex(50,-100));
            map.Entities[0].Vertices.Add(new Vertex(100, -50));

            map.Entities.Add(new BrushEntity());
            map.Entities[1].Lines.Add(new Line(0, 1, Color.Firebrick, 2f));
            map.Entities[1].Vertices.Add(new Vertex(0, 150));
            map.Entities[1].Vertices.Add(new Vertex(150, 150));
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
            phys.ApplyGravityandAirResistance(map.Entities);
            phys.ApplyInertia(map.Entities);
            Invalidate();
        }
    }
}