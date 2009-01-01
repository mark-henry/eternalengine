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
            Inertia = new PointF(0, 0);
        }

        public Vertex(float X, float Y)
        {
            this.Location = new PointF(X, Y);
            Inertia = new PointF(0, 0);
        }

        private PointF m_inertia;
        public PointF Inertia { get { return m_inertia; } set { m_inertia = value; } }

        public float InertiaX
        {
            get
            {
                return Inertia.X;
            }
            set
            {
                m_inertia.X = value;
            }
        }

        public float InertiaY
        {
            get
            {
                return Inertia.Y;
            }
            set
            {
                m_inertia.Y = value;
            }
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
