using System;

namespace Eclipsedata
{
    public class Circle 
    {
        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public Point Center { get; set; }

        public double Radius { get; set; }


        public override string ToString()
        {
            return string.Format("C: {0}  R: {1}", Center.ToString(), Radius);
        }
    }
}
