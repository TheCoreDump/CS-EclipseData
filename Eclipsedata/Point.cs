using System;
using MathNet.Numerics;

namespace Eclipsedata
{
    public class Point
    {
        public Point(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }


        public decimal DistanceTo(Point otherPoint)
        {
            return Point.Distance(this, otherPoint);
        }

        public static decimal Distance(Point p1, Point p2)
        {
            decimal x = p1.X - p2.X;
            decimal y = p1.Y - p2.Y;

            return DMath.Sqrt((x * x) + (y * y));
        }
    }
}
