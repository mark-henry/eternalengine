
using System;
namespace EternalEngine
{
   [Serializable]
   public class Material
   {
      private static Material matSteel = new Material(.5f, .4f);
      public static Material Steel { get { return matSteel; } }

      /// <summary>
      /// Defines an Eternal Engine Material.
      /// </summary>
      /// <param name="Density">Density in specks per square pixel.</param>
      /// <param name="Elasticity">Elasticity as a percentage of speed retained on collision.</param>
      public Material(float Density, float Elasticity)
      {
         this.Density = Density;
         this.Elasticity = Elasticity;
      }

      /// <summary>
      /// Density in specks per square pixel.
      /// </summary>
      public float Density { get; set; }

      /// <summary>
      /// Percentage of speed to retain on bounce
      /// </summary>
      public float Elasticity { get; set; }
   }
}
