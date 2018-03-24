using System;
using MathNet.Spatial.Euclidean;

namespace Eclipsedata
{
    public class Sphere
    {
        public Sphere()
        {
        }

        public Sphere(Point3D center, decimal radius)
        {
            Center = center;
            Radius = radius;
        }

        public Point3D Center { get; set; }

        public decimal Radius { get; set; }
    }
}
