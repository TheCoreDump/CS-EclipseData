using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Spatial;

namespace Eclipsedata
{
    public class SunMoon3d
    {
        public SunMoon3d(Sphere sun, Sphere moon)
        {
            Sun = sun;
            Moon = moon;
        }

        public Sphere Sun { get; set; }

        public Sphere Moon { get; set; }


    }
}
