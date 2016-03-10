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

            /*
            Set breakpoints to check the results for the linq statements below without having them all run after each other.
            */

            // Query1. What were the arrival delays of all the flights from Boston, MA to Chicago, IL?
            
            var delaysBostonToChicago = from flight in airlinePerformance2014
                                        where flight.OriginCityName == "Boston MA"
                                        && flight.DestinationCityName == "Chicago IL"
                                        select flight.ArrivalDelay;

            // Query2.What were the airports with direct flights to Monterey, CA ? (Hint: useq​uery.Distinct()​
            //var AirportsWithdirectFlightsToMonterey = from flight in airlinePerformance2014
            //                                          where flight.DestinationCityName == "Monterey CA"
            //                                          select flight.OriginCityName;
            //foreach (var originCity in AirportsWithdirectFlightsToMonterey.Distinct())
            //    Console.WriteLine(originCity);

            // Query2. From SheCodes solution (more concise):
            var AirportsWithdirectFlightsToMonterey = (from flight in airlinePerformance2014
                                                       where flight.DestinationCityName == "Monterey CA"
                                                       select flight.OriginCityName).Distinct();

            // Query3. What was the origin and destination of the 10 flights that had the largest arrival delay?
            // The "select new" clause is awesome :) 
            var flightsSortedByArrivalDelayDescending = (from flight in airlinePerformance2014
                                                         orderby flight.ArrivalDelay descending
                                                         select new { flight.OriginCityName, flight.DestinationCityName, flight.ArrivalDelay }
                                                         ).Take(10);

            // Query4. What was the destination airport for the first 20 flights? 
            var DestAirportForFirst20FLights = (from flight in airlinePerformance2014
                                                select flight.DestinationCityName).Take(20);

            // Query5. What was the average arrival delay across all flights?
            //var arrivalDelays = from flight in airlinePerformance2014
            //                    select flight.ArrivalDelay;
            //double averageDelay = (double)arrivalDelays.Sum() / (double)arrivalDelays.Count();
            //Console.WriteLine("Average delay in minutes: {0:0.##}", averageDelay);
            ////// The # custom specifier rounds your numbers ("away from zero") to the desired point, just like Math.Round below:
            ////Console.WriteLine("Average delay in minutes: {0}", Math.Round(averageDelay, 2));
            
            // Query5. From SheCodes solution. Using .Average():
            var arrivalDelays2 = (from flight in airlinePerformance2014
                                  select flight.ArrivalDelay).Average();

            // Query6. What was the origin and destination of the shortest flight in the data set and what is the 
            // distance of that flight?

            var shortestFlight = (from flight in airlinePerformance2014
                                  orderby flight.Distance
                                  select new { flight.Distance, flight.OriginCityName, flight.DestinationCityName }).Take(1);

            /*
            At first I had trouble accessing the properties of the anonymous type (shortestFlight) created by the linq statement above. 
            I didn't want to use a foreach statement for a sequence with just one object.
            However, all of the methods like FirstOrDefault(), Single() (and even more) below can do that:
            */
            Console.WriteLine("Query6:\nThe shortest flight was from {0} to {1} \nat a distance of {2} miles.\n", shortestFlight.FirstOrDefault().OriginCityName, shortestFlight.Single().DestinationCityName, shortestFlight.ElementAt(0).Distance);

            // Query7. What was the longest flight out of San Francisco, CA?
            var longestFlightFromSanFrancisco = (from flight in airlinePerformance2014
                                                 where flight.OriginCityName == "San Francisco CA"
                                                 orderby flight.Distance descending
                                                 select new { flight.Distance, flight.OriginCityName, flight.DestinationCityName }
                                                 ).Take(1);
            Console.WriteLine("Query7:\nThe longest flight departing from {0} went to {1} and had a distance of {2} miles.\n",
                longestFlightFromSanFrancisco.SingleOrDefault().OriginCityName,
                longestFlightFromSanFrancisco.SingleOrDefault().DestinationCityName,
                longestFlightFromSanFrancisco.SingleOrDefault().Distance);

            // Query8. The weighted arrival delay of a flight is its arrival delay divided by the distance. What
            // was the flight with the largest weighted arrival delay out of Boston, MA?  
            
            //var weighted = (from flight in airlinePerformance2014
            //                where flight.OriginCityName == "Boston MA"
            //                orderby (flight.ArrivalDelay / flight.Distance) descending
            //                select flight).Take(1);
            //Console.WriteLine("Query8:\nFrom {0} to {1} with an arrival delay of {2} and a distance of {3}\n",
            //    weighted.FirstOrDefault().OriginCityName,
            //    weighted.FirstOrDefault().DestinationCityName,
            //    weighted.FirstOrDefault().ArrivalDelay,
            //    weighted.FirstOrDefault().Distance);

            /* 
            My solution above worked. However, SheCodes used the let keyword:
            From DotNetPerls:
            "Let is a part of a query expression. It introduces a variable. We can then reuse the variable elsewhere in the query. 
            This makes possible certain complex query expressions, and makes other expressions simpler."
            In debugging mode, the "weighted" variable is hidden in the "Autos" window under "Non Public members". 
            */

            // Query8. From Shecodes solution:
            var weighted = (from flight in airlinePerformance2014
                            where flight.OriginCityName == "Boston MA"
                            let weightedDelay = flight.ArrivalDelay / (double)flight.Distance
                            orderby weightedDelay descending
                            select flight
             ).Take(1);
            Console.WriteLine("Query8:\nFrom {0} to {1} with an arrival delay of {2} and a distance of {3} miles.\n",
                weighted.FirstOrDefault().OriginCityName,
                weighted.FirstOrDefault().DestinationCityName,
                weighted.FirstOrDefault().ArrivalDelay,
                weighted.FirstOrDefault().Distance);

            // 9. How many flights from Seattle, WA were not delayed on arrival?

            /*
            I made a pretty big mistake here when I used flight.ArrivalDelay == 0 at first.
            This left out hundreds of flights that arrived early!! Shecodes used flight.ArrivalDelay <= 0
            as below:
            */

            var undelayedFromSeattle = (from flight in airlinePerformance2014
                                        where flight.OriginCityName == "Seattle WA"
                                        where flight.ArrivalDelay <= 0
                                        select flight).Count();
            Console.WriteLine("{0} flights from Seattle were not delayed at all.", undelayedFromSeattle);

            // 10 What were the top 10 origin airports with the largest average departure delays, including the values of these delays?

            var top10AverageDelayedAirports = (from flight in airlinePerformance2014
                                              group flight.DepartureDelay by flight.OriginCityName into departureDelayGroup
                                              let averageDelay = departureDelayGroup.Average()
                                              orderby averageDelay descending
                                              select new {Airport = departureDelayGroup.Key, Delay = averageDelay }).Take(10);
            Console.WriteLine("\nQuery10:\n");
            foreach (var airport in top10AverageDelayedAirports)
                Console.WriteLine("Airport {0} had an average delay of {1:#.#} minutes.", airport.Airport, airport.Delay);




            //var arrivalDelays = from flight in airlinePerformance2014
            //                    select flight.ArrivalDelay;
            //double averageDelay = (double)arrivalDelays.Sum() / (double)arrivalDelays.Count();
            //Console.WriteLine("Average delay in minutes: {0:0.####}", averageDelay);
            //Console.WriteLine("Average delay in minutes: {0}", Math.Round(averageDelay, 2));


            //var averageDelayList = (from flight in airlinePerformance2014
            //                        orderby (flight.DepartureDelay)
            //                        let averageDelay = (double)flight.DepartureDelay / airlinePerformance2014.Count()
            //                        orderby averageDelay descending, flight.OriginCityName
            //                        select new { OriginCityName = flight.OriginCityName, AverageDepartureDelay = averageDelay }).Take(10);

            ////var top10ByAirports = (from line in flightsByAverageDelay
            ////                       where line.OriginCityName.
            ////                       select new {Origin = line.OriginCityName.Distinct(), line.AverageDepartureDelay }).Take(10);


            //// orderby flight.DepartureDelay / airlinePerformance2014.Count() descending

            //StringBuilder sb = new StringBuilder();

            //foreach (var flight in flightsByAverageDelay)
            //{
            //    sb.Append(flight.OriginCityName + ":" + flight.AverageDepartureDelay + "\n");
            //}
            //Console.WriteLine(sb);

            //// SheCodes solution: // MINE SEEMS WRONG! // 
            //var top10 = (from flight in airlinePerformance2014
            //               group flight.DepartureDelay by flight.OriginCityName into g
            //               let averageDelay = g.Average()
            //               orderby averageDelay descending
            //               select new { Airport = g.Key, DepartureDelay = averageDelay }
            //  ).Take(10);

            //StringBuilder sb = new StringBuilder();

            //foreach (var flight in top10)
            //{
            //    sb.Append(flight.Airport + ":" + flight.DepartureDelay + "\n");
            //}
            //Console.WriteLine(sb);


            //foreach (var flight in flightsSortedByArrivalDelayDescending.Take(10))
            //{
            //    Console.WriteLine("Flight from {0} to {1} had a delay of {2} minutes!", flight.OriginCityName, flight.DestinationCityName, flight.ArrivalDelay);
            //}

            //for (int i = 0; i < 11; i++)
            //{
            //    foreach (var flight in flightsSortedByArrivalDelayDescending.Take(10))
            //    {
            //        Console.WriteLine("Flight from {0} to {1} had a delay of {2} minutes!", flight.OriginCityName, flight.DestinationCityName, flight.ArrivalDelay);
            //    }

            //}


            //var tenLargestArrivalDelayFlights = new List<FlightInfo>();

            //for (int i = 0; i < 11; i++)
            //{
            //    tenLargestArrivalDelayFlights.Add((FlightInfo)flightsSortedByArrivalDelayDescending.Skip(i)); 
            //}

            //foreach (var flight in tenLargestArrivalDelayFlights)
            //    Console.WriteLine("Flight from {0} to {1} had a delay of {3} minutes!", flight.OriginCityName, flight.DestinationCityName, flight.ArrivalDelay);

            // 




            //// 1. What were the arrival delays of all the flights from Boston, MA to Chicago, IL?
            //var delaysBostonToChicago = airlinePerformance2014.Where(p => p.OriginCityName == "Boston MA").Where(p => p.DestinationCityName == "Chicago IL");
            //foreach (var flight in delaysBostonToChicago)
            //    Console.WriteLine(flight.ArrivalDelay);

            //Console.WriteLine(delaysBostonToChicago.Sum(p => p.ArrivalDelay));





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
            //var newYorkToLAFlights = airlinePerformance2014.Where(p => p.OriginCityName == "New York NY").Where(p => p.DestinationCityName == "Los Angeles CA");
            //Console.WriteLine("Average Flight Arrival Delay: " + (newYorkToLAFlights.Sum(p => p.ArrivalDelay) / ((double)newYorkToLAFlights.Count())));


            //double delaySum = newYorkToLAFlights.Sum(p => p.ArrivalDelay);
            //Console.WriteLine(delaySum);
            //Console.WriteLine((double)newYorkToLAFlights.Count());


            Console.ReadLine();
        }
    }
}
