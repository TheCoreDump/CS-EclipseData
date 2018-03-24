using System;

namespace Eclipsedata
{
    public class Point3d
    {
        public Point3d() { }

        public Point3d(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }


        public override string ToString() => $"({X}, {Y}, {Z})";


        public decimal DistanceTo(Point3d otherPoint) => Distance(this, otherPoint);

        public static decimal Distance(Point3d p1, Point3d p2)
        {
            decimal x = p1.X - p2.X;
            decimal y = p1.Y - p2.Y;
            decimal z = p1.Z - p2.Z;

            return DMath.Sqrt((x * x) + (y * y) + (z * z));
        }

    }
}
