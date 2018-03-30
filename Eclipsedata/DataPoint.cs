using MathNet.Spatial.Euclidean;
using System;

namespace Eclipsedata
{
    public class DataPoint
    {
        public DataPoint(string rawData)
        {
            string[] parts = rawData.Split(',');

            JulianDate = Double.Parse(parts[0], System.Globalization.NumberStyles.Float);
            UTCDate = DateTime.Parse(parts[1]);
            Center = new Vector3D(Double.Parse(parts[2]), Double.Parse(parts[3]), Double.Parse(parts[4]));
            Velocity = new Vector3D(Double.Parse(parts[5]), Double.Parse(parts[6]), Double.Parse(parts[7]));

            LT = Double.Parse(parts[8]);
            Range = Double.Parse(parts[9]);
            RangeRate = Double.Parse(parts[10]);

            Rotation = new Vector3D(0, 0, 0);
        }

        public double JulianDate { get; set; }

        public DateTime UTCDate { get; set; }

        public Vector3D Center { get; set; }

        public Vector3D Velocity { get; set; }

        public Vector3D Rotation { get; set; }

        public double LT { get; set; }

        public double Range { get; set; }

        public double RangeRate { get; set; }

        public override string ToString() => $"DateTime: {UTCDate.ToString("yyyy-MM-dd HH:mm:ss")} Position: {Center}  Velocity: {Velocity}  LT: {LT} Range: {Range} RangeRate: {RangeRate} Julian Date: {JulianDate}";

    }
}
