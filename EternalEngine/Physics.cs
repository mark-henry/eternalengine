using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EternalEngine
{
   public class Physics
   {
      public Physics(List<Entity> entities)
      {
         m_entities = entities;
         Gravity = 1f;
         AirResistance = .99f;
         ElasticityCoefficient = 1.5f;
         //DebugBuffer = new GraphicsPath();
      }

      public float Gravity { get; set; }
      public float AirResistance { get; set; }
      public float ElasticityCoefficient { get; set; }
      public float Friction { get; set; }

      private List<Entity> m_entities;
      public List<Entity> Entities { get { return m_entities; } }

      public void ApplyGravityandAirResistance()
      {
         foreach (Entity e in m_entities)
         {
            e.Velocity = new SizeF(e.Velocity.Width * AirResistance, (e.Velocity.Height + Gravity) * AirResistance);
            e.AngularVelocity *= AirResistance;
         }
      }

      public void ApplyVelocities()
      {
         foreach (Entity e in m_entities)
         {
            e.ApplyVelocities();
         }
      }

      public void CollisionDetection()
      {
         List<Entity> ents = m_entities.FindAll(e => !(e is SceneryEntity));
         for (int e = 0; e < ents.Count; e++)
         {
            for (int c = e + 1; c < ents.Count; c++)
            {
               //Check ent e against ent c for collide
               if (ents[e].PhysBox.IntersectsWith(ents[c].PhysBox))
               {
                  Debug.Print("Physics: PhysBox collision between entity {0} and entity {1}", e, c);
                  //Check Vertex v against each Line l in Entity c
                  SizeF translationE = new SizeF(ents[e].Location);
                  SizeF translationC = new SizeF(ents[c].Location);
                  float elasticity = ents[e].Material.Elasticity * ents[c].Material.Elasticity * ElasticityCoefficient;
                  //Debug.WriteLine("Physics: Elasticity: " + elasticity);
                  Collide(ents[e], ents[c], translationE, translationC);
                  Collide(ents[c], ents[e], translationC, translationE);
               }
            }
         }
      }

      private void Collide(Entity e, Entity c, SizeF translationE, SizeF translationC)
      {
         PointF intersection;
         //float elasticity = e.Material.Elasticity * c.Material.Elasticity * ElasticityCoefficient;
         foreach (Vertex v in e.Vertices)
         {
            foreach (Line l in c.Lines)
            {
               intersection = Intersection(v.Location + translationE, e.Ghost(v) + translationE,
                   c.Ghost(l.Index1) + translationC, c.Ghost(l.Index2) + translationC);
               if (!intersection.IsEmpty) //then v collides with l
               {
                  //tan is the tangent of the surface being bounced off of (on Entity e)
                  //inc is the incident vector of the incoming object (Ent c)
                  //We pretend Ent c is stationary

                  SizeF ev = new SizeF(e.Ghost(v).X - v.Location.X, e.Ghost(v).Y - v.Location.Y);
                  SizeF cv = new SizeF(c.Ghost(l.Index1).X - c.Vertices[l.Index1].Location.X, c.Ghost(l.Index1).Y - c.Vertices[l.Index1].Location.Y);

                  SizeF inc = ev - cv;

                  float thetatan = -(float)Math.Atan2(c.Vertices[l.Index1].Location.Y - c.Vertices[l.Index2].Location.Y,
                      c.Vertices[l.Index1].Location.X - c.Vertices[l.Index2].Location.X);

                  float tanmag = (float)(Math.Sqrt(Math.Pow(inc.Width, 2) + Math.Pow(inc.Height, 2)) *
                     Math.Cos(thetatan + Math.Atan2(inc.Height, inc.Width)));

                  SizeF tan = new SizeF((float)(tanmag * Math.Cos(-thetatan)),
                                        (float)(tanmag * Math.Sin(-thetatan)));

                  SizeF push = new SizeF(this.ElasticityCoefficient * (inc.Width - tan.Width) * e.Mass, //impulse to pass to Push()
                                         this.ElasticityCoefficient * (-inc.Height + tan.Height) * e.Mass);

                  e.Push(intersection, push);
                  //DebugBuffer.AddLine(intersection, intersection + new SizeF(push.Width * 10, push.Height * 10));
                  c.Push(intersection, new SizeF(-push.Height, -push.Width));
                  //DebugBuffer.AddLine(intersection, intersection - new SizeF(push.Width * 10, push.Height * 10));

                  //Debug.WriteLine("Physics: Push: " + new SizeF(inc.Width - tan.Width, tan.Height - inc.Height).ToString());
               }
            }
         }
      }

      /// <summary>
      /// Do two lines intersect
      /// </summary>
      /// <returns>Returns empty point if no intersection</returns>
      private PointF Intersection(PointF a1, PointF a2, PointF b1, PointF b2)
      {
         // Code based on http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/
         float denom = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);
         float num1 = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
         float num2 = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
         if (denom == 0)
         {
            if (num1 == 0 && num2 == 0)
            {
               //Debug.Print("Physics: Intersection: coincident (num1, num2, denom == 0): {0} {1} {2} {3}", a1, a2, b1, b2);
               return new PointF((a1.X + a2.X) / 2, (a1.Y + a2.Y) / 2);
            }
            //Debug.Print("Physics: Intersection: none (denom == 0): {0} {1} {2} {3}", a1, a2, b1, b2);
            return new PointF();
         }

         PointF retp = new PointF(a1.X + (num1 / denom) * (a2.X - a1.X), a1.Y + (num1 / denom) * (a2.Y - a1.Y));

         if (Math.Abs(((Math.Sqrt(Math.Pow(a1.X - retp.X, 2) + Math.Pow(a1.Y - retp.Y, 2)) +
             Math.Sqrt(Math.Pow(a2.X - retp.X, 2) + Math.Pow(a2.Y - retp.Y, 2)))) -
             Math.Sqrt(Math.Pow(a1.X - a2.X, 2) + Math.Pow(a1.Y - a2.Y, 2))) < .01 &&
             Math.Abs((Math.Sqrt(Math.Pow(b1.X - retp.X, 2) + Math.Pow(b1.Y - retp.Y, 2)) +
             Math.Sqrt(Math.Pow(b2.X - retp.X, 2) + Math.Pow(b2.Y - retp.Y, 2))) -
             Math.Sqrt(Math.Pow(b1.X - b2.X, 2) + Math.Pow(b1.Y - b2.Y, 2))) < .01)
         {
            //Debug.Print("Physics: projected collision @ {0} points: {1} {2} {3} {4}", retp, a1, a2, b1, b2);
            Debug.WriteLine("Physics: projected collision @ " + retp);
            return retp;
         }
         else
         {
            //Debug.Print("Physics: Intersection: off of segments: {0} {1} {2} {3}", a1, a2, b1, b2);
            return new PointF();
         }
      }

      //public GraphicsPath DebugBuffer { get; set; }

      //public PointF Intersection(PointF a1, PointF a2, PointF b1, PointF b2)
      //{
      //    // Find intersection of sets in y-axis -- kick out if no intersection
      //    if (a1.Y < a2.Y) { float inter = a1.Y; a1.Y = a2.Y; a2.Y = inter; }
      //    if (b1.Y < b2.Y) { float inter = b1.Y; b1.Y = b2.Y; b2.Y = inter; }


      //    // Calculate y-coords at beginning of range, then at end -- kick out if no crossover

      //    // Kick out if lines are coincident or parallel (check slope)

      //    // They're kosher -- use code above to return new PointF
      //    // Code based on http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/
      //    float denom = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);
      //    float num1 = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
      //    float num2 = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
      //    return new PointF(a1.X + (num1 / denom) * (a2.X - a1.X), a1.Y + (num1 / denom) * (a2.Y - a1.Y));
      //}
   }
}