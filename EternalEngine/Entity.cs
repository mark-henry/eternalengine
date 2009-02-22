using System;
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
            Location = new PointF(0, 0);
            AngularInertia = 0;
            Inertia = new SizeF(0, 0);
        }
        public Entity(PointF location)
        {
            Lines = new List<Line>();
            Vertices = new List<Vertex>();
            Location = location;
            AngularInertia = 0;
            Inertia = new SizeF(0, 0);
        }

        public virtual float Mass
        {
            get
            {
                float area = 0;
                for (int i = 0; i < Lines.Count; i++) { area += LineLength(i) * Lines[i].Width; }
                return area * Material.Density;
            }
        }

        public float VertexMass(int index)
        {
            float ret = 0;
            foreach (Line l in Lines)
            {
                if (l.Index1 == index || l.Index2 == index) { ret += LineLength(Lines.IndexOf(l)) * l.Width; }
            }
            return ret * Material.Density * .5f;
        }

        public float LineLength(int index)
        {
            return (float)(Math.Sqrt(Math.Pow((Vertices[Lines[index].Index1].Location.X - Vertices[Lines[index].Index2].Location.X), 2)
                + Math.Pow((Vertices[Lines[index].Index1].Location.Y - Vertices[Lines[index].Index2].Location.Y), 2)));
        }

        public void Rotate(double theta)
        {
            //theta *= Math.PI / 180;
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

        public PointF Ghost(Vertex v)
        {
            PointF cm = CenterofMass;
            double inittheta;
            double l;
            inittheta = Math.Atan2(v.Location.Y - cm.Y, v.Location.X - cm.X);
            l = Math.Sqrt(Math.Pow(v.Location.X - cm.X, 2) + Math.Pow(v.Location.Y - cm.Y, 2));
            return new PointF(cm.X + (float)(l * Math.Cos(AngularInertia + inittheta)) + Inertia.Width,
                cm.Y + (float)(l * Math.Sin(AngularInertia + inittheta)) + Inertia.Height);
        }
        public PointF Ghost(int index)
        {
            return Ghost(Vertices[index]);
        }

        public void ApplyInertia()
        {
            Rotate(AngularInertia);
            Location = Location + Inertia;
        }

        public void Push(PointF point, SizeF push)
        {
            PointF cm = CenterofMass;
            float theta = (float)(Math.Atan2(push.Height, push.Width) - Math.Atan2(point.Y - cm.Y, point.X - cm.X));
            float leverarm = Math.Abs((float)(Math.Sqrt(Math.Pow(point.X - cm.X, 2) + Math.Pow(point.Y - cm.Y, 2)) *  // r * sin(Θ)
                Math.Sin(theta)));
                
            //Inertia
            Inertia = new SizeF(Inertia.Width + (push.Width * (float)Math.Cos(theta)), Inertia.Height + (push.Height * (float)Math.Sin(theta)));

            //Angular Inertia
            //Thanks to http://hyperphysics.phy-astr.gsu.edu/Hbase/torq2.html
            AngularInertia += ((float)Math.Sqrt(Math.Pow(push.Width, 2) + Math.Pow(push.Height, 2)) * leverarm) / MomentofInertia; // F * lever arm length
        }

        /// <summary>
        /// Returns a RectangleF describing where the Entity will be next frame
        /// </summary>
        public RectangleF PhysBox
        {
            get
            {
                return new RectangleF(Location.X + Vertices.Min<Vertex>(v => Ghost(v).X),
                    Location.Y + Vertices.Min<Vertex>(v => Ghost(v).Y),
                    Vertices.Max<Vertex>(v => Ghost(v).X) - Vertices.Min<Vertex>(v => Ghost(v).X),
                    Vertices.Max<Vertex>(v => Ghost(v).Y) - Vertices.Min<Vertex>(v => Ghost(v).Y));
            }
        }

        public virtual SizeF Inertia { get; set; }

        public virtual float AngularInertia { get; set; }

        public float MomentofInertia
        {
            get
            {
                float ret = 0;
                for (int i = 0; i < Vertices.Count; i++)
                {
                    ret += VertexMass(i) * (float)(Math.Pow(Vertices[i].Location.X - CenterofMass.X, 2) + Math.Pow(Vertices[i].Location.Y - CenterofMass.Y, 2));
                }
                return ret;
            }
        }

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

        public override float AngularInertia { get { return 0f; } set { } }

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

        public void ClearAnimation()
        {
            m_animation = null;
        }
    }

    [Serializable]
    public class BrushEntity : Entity
    {
        public BrushEntity()
        {
        }

        public override SizeF Inertia { get { return new SizeF(0, 0); } set { } }
        public override float AngularInertia { get { return 0; } set { } }
        public override float Mass { get { return float.MaxValue; } }
    }

    [Serializable]
    public class SceneryEntity : Entity
    {
        public SceneryEntity()
        {
        }

        public override SizeF Inertia { get { return new SizeF(0, 0); } set { } }
        public override float AngularInertia { get { return 0; } set { } }
    }

    [Serializable]
    public class PropEntity : Entity
    {
        public PropEntity()
        {
        }
    }
}
