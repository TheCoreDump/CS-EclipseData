using MathNet.Spatial.Euclidean;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using MathNet.Spatial.Units;
using Newtonsoft.Json.Linq;

namespace Eclipsedata
{
    public class SunAndMoonRE : SunAndMoonBase
    {
        public static double EARTH_RADIUS1 => 6371.01D;

        public static double MOON_RADIUS1 => 1737.4D;

        public static double SOLAR_RADIUS1 => 695700D;

        private const double SOLAR_DECLINATION = 11.9D; // Degrees
        private const double SOLAR_NOON_START_LONG = -45.0D; // Degrees

        private const double DEGREES_PER_HOUR = 15.0D; // Degrees / hour
        private const double DEGREES_PER_MINUTE = DEGREES_PER_HOUR / 60.0D; // degrees / min
        private const double DEGREES_PER_SECOND = DEGREES_PER_MINUTE / 60.0D; // degrees / sec

        private DateTime WindowStartTime = new DateTime(2017, 08, 21, 15, 00, 00);
        private DateTime WindowEndTime = new DateTime(2017, 08, 21, 22, 00, 00);

        private TimeSpan SolarNoonOffset = new TimeSpan(0, 3, 6);

        private Point3D _sunCenter;
        private Point3D _moonCenter;
        private Point3D _earthCenter;

        private Vector3D _sunToMoon;
        private Vector3D _sunToEarth;
        private Vector3D _earthToMoon;

        private Vector3D _sunToMoonRotated;
        private Vector3D _sunToEarthRotated;

        private Vector3D _umbra;

        private Point3D _umbraIntersection;
        private Point3D _umbraIntersectionRotated;
        private Point3D _penumbraIntersection;
        private Point3D _penumbraIntersectionRotated;

        private List<Ray3D> _umbraRays = new List<Ray3D>();
        private List<Ray3D> _umbraRaysRotated = new List<Ray3D>();
        private List<Ray3D> _penumbraRays = new List<Ray3D>();
        private List<Ray3D> _penumbraRaysRotated = new List<Ray3D>();

        private DateTime currentTimestamp;
        private double currentSolarLongitude;

        private double _umbraAngle;

        private Matrix<double> _rotationMatrix;

        private bool _centerIntersectsEarth;


        public DataPoint MoonDataPoint { get; set; }

        public DataPoint EarthDataPoint { get; set; }


        public SunAndMoonRE(DateTime systemTimestamp, DataPoint moonDataPoint, DataPoint earthDataPoint)
        {
            MoonDataPoint = moonDataPoint;
            EarthDataPoint = earthDataPoint;


            if ((systemTimestamp < WindowStartTime) ||
                (WindowEndTime < systemTimestamp))
                throw new ApplicationException($"System date is outside acceptable range: {systemTimestamp.ToString("yyyy-MM-dd HH:mm:ss")}");

            currentTimestamp = systemTimestamp;

            TimeSpan startOffset = systemTimestamp.Subtract(WindowStartTime);

            currentSolarLongitude = SOLAR_NOON_START_LONG - (startOffset.TotalSeconds * DEGREES_PER_SECOND);

            _sunToMoon = moonDataPoint.Center;
            _sunToEarth = earthDataPoint.Center;

            _sunCenter = new Point3D(0D, 0D, 0D);
            _moonCenter = new Point3D(_sunToMoon.X, _sunToMoon.Y, _sunToMoon.Z);
            _earthCenter = new Point3D(_sunToEarth.X, _sunToEarth.Y, _sunToEarth.Z);


            _earthToMoon = new Vector3D(_moonCenter.X - _earthCenter.X, _moonCenter.Y - _earthCenter.Y, _moonCenter.Z - _earthCenter.Z);


            _rotationMatrix = Matrix3D.RotationTo(_sunToMoon.Normalize(), new UnitVector3D(1D, 0D, 0D));
            _centerIntersectsEarth = RayIntersectsSphere(new Ray3D(_sunCenter, _sunToMoon.Normalize()), _earthCenter, EARTH_RADIUS1);

            _sunToMoonRotated = _sunToMoon.TransformBy(_rotationMatrix);
            _sunToEarthRotated = _sunToEarth.TransformBy(_rotationMatrix);

            _umbraAngle = Math.Asin((SOLAR_RADIUS1 - MOON_RADIUS1) / _sunToMoon.Length);

            _umbraIntersectionRotated = new Point3D
                (
                    ((_sunToMoonRotated.X * SOLAR_RADIUS1) - (0D * MOON_RADIUS1)) / (SOLAR_RADIUS1 - MOON_RADIUS1),
                    0D,
                    0D
                );

            _umbraIntersection = _umbraIntersectionRotated.TransformBy(_rotationMatrix.Inverse());


            _penumbraIntersectionRotated = new Point3D
                (
                    ((_sunToMoonRotated.X * SOLAR_RADIUS1) + (0D * MOON_RADIUS1)) / (SOLAR_RADIUS1 + MOON_RADIUS1),
                    0D,
                    0D
                );

            _penumbraIntersection = _penumbraIntersectionRotated.TransformBy(_rotationMatrix.Inverse());


            Point3D _umbraStartPoint = new Point3D(SOLAR_RADIUS1 * Math.Sin(_umbraAngle), 0D, SOLAR_RADIUS1 * Math.Cos(_umbraAngle));


            for (int i = 0; i < 4; i++)
            {
                double angle = (Convert.ToDouble(i) / 4D) * (2D * Math.PI);

                Matrix<double> tmpRotationMatrix = Matrix3D.RotationAroundXAxis(Angle.FromRadians(angle));
                Point3D _umbraStartPointRotated = _umbraStartPoint.TransformBy(tmpRotationMatrix);
                _umbraRaysRotated.Add(new Ray3D(_umbraStartPointRotated, _umbraIntersectionRotated - _umbraStartPointRotated));
            }


            foreach (Ray3D tmpRay in _umbraRaysRotated)
                _umbraRays.Add(new Ray3D(tmpRay.ThroughPoint.TransformBy(_rotationMatrix.Inverse()), tmpRay.Direction.TransformBy(_rotationMatrix.Inverse())));


        }


        public JObject OutputResults()
        {
            JObject Result = new JObject();

            Result.Add("Current Timestamp:", currentTimestamp.ToString("yyyy-MM-dd HH:mm:ss"));

            Result.Add("Sun Center", _sunCenter.ToJObject());
            Result.Add("Moon Center", _moonCenter.ToJObject());
            Result.Add("Earth Center", _earthCenter.ToJObject());

            Result.Add("Sun To Moon Vector", _sunToMoon.ToJObject());
            Result.Add("Sun To Earth Vector", _sunToEarth.ToJObject());
            Result.Add("Earth To Moon Vector", _earthToMoon.ToJObject());
            Result.Add("Umbra Vector", _umbra.ToJObject());


            JArray _raysArray = new JArray();
            JArray _rotatedRaysArray = new JArray();

            foreach (Ray3D tmpRay in _umbraRaysRotated)
                _rotatedRaysArray.Add(tmpRay.ToJObject());

            foreach (Ray3D tmpRay in _umbraRays)
                _raysArray.Add(tmpRay.ToJObject());

            Result.Add("Rays", _raysArray);
            Result.Add("RotatedRays", _rotatedRaysArray);

            return Result;
        }

        public JObject Process(Action<string> debugOutput)
        {
            Plane tmpPlane = new Plane(_sunCenter, _sunToMoon.Normalize());
            
            double _moonToUmbraVertex = MOON_RADIUS1 / Math.Sin(_umbraAngle);
            double _sunToUmbraVertex = _sunToMoon.Length + _moonToUmbraVertex;
            _umbra = _sunToMoon.Normalize().ScaleBy(_sunToUmbraVertex);

            // Solar declination: 11.91 degrees at 15:00:00
            //                    11.89 degrees at 16:00:00
            // Solar noon at 0 degrees: 12:03:06pm
            // 15 degrees per hour (15d 0' 0")
            // 0.25 degrees per minute (0d 15' 0")
            // 0.0041667 degrees per second (0d 0' 15")


            debugOutput($"**** Start of Data ****");
            debugOutput($"Current Timestamp: {currentTimestamp}");
            debugOutput($"Current Solar Longitude: {currentSolarLongitude}");

            debugOutput($"Moon-to-Umbra Distance: {_moonToUmbraVertex} km");
            debugOutput($"Sun-to-Umbra Distance: {_sunToUmbraVertex} km");
            debugOutput($"Sun-to-Umbra Distance (Vector): {_umbra.Length} km");


            debugOutput($"a: {_umbraAngle}");
            debugOutput($"r Sin(a): {SOLAR_RADIUS1 * Math.Sin(_umbraAngle)}");
            debugOutput($"Sun-Moon Plane: {tmpPlane}");


            debugOutput($"Center Intersects Earth: {_centerIntersectsEarth}");


            debugOutput($"Rotated Sun-Moon Vector: {_sunToMoonRotated}");
            debugOutput($"Rotated Sun-Moon Vector Distance: {_sunToMoonRotated.Length} km");
            debugOutput($"Sun-Earth Vector: {_sunToEarthRotated}");
            debugOutput($"Sun-Earth Vector Distance: {_sunToEarthRotated.Length} km");

            debugOutput($"Umbra Intersection: {_umbraIntersection}");
            debugOutput($"Umbra Intersection Rotated: {_umbraIntersectionRotated}");

            debugOutput($"Penumbra Intersection: {_penumbraIntersection}");
            debugOutput($"Penumbra Intersection Rotated: {_penumbraIntersectionRotated}");


            debugOutput($"**** End of Data ****");

            return null;

        }


        public bool RayIntersectsSphere(Ray3D ray, Point3D sphereCenter, double radius)
        {
            Vector3D rayCenterDiff = ray.ThroughPoint - sphereCenter;
            double rSquared = radius * radius;

            double dotpr = ray.Direction.DotProduct(rayCenterDiff);
            double dotpp = rayCenterDiff.DotProduct(rayCenterDiff);

            if ((dotpr > 0) || (dotpp < rSquared))
                return false;

            Vector3D a = rayCenterDiff - (ray.Direction.ScaleBy(dotpr));
            double aSquared = a.DotProduct(a);

            if (aSquared > rSquared)
                return false;

            return true;
        }

    }
}
