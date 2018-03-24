using System;

namespace DataFileParser
{
    public class DataPoint
    {
        public DataPoint(string rawData)
        {
            string[] parts = rawData.Split(',');

            JulianDate = Decimal.Parse(parts[0], System.Globalization.NumberStyles.Float);
            UTCDate = DateTime.Parse(parts[1]);
            X = Decimal.Parse(parts[2], System.Globalization.NumberStyles.Float);
            Y = Decimal.Parse(parts[3], System.Globalization.NumberStyles.Float);
            Z = Decimal.Parse(parts[4], System.Globalization.NumberStyles.Float);
            vX = Decimal.Parse(parts[5], System.Globalization.NumberStyles.Float);
            vY = Decimal.Parse(parts[6], System.Globalization.NumberStyles.Float);
            vZ = Decimal.Parse(parts[7], System.Globalization.NumberStyles.Float);
            LT = Decimal.Parse(parts[8], System.Globalization.NumberStyles.Float);
            Range = Decimal.Parse(parts[9], System.Globalization.NumberStyles.Float);
            RangeRate = Decimal.Parse(parts[10], System.Globalization.NumberStyles.Float);
        }

        public decimal JulianDate { get; set; }

        public DateTime UTCDate { get; set; }

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }

        public decimal vX { get; set; }

        public decimal vY { get; set; }

        public decimal vZ { get; set; }

        public decimal LT { get; set; }

        public decimal Range { get; set; }

        public decimal RangeRate { get; set; }

        public override string ToString() => $"DateTime: {UTCDate.ToString("yyyy-MM-dd HH:mm:ss")} Position: <{X},{Y},{Z}>  Velocity: <{vX},{vY},{vZ}>  LT: {LT} Range: {Range} RangeRate: {RangeRate} Julian Date: {JulianDate}";

    }
}
