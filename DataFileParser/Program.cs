using System;
using System.Collections.Generic;
using System.IO;

namespace DataFileParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<DataPoint> allPoints = new List<DataPoint>();

            FileInfo sourceFile = new FileInfo("C:\\Dev\\Projects\\Personal\\C#\\CS-EclipseData\\Data\\Earth Data.csv");

            if (!sourceFile.Exists)
                throw new FileNotFoundException($"File not found", sourceFile.FullName);

            using (StreamReader SR = sourceFile.OpenText())
            {
                string lineData; 

                while (null != (lineData = SR.ReadLine()))
                {
                    string tmpData = lineData.Trim();

                    if (!string.IsNullOrEmpty(tmpData))
                        allPoints.Add(new DataPoint(tmpData));
                }
            }

            allPoints.ForEach((dp) => Console.WriteLine(dp.ToString()));
        }
    }
}
