using System;
using System.Drawing;

namespace EternalEngine
{
   public class Camera
   {
      private PointF m_shakedisplace = new Point(0, 0);

      public Camera() : this(new PointF(0, 0)) { }
      public Camera(float x, float y) : this(new PointF(x, y)) { }
      public Camera(PointF Location)
      {
         this.Location = Location;
      }

      private PointF m_location;
      public PointF Location
      {
         get
         {
            return new PointF(m_shakedisplace.X + m_location.X, m_shakedisplace.Y + m_location.Y);
         }
         set { m_location = value; }
      }

      public void ShakeCamera(int intensity)
      {
         Random r = new Random();
         m_shakedisplace = new PointF((float)(r.NextDouble() - .5) * (intensity / 5), (float)(r.NextDouble() - .5) * (intensity / 5));
      }
   }
}
