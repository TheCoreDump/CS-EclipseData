using MathNet.Spatial.Euclidean;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipsedata
{
    public static class Extensions
    {
        public static JObject ToJObject(this Vector3D v)
        {
            return new JObject(
                new JProperty("X", v.X),
                new JProperty("Y", v.Y),
                new JProperty("Z", v.Z),
                new JProperty("Length", v.Length)
                );
        }


        public static JObject ToJObject(this UnitVector3D uv)
        {
            return new JObject(
                new JProperty("X", uv.X),
                new JProperty("Y", uv.Y),
                new JProperty("Z", uv.Z)
                );
        }



        public static JObject ToJObject(this Point3D p)
        {
            return new JObject(
                new JProperty("X", p.X),
                new JProperty("Y", p.Y),
                new JProperty("Z", p.Z)
                );
        }




        public static JObject ToJObject(this Ray3D r)
        {
            return new JObject(
                new JProperty("Direction", r.Direction.ToJObject()),
                new JProperty("Through Point", r.ThroughPoint.ToJObject())
                );
        }

        public static string ToPovRayVector(this Vector3D v) => $"<{v.X},{v.Y},{v.Z}>";


        public static string ToPovRayDateTime(this DateTime dt) => $"\"{dt.ToString("yyyy-MM-dd HH:mm:ss+00:00")}\"";

    }
}
