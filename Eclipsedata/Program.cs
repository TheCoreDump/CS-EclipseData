using System;

namespace Eclipsedata
{
    public class Program
    {
        public static DateTimeOffset PivotTime = new DateTimeOffset(2017, 08, 21, 16, 24, 40, new TimeSpan(0));

        public const double LATITUDE = 36.824698;
        public const double LONGITUDE = -87.497039;
        public const double ELEVATION = 0.160934; //km (528 feet)
        public const double EARTH_RADIUS = 6370.657; //km
        public const double MOON_RADIUS = 1737; //km
        public const double SOLAR_RADIUS = 695700; //km
        public const double SOLAR_DISTANCE_AU = 1.0115488724729087; //au
        public const double SOLAR_DISTANCE = 151325557.43; //km
        public const double SOLAR_ELEVATION_DEG = 58.12768839932917;
        public const double RELATIVE_AIR_MASS = 1.1768441564628445;

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
