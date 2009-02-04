using System.Collections.Generic;
using System;

namespace EternalEngine
{
    [Serializable]
    public class Keyframe
    {
        public Keyframe(List<Vertex> vertices)
        {
            m_vertices = new List<Vertex>();
            foreach (Vertex v in vertices)
            {
                m_vertices.Add(new Vertex(v.Location));
            }
        }

        private List<Vertex> m_vertices;
        public List<Vertex> Vertices { get { return m_vertices; } }
    }
}
