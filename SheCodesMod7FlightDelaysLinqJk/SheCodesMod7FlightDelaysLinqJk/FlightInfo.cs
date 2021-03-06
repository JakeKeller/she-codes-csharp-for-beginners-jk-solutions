﻿using System;
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
                    invalidLines++;
                /*
                Keep the invald lines in mind! They might be why shecodes and j.archer get different results
                because they weeded out a different amount of "invalid" lines in the csv. 
                [I checked and no, at least shecodes' and my version have the same amount of valid lines]
                Does it make sense to ignore a line where only one item is missing in a query that only concerns the other items?
                Ideally, I would weed out invalid lines specifically for a query.

                Idea: Possibly include a class for debugging and csv infos where these things are stored as properties. E.g. csvAnalysis.
                In there I could also make an incrementing line counter variable and implement error messages pointing to the line 
                where an invalid entry was found or rather print out a message detailing how many lines were weeded out for what 
                specific problem (null entries, missing entries (of type x) etc.etc.).
                */
            }
            Console.WriteLine("Please Note:\nThe analysis below is based on data from {0} valid flights in the csv file.\n" +
                "({1} flight entries/lines in the csv were invalid and not included!)\n", validLines, invalidLines);
            return airlinePerformance2014;
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
