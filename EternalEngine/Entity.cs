using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

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
                Debug.WriteLine("Warning: Accessed property Entity.Mass; this prop needs rewriting");
                double length = 0;
                foreach (Line l in Lines)
                {
                    length += Math.Sqrt(Math.Pow((Vertices[l.Index1].Location.X - Vertices[l.Index2].Location.X), 2)
                        + Math.Pow((Vertices[l.Index1].Location.Y - Vertices[l.Index2].Location.Y), 2));
                }
                return length*Material.Density;
            }
        }

        public double GetVertexMass(int index)
        {
            double ret = -1;
            foreach (Line l in Lines)
            {

            }
            return ret;
        }

        public double GetLineLength(int index)
        {
            return Math.Sqrt(Math.Pow((Vertices[Lines[index].Index1].Location.X - Vertices[Lines[index].Index2].Location.X), 2)
                + Math.Pow((Vertices[Lines[index].Index1].Location.Y - Vertices[Lines[index].Index2].Location.Y), 2));
        }

        public PointF Location { get; set; }

        public Color FillColor { get; set; }

        public List<Line> Lines { get; set; }

        public List<Vertex> Vertices { get; set; }

        public Material Material { get; set; }

        public PointF CenterofMass { get { throw new NotImplementedException("Entity.CenterofMass"); } }
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
            get { return m_animation;} 
            set 
            {
                if (value.ModelName != this.ModelName)
                {
                    throw new Exception("Model and animation names mismatch");
                }
                m_animation = value;
            }
        }

        public void Rotate(int degrees)
        {
            foreach (Vertex v in Vertices)
            {
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
