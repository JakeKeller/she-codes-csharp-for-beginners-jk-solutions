using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod7FlightDelaysLinqJk
{
    class FlightInfo
    {
        public string Carrier { get; set; }
        public string OriginCityName { get; set; }
        public string DestinationCityName { get; set; }
        public int DepartureDelay { get; set; }
        public int ArrivalDelay { get; set; }
        public int Cancelled { get; set; }
        public int Distance { get; set; }

        public static List<FlightInfo> PopulateListFromCSV(string csvFile, List<FlightInfo> listName)
        {
            // For each row read by the streamreader run a .Add with a newly parsed object of class FlightInfo.
            // Read in a row, split the string into strings(or rather appropriate types) by comma

            // List<FlightInfo> airlinePerformance2014 = new List<FlightInfo>() { };
            string line = string.Empty;

            StreamReader reader = new StreamReader(csvFile);

            reader.ReadLine().Skip(1);

            while ((line = reader.ReadLine()) != null)
            {
                string[] csvColumns = line.Split(',');
                // Parse to class here :)
                listName.Add(new FlightInfo()
                {
                    Carrier = csvColumns[0],
                    OriginCityName = csvColumns[1],
                    DestinationCityName = csvColumns[2],
                    DepartureDelay = int.Parse(csvColumns[3]),
                    ArrivalDelay = int.Parse(csvColumns[4]),
                    Cancelled = int.Parse(csvColumns[5])
                });

            }
            return airlinePerformance2014;
            // Idea: Make incrementing line counter and implement error message pointing to the line where an invalid entry was found
        }
    }
}
