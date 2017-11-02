using System;

namespace Eclipsedata
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }


        public double DistanceTo(Point otherPoint)
        {
            return Point.Distance(this, otherPoint);
        }

        public static double Distance(Point p1, Point p2)
        {
            double x = p1.X - p2.X;
            double y = p1.Y - p2.Y;

            return Math.Sqrt((x * x) + (y * y));
        }
    }
}
