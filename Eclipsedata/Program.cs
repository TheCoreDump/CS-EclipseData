using MathNet.Spatial.Euclidean;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            List<SunAndMoonRE> Data = LoadData();

            JArray ObjArray = new JArray();

            foreach (SunAndMoonRE tmpData in Data)
                ObjArray.Add(tmpData.OutputResults());

            JObject Result = new JObject(new JProperty("Data Points", ObjArray));


            int i = 0;

            using (FileStream FS = new FileStream("C:\\Dev\\Projects\\Personal\\C#\\CS-EclipseData\\Data\\PovRayData.csv", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter SW = new StreamWriter(FS))
                {
                    foreach (SunAndMoonRE tmpData in Data)
                        PovRayDataWriter.WriteData(SW, i++, tmpData.EarthDataPoint, new Vector3D(0, 0, 0), tmpData.MoonDataPoint);
                }

            }


            using (FileStream FS = new FileStream("C:\\Dev\\Projects\\Personal\\C#\\CS-EclipseData\\Data\\Test Output.json", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter SW = new StreamWriter(FS))
                {
                    using (JsonTextWriter JW = new JsonTextWriter(SW))
                    {
                        Result.WriteTo(JW);
                    }
                }
            }
        }


        private static void OldTest()
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

            SunAndMoonOld test = new SunAndMoonOld(Sun, Moon);
            test.Calculate();

            Console.WriteLine(test);
        }


        public static List<SunAndMoonRE> LoadData()
        {

            // Get the earth data
            Dictionary<DateTime, DataPoint> earthPoints = new Dictionary<DateTime, DataPoint>();

            FileInfo earthSourceFile = new FileInfo("C:\\Dev\\Projects\\Personal\\C#\\CS-EclipseData\\Data\\Earth Data.csv");

            if (!earthSourceFile.Exists)
                throw new FileNotFoundException($"File not found", earthSourceFile.FullName);

            using (StreamReader SR = earthSourceFile.OpenText())
            {
                string lineData;

                while (null != (lineData = SR.ReadLine()))
                {
                    string tmpData = lineData.Trim();

                    if (!string.IsNullOrEmpty(tmpData))
                    {
                        DataPoint newPoint = new DataPoint(tmpData);
                        earthPoints.Add(newPoint.UTCDate, newPoint);
                    }
                }
            }


            // Get the moon data
            Dictionary<DateTime, DataPoint> moonPoints = new Dictionary<DateTime, DataPoint>();

            FileInfo moonSourceFile = new FileInfo("C:\\Dev\\Projects\\Personal\\C#\\CS-EclipseData\\Data\\Moon Data.csv");

            if (!moonSourceFile.Exists)
                throw new FileNotFoundException($"File not found", moonSourceFile.FullName);

            using (StreamReader SR = moonSourceFile.OpenText())
            {
                string lineData;

                while (null != (lineData = SR.ReadLine()))
                {
                    string tmpData = lineData.Trim();

                    if (!string.IsNullOrEmpty(tmpData))
                    {
                        DataPoint newPoint = new DataPoint(tmpData);
                        moonPoints.Add(newPoint.UTCDate, newPoint);
                    }
                }
            }


            List<SunAndMoonRE> Results = new List<SunAndMoonRE>();
            var sortedKeys = earthPoints.Keys.OrderBy((d) => d);

            foreach (DateTime tmpKey in sortedKeys)
            {
                DataPoint earthData = earthPoints[tmpKey];
                DataPoint moonData = moonPoints[tmpKey];

                Results.Add(new SunAndMoonRE(tmpKey, moonData, earthData));
            }

            return Results;
        }
    }
}
