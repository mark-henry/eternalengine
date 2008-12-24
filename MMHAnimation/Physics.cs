﻿using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace EternalEngine
{
    public class Physics
    {
        public Physics()
        {
            Gravity = .25f;
            AirResistance = .9f;
            ElasticityCoefficient = .5f;
        }

        public float Gravity { get; set; }
        public float AirResistance { get; set; }
        public float ElasticityCoefficient { get; set; }
        public float Friction { get; set; }

        public void ApplyGravityandAirResistance(List<Entity> entities)
        {
            foreach (Entity e in entities)
            {
                if (e is ActorEntity || e is PropEntity)
                {
                    foreach (Vertex v in e.Vertices)
                    {
                        v.InertiaY += Gravity;
                        v.InertiaX *= AirResistance;
                        v.InertiaY *= AirResistance;
                    }
                }
            }
        }
 
        public void CollisionDetection(List<Entity> entities)
        {
            List<Entity> relevant = entities.FindAll(IsPhysicsEntity);
            for (int e = 0; e < entities.Count; e++)
            {
                for (int c = e + 1; c < entities.Count; c++)
                {
                    //Check ent e against ent c for collide
                    Debug.WriteLine(e.ToString() + c.ToString());
                }
            }
        }
        private bool IsPhysicsEntity(Entity ent) { return !(ent is SceneryEntity); }

        public void ApplyInertia(List<Entity> entities)
        {
            foreach (Entity e in entities)
            {
                if (e is ActorEntity || e is PropEntity)
                {
                    foreach (Vertex v in e.Vertices)
                    {
                        v.LocationX += v.InertiaX;
                        v.LocationY += v.InertiaY;
                    }
                }
            }
        }
    }
}
