using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EternalEngine
{
   /// <summary>
   /// Represents a new instance of the Eternal Engine
   /// </summary>
   public class Engine
   {
      public Engine(Size clientsize) : this(new PointF(0, 0), clientsize) { }
      public Engine(PointF camerainitloc, Size clientsize)
      {
         Camera = new Camera(camerainitloc, clientsize);
         Map = new Map();
         Physics = new Physics(Map.Entities);
         GUI = new GUI(clientsize);
      }

      public Camera Camera { get; set; }

      public Map Map { get; set; }

      public Physics Physics { get; set; }

      public GUI GUI { get; set; }

      public void Draw(Graphics g)
      {
         g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
         if (this.GUI.IsPaused)
         { }
         else
         {
            foreach (Entity ent in this.Map.Entities)
            {
               foreach (Line l in ent.Lines)
               {
                  g.DrawLine(new Pen(l.Color, l.Width),
                      this.Camera.WorldtoScreen(new PointF(ent.Vertices[l.Index1].Location.X + ent.Location.X, ent.Vertices[l.Index1].Location.Y + ent.Location.Y)),
                      this.Camera.WorldtoScreen(new PointF(ent.Vertices[l.Index2].Location.X + ent.Location.X, ent.Vertices[l.Index2].Location.Y + ent.Location.Y)));
                  //Debug.WriteLine(this.Camera.WorldtoScreen(ent.Vertices[l.Index1].Location).ToString() + "\n" + this.Camera.WorldtoScreen(ent.Vertices[l.Index2].Location).ToString());
               }
            }
            //g.DrawEllipse(new Pen(Color.Indigo, 2), this.Camera.WorldtoScreen(this.Map.Entities[0].CenterofMass).X - .5f + this.Map.Entities[0].Location.X,
            //    this.Camera.WorldtoScreen(this.Map.Entities[0].CenterofMass).Y - .5f + this.Map.Entities[0].Location.Y, 1, 1);
            //g.DrawRectangle(new Pen(Brushes.Coral, 2f), r.X, r.Y, r.Width, r.Height);
            //g.DrawRectangle(new Pen(Brushes.Coral, 2f), r2.X, r2.Y, r2.Width, r2.Height);
            //g.DrawPath(new Pen(Brushes.Blue, 2f), this.Physics.DebugBuffer);
            //this.Physics.DebugBuffer.Reset();
         }
         this.GUI.Draw(g);
         //g.FillRegion(Brushes.Purple, this.GUI.GetInvalidatedRegion());

      }

      /// <summary>
      /// To be called by a 40-milliseconds tick event
      /// </summary>
      public void OnTick()
      {
         foreach (ActorEntity ae in this.Physics.Entities.FindAll(ee => ee is ActorEntity))
         {
            ae.Animation.GoTo(ae.Vertices, ae.Animation.CurrentFrame + 1);
         }
         this.Physics.ApplyGravityandAirResistance();
         this.Physics.CollisionDetection();
         this.Physics.ApplyVelocities();
      }
   }
}
