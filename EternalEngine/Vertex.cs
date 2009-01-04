using System.Drawing;
using System;

namespace EternalEngine
{
    [Serializable]
    public class Vertex
    {
        public Vertex(PointF Location)
        {
            this.Location = Location;
        }

        public Vertex(float X, float Y)
        {
            this.Location = new PointF(X, Y);
        }

        private PointF m_location;
        /// <summary>
        /// Object space!
        /// </summary>
        public PointF Location { get { return m_location; } set { m_location = value; } }

        public float LocationX
        {
            get
            {
                return Location.X;
            }
            set
            {
                m_location.X = value;
            }
        }

        public float LocationY
        {
            get
            {
                return Location.Y;
            }
            set
            {
                m_location.Y = value;
            }
        }
    }
}