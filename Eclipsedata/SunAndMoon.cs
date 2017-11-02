using System;
using System.Text;

namespace Eclipsedata
{
    public class SunAndMoon : IDisposable
    {
        private static DateTime _eclipticConjunction = new DateTime(2017, 08, 21, 18, 30, 11);
        private static DateTime _greatestEclipse = new DateTime(2017, 08, 21, 18, 25, 32);

        private static double _sunDegreesPerSecond = 360.0 / 86400.0;
        private static double _moonDegreesPerSecond = 360.0 / (86400.0 * 27.3);

        private Circle _sun;
        private Circle _moon;

        private double a;
        private double b;
        private double r0;

        private double c;
        private double d;
        private double r1;

        public SunAndMoon(Circle sun, Circle moon)
        {
            _sun = sun;
            _moon = moon;

            SameSize = sun.Radius == moon.Radius;

            LargerOrb = BiggerCircle(sun, moon);
            SmallerOrb = SmallerCircle(sun, moon);

            a = LargerOrb.Center.X;
            b = LargerOrb.Center.Y;
            r0 = LargerOrb.Radius;

            c = SmallerOrb.Center.X;
            d = SmallerOrb.Center.Y;
            r1 = SmallerOrb.Radius;

        }

        public bool SameSize { get; set; }


        public Circle LargerOrb { get; set; }

        public Circle SmallerOrb { get; set; }


        public Point OuterLineIntersection { get; set; }

        public Line WestUmbra { get; set; }

        public Line EastUmbra { get; set; }


        public Point InnerLineIntersection { get; set; }

        public Line EastPenumbra { get; set; }

        public Line WestPenumbra { get; set; }


        public Point WestUmbraIntercept { get; set; }

        public Point EastUmbraIntercept { get; set; }

        public Point EastPenumbraIntercept { get; set; }

        public Point WestPenumbraIntercept { get; set; }


        public double UmbralWidth { get; set; }

        public double PenumbralWidth { get; set; }


        public bool IsTotal { get; set; }


        protected void ComputeOuterLineIntersection()
        {
            if (SameSize)
                return;

            double x = ((c * r0) - (a * r1)) / (r0 - r1);
            double y = ((d * r0) - (b * r1)) / (r0 - r1);

            OuterLineIntersection = new Point(x, y);
        }

        protected void ComputeOuterLines()
        {
            Point i = OuterLineIntersection;

            if (i != null)
            {
                double xp = i.X;
                double yp = i.Y;

                double r0denominator = ((xp - a) * (xp - a)) + ((yp - b) * (yp - b));
                double r0squared = r0 * r0;


                double x1 = ( 
                                ( (r0squared * (xp - a)) + ( (r0 * (yp - b)) * Math.Sqrt(r0denominator + r0squared ) ) ) 
                                / 
                                ( r0denominator ) 
                                ) + a;
                double y1 = (
                                ((r0squared * (yp - b)) - ((r0 * (xp - a)) * Math.Sqrt(r0denominator + r0squared)))
                                /
                                (r0denominator)
                                ) + b;

                double x2 = (
                                ((r0squared * (xp - a)) - ((r0 * (yp - b)) * Math.Sqrt(r0denominator + r0squared)))
                                /
                                (r0denominator)
                                ) + a;
                double y2 = (
                                ((r0squared * (yp - b)) + ((r0 * (xp - a)) * Math.Sqrt(r0denominator + r0squared)))
                                /
                                (r0denominator)
                                ) + b;



                double r1denominator = ((xp - c) * (xp - c)) + ((yp - d) * (yp - d));
                double r1squared = r1 * r1;

                double x3 = (
                                ((r1squared * (xp - c)) + ((r1 * (yp - d)) * Math.Sqrt(r1denominator + r1squared)))
                                /
                                (r1denominator)
                                ) + c;
                double y3 = (
                                ((r1squared * (yp - d)) - ((r1 * (xp - c)) * Math.Sqrt(r1denominator + r1squared)))
                                /
                                (r1denominator)
                                ) + d;

                double x4 = (
                                ((r1squared * (xp - c)) - ((r1 * (yp - d)) * Math.Sqrt(r1denominator + r1squared)))
                                /
                                (r1denominator)
                                ) + c;
                double y4 = (
                                ((r1squared * (yp - d)) + ((r1 * (xp - c)) * Math.Sqrt(r1denominator + r1squared)))
                                /
                                (r1denominator)
                                ) + d;


                WestUmbra = new Line(new Point(x1, y1), new Point(x3, y3));
                EastUmbra = new Line(new Point(x2, y2), new Point(x4, y4));
            }
        }

        protected void ComputeInnerLineIntersection()
        {
            if (SameSize)
                return;

            double x = ((c * r0) + (a * r1)) / (r0 + r1);
            double y = ((d * r0) + (b * r1)) / (r0 + r1);

            InnerLineIntersection = new Point(x, y);
        }

        protected void ComputeInnerLines()
        {
            Point i = InnerLineIntersection;

            if (i != null)
            {
                double xp = i.X;
                double yp = i.Y;

                double r0denominator = ((xp - a) * (xp - a)) + ((yp - b) * (yp - b));
                double r0squared = r0 * r0;


                double x1 = (
                                ((r0squared * (xp - a)) + ((r0 * (yp - b)) * Math.Sqrt(r0denominator - r0squared)))
                                /
                                (r0denominator)
                                ) + a;
                double y1 = (
                                ((r0squared * (yp - b)) - ((r0 * (xp - a)) * Math.Sqrt(r0denominator - r0squared)))
                                /
                                (r0denominator)
                                ) + b;

                double x2 = (
                                ((r0squared * (xp - a)) - ((r0 * (yp - b)) * Math.Sqrt(r0denominator - r0squared)))
                                /
                                (r0denominator)
                                ) + a;
                double y2 = (
                                ((r0squared * (yp - b)) + ((r0 * (xp - a)) * Math.Sqrt(r0denominator - r0squared)))
                                /
                                (r0denominator)
                                ) + b;



                double r1denominator = ((xp - c) * (xp - c)) + ((yp - d) * (yp - d));
                double r1squared = r1 * r1;

                double x3 = (
                                ((r1squared * (xp - c)) + ((r1 * (yp - d)) * Math.Sqrt(r1denominator - r1squared)))
                                /
                                (r1denominator)
                                ) + c;
                double y3 = (
                                ((r1squared * (yp - d)) - ((r1 * (xp - c)) * Math.Sqrt(r1denominator - r1squared)))
                                /
                                (r1denominator)
                                ) + d;

                double x4 = (
                                ((r1squared * (xp - c)) - ((r1 * (yp - d)) * Math.Sqrt(r1denominator - r1squared)))
                                /
                                (r1denominator)
                                ) + c;
                double y4 = (
                                ((r1squared * (yp - d)) + ((r1 * (xp - c)) * Math.Sqrt(r1denominator - r1squared)))
                                /
                                (r1denominator)
                                ) + d;


                EastPenumbra = new Line(new Point(x1, y1), new Point(x3, y3));
                WestPenumbra = new Line(new Point(x2, y2), new Point(x4, y4));
            }
        }

        protected void ComputeIntercepts()
        {
            WestPenumbraIntercept = WestPenumbra.ComputeForX(0);
            EastPenumbraIntercept = EastPenumbra.ComputeForX(0);

            WestUmbraIntercept = WestUmbra.ComputeForX(0);
            EastUmbraIntercept = EastUmbra.ComputeForX(0);

            UmbralWidth = Point.Distance(WestUmbraIntercept, EastUmbraIntercept);
            PenumbralWidth = Point.Distance(WestPenumbraIntercept, EastPenumbraIntercept);

            IsTotal = WestUmbraIntercept.X < EastUmbraIntercept.X;
        }

        protected Circle BiggerCircle(Circle a, Circle b)
        {
            // Return the larger circle.  If the same, return current
            if (a.Radius >= b.Radius)
                return a;
            else
                return b;
        }

        protected Circle SmallerCircle(Circle a, Circle b)
        {
            // Return the smaller circle.  If the same, return other
            if (a.Radius >= b.Radius)
                return b;
            else
                return a;
        }

        public void Calculate()
        {
            ComputeOuterLineIntersection();
            ComputeOuterLines();

            ComputeInnerLineIntersection();
            ComputeInnerLines();

            ComputeIntercepts();
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();

            buffer.AppendLine("Two Circles: ");
            buffer.AppendFormat(" Larger: {0}", LargerOrb.ToString());
            buffer.AppendLine();
            buffer.AppendFormat(" Smaller: {0}", SmallerOrb.ToString());
            buffer.AppendLine();

            buffer.AppendFormat(" Outer Intersection: {0}", OuterLineIntersection);
            buffer.AppendLine();
            buffer.AppendFormat(" West Umbra Line: {0}", WestUmbra);
            buffer.AppendLine();
            buffer.AppendFormat(" East Umbra Line 2: {0}", EastUmbra);
            buffer.AppendLine();

            buffer.AppendFormat(" Inner Intersection: {0}", InnerLineIntersection);
            buffer.AppendLine();
            buffer.AppendFormat(" West Penumbra Line: {0}", WestPenumbra);
            buffer.AppendLine();
            buffer.AppendFormat(" East Penumbra Line: {0}", EastPenumbra);

            buffer.AppendLine();
            buffer.AppendFormat(" West Penumbra Intercept: {0}", WestPenumbraIntercept);
            buffer.AppendLine();
            buffer.AppendFormat(" East Penumbra Intercept: {0}", EastPenumbraIntercept);

            buffer.AppendLine();
            buffer.AppendFormat(" Penumbral Width: {0}", PenumbralWidth);

            buffer.AppendLine();
            buffer.AppendFormat(" West Umbra Intercept: {0}", WestUmbraIntercept);
            buffer.AppendLine();
            buffer.AppendFormat(" East Umbra Intercept: {0}", EastUmbraIntercept);

            buffer.AppendLine();
            buffer.AppendFormat(" Umbral Width: {0}", UmbralWidth);

            buffer.AppendLine();
            buffer.AppendFormat(" Type: {0}", IsTotal ? "Total" : "Annular");


            return buffer.ToString();
        }

        public void Dispose()
        {
            _sun = null;
            _moon = null;
        }

        public static SunAndMoon CreateSetup(double sunDiameter, double sunAltitude, double moonDiameter, double moonAltitude, DateTime time)
        {
            // Check to make sure the numbers make sense
            if ((time < _greatestEclipse.Subtract(new TimeSpan(5,0,0))) || (_greatestEclipse.Add(new TimeSpan(5, 0, 0)) < time))
                throw new ApplicationException(string.Format("Requested time {0} outside bounds", time.ToString()));

            if ((moonAltitude + (moonDiameter / 2)) >= (sunAltitude - (sunDiameter * 2)))
                throw new ApplicationException("Moon must be below the sun");

            if ((moonAltitude - (moonDiameter / 2)) <= 0)
                throw new ApplicationException("Moon must be above ground");


            // Determine the sun's x position
            double sunX = 0;

            // Determine the moon's x position
            double moonX = 0;


            // Make the sun
            Circle Sun = new Circle(new Point(sunX, sunAltitude), sunDiameter / 2);

            // Make the moon
            Circle Moon = new Circle(new Point(moonX, moonAltitude), moonDiameter / 2);


            return new SunAndMoon(Sun, Moon);
        }
    }
}
