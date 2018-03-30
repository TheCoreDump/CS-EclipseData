using System;

namespace DataFileParser
{
    public class DataPoint
    {
        public DataPoint(string rawData)
        {
            string[] parts = rawData.Split(',');

            JulianDate = Double.Parse(parts[0]);
            UTCDate = DateTime.Parse(parts[1]);
            X = Double.Parse(parts[2]);
            Y = Double.Parse(parts[3]);
            Z = Double.Parse(parts[4]);
            vX = Double.Parse(parts[5]);
            vY = Double.Parse(parts[6]);
            vZ = Double.Parse(parts[7]);
            LT = Double.Parse(parts[8]);
            Range = Double.Parse(parts[9]);
            RangeRate = Double.Parse(parts[10]);
        }

        public double JulianDate { get; set; }

        public DateTime UTCDate { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double vX { get; set; }

        public double vY { get; set; }

        public double vZ { get; set; }

        public double LT { get; set; }

        public double Range { get; set; }

        public double RangeRate { get; set; }

        public override string ToString() => $"DateTime: {UTCDate.ToString("yyyy-MM-dd HH:mm:ss")} Position: <{X},{Y},{Z}>  Velocity: <{vX},{vY},{vZ}>  LT: {LT} Range: {Range} RangeRate: {RangeRate} Julian Date: {JulianDate}";

    }
}
