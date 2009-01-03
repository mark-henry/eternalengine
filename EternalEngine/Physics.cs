using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.Collections;

namespace EternalEngine
{
    public class Physics
    {
        public Physics(List<Entity> entities)
        {
            m_entities = entities;
            Gravity = .9f;
            AirResistance = .95f;
            ElasticityCoefficient = -10f;
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
                if (e is ActorEntity || e is PropEntity)
                {
                    foreach (Vertex v in e.Vertices)
                    {
                        v.InertiaY += Gravity;
                        v.InertiaX *= AirResistance;
                        v.InertiaY *= AirResistance;
                        //Debug.WriteLine(v.Inertia);
                    }
                }
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
                    if (ents[e].PhysBox.IntersectsWith(m_entities[c].PhysBox))
                    {
                        Debug.Print("Physics: PhysBox collision between entity {0} and entity {1}", e, c);
                        //Check Vertex v against each Line l in Entity c
                        PointF intersection;
                        foreach (Vertex v in ents[e].Vertices)
                        {
                            foreach (Line l in ents[c].Lines)
                            {
                                intersection = Intersection(v.Location + new SizeF(ents[e].Location),
                                    v.Location + new SizeF(ents[e].Location) + ents[e].InertialDisplacement(),
                                    ents[c].Vertices[l.Index1].Location + ents[c].InertialDisplacement(),
                                    ents[c].Vertices[l.Index2].Location + ents[c].InertialDisplacement());
                                if (intersection != PointF.Empty)
                                {
                                    Debug.WriteLine("Physics: projected collision @ " + intersection);
                                    v.Inertia = new PointF((v.Inertia.X * ElasticityCoefficient * ents[e].Material.Elasticity),
                                        v.Inertia.Y * ElasticityCoefficient * ents[e].Material.Elasticity + Gravity);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ApplyInertia()
        {
            foreach (Entity e in m_entities)
            {
                if (e is ActorEntity || e is PropEntity)
                {
                    e.Location = e.Location + e.InertialDisplacement();
                }
            }
        }

        public PointF Intersection(PointF a1, PointF a2, PointF b1, PointF b2)
        {
            // Code based on http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/
            float denom = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);
            float num1 = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
            float num2 = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
            if (denom == 0)
            {
                if (num1 == 0 && num2 == 0)
                {
                    Debug.Print("Physics: Intersection: coincident (num1, num2, denom == 0): {0} {1} {2} {3}", a1, a2, b1, b2);
                    return new PointF((a1.X + a2.X) / 2, (a1.Y + a2.Y) / 2);
                }
                Debug.Print("Physics: Intersection: none (denom == 0): {0} {1} {2} {3}", a1, a2, b1, b2);
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
                return retp;
            }
            else { Debug.Print("Physics: Intersection: off of segments: {0} {1} {2} {3}", a1, a2, b1, b2); return new PointF(); }
        }

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
