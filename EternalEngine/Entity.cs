﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Linq;

namespace EternalEngine
{
    [Serializable]
    public abstract class Entity
    {
        public Entity()
        {
            Lines = new List<Line>();
            Vertices = new List<Vertex>();
            this.Location = new PointF(0, 0);
        }
        public Entity(PointF Location)
        {
            Lines = new List<Line>();
            Vertices = new List<Vertex>();
            this.Location = Location;
        }

        public double Mass
        {
            get
            {
                double length = 0;
                for (int i = 0; i < Lines.Count; i++) { length += LineLength(i); }
                return length * Material.Density;
            }
        }

        public double VertexMass(int index)
        {
            double ret = 0;
            foreach (Line l in Lines)
            {
                if (l.Index1 == index || l.Index2 == index) { ret += .5 * LineLength(Lines.IndexOf(l)) * Material.Density; }
            }
            return ret;
        }

        public double LineLength(int index)
        {
            return Math.Sqrt(Math.Pow((Vertices[Lines[index].Index1].Location.X - Vertices[Lines[index].Index2].Location.X), 2)
                + Math.Pow((Vertices[Lines[index].Index1].Location.Y - Vertices[Lines[index].Index2].Location.Y), 2));
        }

        public void Rotate(double theta)
        {
            theta *= -Math.PI / 180;
            PointF cm = CenterofMass;
            double inittheta;
            double l;
            foreach (Vertex v in Vertices)
            {
                inittheta = Math.Atan2(cm.Y - v.Location.Y, cm.X - v.Location.X);
                l = Math.Sqrt(Math.Pow(v.Location.X - cm.X, 2) + Math.Pow(v.Location.Y - cm.Y, 2));
                v.Location = new PointF(cm.X + (float)(l * Math.Cos(theta + inittheta)), cm.Y + (float)(l * Math.Sin(theta + inittheta)));
            }
        }

        /// <summary>
        /// Returns a RectangleF describing where the Entity will be next frame
        /// </summary>
        public RectangleF PhysBox
        {
            get
            {
                return new RectangleF(Location.X + Vertices.Min<Vertex>(v => v.Location.X) + Inertia.Width,
                    Location.Y + Vertices.Min<Vertex>(v => v.Location.Y) + Inertia.Height,
                    Vertices.Max<Vertex>(v => v.Location.X) - Vertices.Min<Vertex>(v => v.Location.X),
                    Vertices.Max<Vertex>(v => v.Location.Y) - Vertices.Min<Vertex>(v => v.Location.Y));
            }
        }

        public SizeF Inertia { get; set; }

        public float AngularInertia { get; set; }

        public float MomentofInertia { get; set; }

        public PointF Location { get; set; }

        public Color FillColor { get; set; }

        public List<Line> Lines { get; set; }

        public List<Vertex> Vertices { get; set; }

        public Material Material { get; set; }

        public PointF CenterofMass
        {
            get
            {
                double topx = 0;
                double topy = 0;
                double botx = 0;
                double boty = 0;

                for (int i = 0; i < Vertices.Count; i++)
                {
                    topx += VertexMass(i) * Vertices[i].Location.X;
                    botx += VertexMass(i);
                    topy += VertexMass(i) * Vertices[i].Location.Y;
                    boty += VertexMass(i);
                }

                if (botx == 0 || boty == 0) { return new PointF(0, 0); }
                return new PointF((float)(topx / botx), (float)(topy / boty));
            }
        }
    }

    [Serializable]
    public class ActorEntity : Entity
    {
        public ActorEntity(Animation anim)
        {
            Animation = anim;
        }

        public ActorEntity()
        {
        }

        public string ModelName { get; set; }

        private Animation m_animation;
        public Animation Animation
        {
            get { return m_animation; }
            set
            {
                if (value.ModelName != this.ModelName)
                {
                    throw new Exception("Model and animation names mismatch");
                }
                m_animation = value;
            }
        }
    }

    [Serializable]
    public class BrushEntity : Entity
    {
        public BrushEntity()
        {
        }
    }

    [Serializable]
    public class SceneryEntity : Entity
    {
        public SceneryEntity()
        {
        }
    }

    [Serializable]
    public class PropEntity : Entity
    {
        public PropEntity()
        {
        }
    }
}
