using System;

namespace Eclipsedata
{
    public class Program
    {
        public static DateTimeOffset PivotTime = new DateTimeOffset(2017, 08, 21, 16, 24, 40, new TimeSpan(0));

        public const decimal LATITUDE = 36.824698M;
        public const decimal LONGITUDE = -87.497039M;
        public const decimal ELEVATION = 0.160934M; //km (528 feet)
        public const decimal EARTH_RADIUS = 6370.657M; //km
        public const decimal MOON_RADIUS = 1737M; //km
        public const decimal SOLAR_RADIUS = 695700M; //km
        public const decimal SOLAR_DISTANCE_AU = 1.0115488724729087M; //au
        public const decimal SOLAR_DISTANCE = 151325557.43M; //km
        public const decimal SOLAR_ELEVATION_DEG = 58.12768839932917M;
        public const decimal RELATIVE_AIR_MASS = 1.1768441564628445M;

        // km per AU: 149,597,870.7

        public static void Main(string[] args)
        {
            /*
            Circle Sun = new Circle(new Point(0, 3000), 16);
            Circle Moon = new Circle(new Point(10, 2000), 16.5);

            Console.WriteLine("Sun: {0}", Sun);
            Console.WriteLine("Moon: {0}", Moon);

            SunAndMoon test = new SunAndMoon(Sun, Moon);
            test.Calculate();

            Console.WriteLine(test);
            */


            // 3168

            Circle Sun = new Circle(new Point(0, SOLAR_DISTANCE - (EARTH_RADIUS + SOLAR_RADIUS)), SOLAR_RADIUS);
            Circle Moon = new Circle(new Point(0, 231215 - (EARTH_RADIUS + MOON_RADIUS)), MOON_RADIUS);

            Console.WriteLine("Sun: {0}", Sun);
            Console.WriteLine("Moon: {0}", Moon);

            SunAndMoon test = new SunAndMoon(Sun, Moon);
            test.Calculate();

            Console.WriteLine(test);

        }
    }
}
