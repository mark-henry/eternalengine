using System.Collections.Generic;
using System.Drawing;
using System;

namespace EternalEngine
{
   [Serializable]
   public class Map
   {
      public Map(Point startPoint)
      {
         StartPoint = startPoint;
         Entities = new List<Entity>();
      }
      public Map() : this(new Point(0, 0)) { }

      public List<Entity> Entities { get; set; }

      /// <summary>
      /// The point at which the player enters the map
      /// </summary>
      public Point StartPoint { get; private set; }
   }
}
