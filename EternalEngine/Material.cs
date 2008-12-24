
using System;
namespace EternalEngine
{
    [Serializable]
    public class Material
    {
        private static Material matSteel = new Material(10, .1);
        public static Material Steel { get { return matSteel; } }

        public Material(double Density, double Elasticity)
        {
            this.Density = Density;
            this.Elasticity = Elasticity;
        }

        /// <summary>
        /// Weight of line per 1 unit.
        /// </summary>
        public double Density { get; set; }

        /// <summary>
        /// Percentage of speed to retain on bounce
        /// </summary>
        public double Elasticity { get; set; }
    }
}
