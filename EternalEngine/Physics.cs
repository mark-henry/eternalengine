using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Drawing;

namespace EternalEngine
{
    public class Physics
    {
        public Physics(List<Entity> entities)
        {
            m_entities = entities;
            Gravity = .9f;
            AirResistance = .95f;
            ElasticityCoefficient = .5f;
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
            List<Entity> relevant = m_entities.FindAll(e => !(e is SceneryEntity));
            for (int e = 0; e < m_entities.Count; e++)
            {
                for (int c = e + 1; c < m_entities.Count; c++)
                {
                    //Check ent e against ent c for collide
                    if (m_entities[e].PhysBox.IntersectsWith(m_entities[c].PhysBox))
                    {
                        Debug.Print("Physics: PhysBox collision between entity {0} and entity {1}", e, c);
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
                    e.Location = new PointF(e.Location.X + e.Vertices.Average<Vertex>(i => i.InertiaX),
                        e.Location.Y + e.Vertices.Average<Vertex>(i => i.InertiaY));
                }
            }
        }
    }
}
