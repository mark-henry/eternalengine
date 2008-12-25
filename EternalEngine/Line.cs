using System.Drawing;
using System;

namespace EternalEngine
{
    [Serializable]
    public class Line
    {
        public Line(int VertexIndex1, int VertexIndex2, Color Color, float Width)
        {
            Index1 = VertexIndex1;
            Index2 = VertexIndex2;
            this.Color = Color;
            this.Width = Width;
        }

        public int Index1 { get; set; }
        public int Index2 { get; set; }

        public Color Color { get; set; }

        public float Width { get; set; }
    }
}
