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

            int invalidLines = 0;
            int validLines = 0;

            List<FlightInfo> airlinePerformance2014 = new List<FlightInfo>() { };
            string line = string.Empty;

            StreamReader reader = new StreamReader(csvFile);

            reader.ReadLine().Skip(1);


            while ((line = reader.ReadLine()) != null)
            {
                string[] csvColumns = line.Split(',');
                if (CheckIfCSVLineIsValid(csvColumns))
                {
                    listName.Add(new FlightInfo()
                    {
                        Carrier = csvColumns[0],
                        OriginCityName = csvColumns[1],
                        DestinationCityName = csvColumns[2],
                        DepartureDelay = int.Parse(csvColumns[3]),
                        ArrivalDelay = int.Parse(csvColumns[4]),
                        Cancelled = int.Parse(csvColumns[5]),
                        Distance = int.Parse(csvColumns[6])
                    });
                    validLines++;
                }
                else
                    invalidLines++; // Keep these in mind! They might be why shecodes and j.archer get different results
                // because they didn't weed out as many lines. And maybe I shouldn't weed out any information that can be used?
                // But if one flight delay way unknown, then it doesn't make sense to divide 20min. delay by 10 flights (where only 9 
                // included delay data), correct? This is about averages after all.
                // Possibly include a class for debugging and csv infos where these things are stored as properties. E.g. csvAnalysis
                
                // Parse to class here :)

            }
            Console.WriteLine("Please Note:\nThe analysis below is based on data from {0} valid flights in the csv file.\n" +
                "({1} flight entries/lines in the csv were invalid and not included!)\n", validLines, invalidLines);
            return airlinePerformance2014;
            /* 
            Idea: Make incrementing line counter and implement error message pointing to the line where an invalid entry was found
            or rather print out a message detailing how many lines were weeded out for what specific problem (null entries, etc.etc.).
            */
        }

        /// <summary>
        /// Checks each line of the CSV for validity: No column except column 8 can be empty and there can be no more than 8 columns in a line.
        /// </summary>
        /// <param name="csvColumns"></param>
        /// <returns></returns>
        public static bool CheckIfCSVLineIsValid(string[] csvColumns)
        {
            return csvColumns.Length == 8
                //&& csvColumns.Contains(""); // This does not work.
                && csvColumns[0] != ""
                && csvColumns[1] != ""
                && csvColumns[2] != ""
                && csvColumns[3] != ""
                && csvColumns[4] != ""
                && csvColumns[5] != ""
                && csvColumns[6] != ""
                && csvColumns[6] != "0";                
        }
    }
}
