using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EternalEngine;

namespace AEternal
{
    [Serializable]
    public class AEternalEntity : ActorEntity
    {
        public AEternalEntity()
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
