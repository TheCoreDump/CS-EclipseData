using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipsedata
{
    public class PovRayDataWriter
    {
        public static void WriteData(TextWriter writer, int dataPoint, DataPoint earthData, Vector3D earthRotation, DataPoint moonData)
        {
            // DataPointNum, DateTime, Earth Center Pos, Earth Rotation Vector, Moon Center Pos
            writer.Write(dataPoint);
            writer.Write(",");
            writer.Write(earthData.UTCDate.ToPovRayDateTime());
            writer.Write(",");
            writer.Write(earthData.Center.ToPovRayVector());
            writer.Write(",");
            writer.Write(earthData.Rotation.ToPovRayVector());
            writer.Write(",");
            writer.WriteLine(moonData.Center.ToPovRayVector());
        }
    }
}
