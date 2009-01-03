
using System;
namespace EternalEngine
{
    [Serializable]
    public class Material
    {
        private static Material matSteel = new Material(2.75f, .3f);
        public static Material Steel { get { return matSteel; } }

        public Material(float Density, float Elasticity)
        {
            this.Density = Density;
            this.Elasticity = Elasticity;
        }

        /// <summary>
        /// Weight of line per 1 pixel of length.
        /// </summary>
        public float Density { get; set; }

        /// <summary>
        /// Percentage of speed to retain on bounce
        /// </summary>
        public float Elasticity { get; set; }
    }
}
