using System.Collections.Generic;
using System.Drawing;
using System;

namespace EternalEngine
{
    [Serializable]
    public class Map
    {
        public Map() { this.StartPoint = new Point(0, 0); Entities = new List<Entity>(); }
        public Map(Point StartPoint) { this.StartPoint = StartPoint; Entities = new List<Entity>(); }

        public List<Entity> Entities { get; set; }

        /// <summary>
        /// The point at which the player enters the map
        /// </summary>
        public Point StartPoint { get; set; }
    }
}
