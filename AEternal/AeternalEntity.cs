using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EternalEngine;

namespace Aeternal
{
    [Serializable]
    public class AeternalEntity : ActorEntity
    {
        public AeternalEntity()
        {
        }

        private int m_selectedvertex = -1;
        public int SelectedVertex
        {
            get { return m_selectedvertex; }
            set { if (value > this.Vertices.Count) { throw new ArgumentOutOfRangeException(); } m_selectedvertex = value; }
        }
    }
}
