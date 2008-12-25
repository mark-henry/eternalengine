using System.Collections.Generic;
using System;

namespace EternalEngine
{
    [Serializable]
    public class Keyframe
    {
        public Keyframe(List<Vertex> vertices)
        {
            m_vertices = vertices;
        }

        private List<Vertex> m_vertices;
        public List<Vertex> Vertices { get { return m_vertices; } }
    }
}
