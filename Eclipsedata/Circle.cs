using System;

namespace Eclipsedata
{
    public class Circle 
    {
        public Circle(Point center, decimal radius)
        {
            Center = center;
            Radius = radius;
        }

        public Point Center { get; set; }

        public decimal Radius { get; set; }

        public override string ToString() => $"C: {Center.ToString()}  R: {Radius}";
    }
}
