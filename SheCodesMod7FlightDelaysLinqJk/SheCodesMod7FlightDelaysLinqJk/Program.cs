using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod7FlightDelaysLinqJk
{
    class Program
    {
        static void Main(string[] args)
        {
            const string csvName = "airline-on-time-performance-sep2014-us.csv";
            List<FlightInfo> airlinePerformance2014 = new List<FlightInfo>() { };
            FlightInfo.PopulateListFromCSV(csvName, airlinePerformance2014);
        }
    }
}
