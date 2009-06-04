using System;
using System.Drawing;

namespace EternalEngine
{
   public class Camera
   {
      private PointF m_shakedisplace = new Point(0, 0);

      public Camera(Size screensize) : this(new PointF(0, 0), screensize) { }
      public Camera(float x, float y, Size screensize) : this(new PointF(x, y), screensize) { }
      public Camera(PointF Location, Size screensize)
      {
         this.Location = Location;
         this.ScreenSize = screensize;
      }

      public Size ScreenSize { get; set; }

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

      public PointF ScreenToWorld(PointF p)
      {
         PointF retp = new PointF(p.X, p.Y);
         retp.X = p.X + this.Location.X - (this.ScreenSize.Width / 2);
         retp.Y = p.Y + this.Location.Y - (this.ScreenSize.Height / 2);
         return retp;
      }
      public PointF WorldtoScreen(PointF p)
      {
         PointF retp = new PointF(p.X, p.Y);
         retp.X = p.X - this.Location.X + (this.ScreenSize.Width / 2);
         retp.Y = p.Y - this.Location.Y + (this.ScreenSize.Height / 2);
         return retp;
      }
   }
}
