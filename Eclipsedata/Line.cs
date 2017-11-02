using System;

namespace Eclipsedata
{
    public class Line
    {
        public Line(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;

            Slope = (P1.Y - P2.Y) / (P1.X - P2.X);

            Intercept = P1.Y - (Slope * P1.X);


        }

        public Point P1 { get; set; }

        public Point P2 { get; set; }

        public double Slope { get; set; }

        public double Intercept { get; set; }

        //public double AValue { get; set; }

        //public double BValue { get; set; }

        //public double CValue { get; set; }

        public override string ToString()
        {
            //return string.Format("P1: {0} P2: {1}  Eq: {2}x {3}y {4} = 0  Eq2: y = {5}x + {6}", P1.ToString(), P2.ToString(), AValue, BValue, CValue, Slope, Intercept);
            return string.Format("P1: {0} P2: {1}  Eq: y = {2}x + {3}", P1.ToString(), P2.ToString(), Slope, Intercept);
        }

        public Point ComputeForY(double x)
        {
            return new Point(x, (Slope * x + Intercept));
        }

        public Point ComputeForX(double y)
        {
            return new Point((y - Intercept) / Slope, y);
        }
    }
}
