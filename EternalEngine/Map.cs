using System.Collections.Generic;
using System.Drawing;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

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

      public void Load(string path)
      {
         try
         {
            StreamReader sr = new StreamReader(path);
            BinaryFormatter bf = new BinaryFormatter();
            this.Entities = (List<Entity>)bf.Deserialize(sr.BaseStream);
         }
         catch { Debug.WriteLine("Error loading file " + path); }
      }

      public List<Entity> Entities { get; set; }

      /// <summary>
      /// The point at which the player enters the map
      /// </summary>
      public Point StartPoint { get; private set; }
   }
}
