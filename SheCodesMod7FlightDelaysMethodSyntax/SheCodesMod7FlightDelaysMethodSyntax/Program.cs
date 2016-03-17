using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod7FlightDelaysMethodSyntax
{
    class Program
    {
        static void Main(string[] args)
        {
            const string csvName = "airline-on-time-performance-sep2014-us.csv";
            List<FlightInfo> airlinePerformance2014 = new List<FlightInfo>() { };
            FlightInfo.PopulateListFromCSV(csvName, airlinePerformance2014);

            /* 
            Use lambda expressions to query the data.
            Firstly, I want to select all flights from New York to Los Angeles.
            Then get the sum of their delays.
            Divide that sum by the number of flights (double value, instead of int)
            Display average delay

            After that make a prompt and take in the user values to do the calculation.
            Check with test file (remember that weeded out lines will throw this off a bit!)
            */

            // Using a LINQ expression:
            //var newYorkToLAFlights = from flightInfo in airlinePerformance2014
            //                         where flightInfo.OriginCityName == "New York NY"
            //                         && flightInfo.DestinationCityName == "Los Angeles CA"
            //                         select flightInfo;
            //Console.WriteLine("Average Flight Arrival Delay: " + (newYorkToLAFlights.Sum(p => p.ArrivalDelay) / ((double)newYorkToLAFlights.Count())));

            // Using a lambda expression:
            var newYorkToLAFlights = airlinePerformance2014.Where(p => p.OriginCityName == "New York NY").Where(p => p.DestinationCityName == "Los Angeles CA");
            Console.WriteLine("Average Flight Arrival Delay: " + (newYorkToLAFlights.Sum(p => p.ArrivalDelay) / ((double)newYorkToLAFlights.Count())));


            //double delaySum = newYorkToLAFlights.Sum(p => p.ArrivalDelay);
            //Console.WriteLine(delaySum);
            //Console.WriteLine((double)newYorkToLAFlights.Count());


            Console.ReadLine();
        }
    }
}
