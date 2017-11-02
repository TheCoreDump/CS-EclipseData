using System;

namespace Eclipsedata
{
    public class Point3d
    {
        public Point3d()
        {
        }

        public Point3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }


        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }


        public double DistanceTo(Point3d otherPoint)
        {
            return Point3d.Distance(this, otherPoint);
        }

        public static double Distance(Point3d p1, Point3d p2)
        {
            double x = p1.X - p2.X;
            double y = p1.Y - p2.Y;
            double z = p1.Z - p2.Z;

            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }

    }
}
