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
            Console.WriteLine("Query8:\nFrom {0} to {1} with an arrival delay of {2} and a distance of {3} miles.",
                weighted.FirstOrDefault().OriginCityName,
                weighted.FirstOrDefault().DestinationCityName,
                weighted.FirstOrDefault().ArrivalDelay,
                weighted.FirstOrDefault().Distance);

            // Query9. How many flights from Seattle, WA were not delayed on arrival?

            /*
            I made a pretty big mistake here when I used flight.ArrivalDelay == 0 at first.
            This left out hundreds of flights that arrived early!! Shecodes used flight.ArrivalDelay <= 0
            as below:
            */

            var undelayedFromSeattle = (from flight in airlinePerformance2014
                                        where flight.OriginCityName == "Seattle WA"
                                        where flight.ArrivalDelay <= 0
                                        select flight).Count();
            Console.WriteLine("\nQuery9\n:{0} flights from Seattle were not delayed at all.", undelayedFromSeattle);

            // Query10 What were the top 10 origin airports with the largest average departure delays, including the values of these delays?

            var top10AverageDelayedAirports = (from flight in airlinePerformance2014
                                              group flight.DepartureDelay by flight.OriginCityName into departureDelayGroup
                                              let averageDelay = departureDelayGroup.Average()
                                              orderby averageDelay descending
                                              select new {Airport = departureDelayGroup.Key, Delay = averageDelay }).Take(10);
            Console.WriteLine("\nQuery10:");
            // Note: I finally added some formatting. I added a field length (after comma) and a "-" for right alignment of object in that field.
            foreach (var airport in top10AverageDelayedAirports)
                Console.WriteLine("Airport {0,-25} had an average delay of {1,-5:#.#} minutes.", airport.Airport, airport.Delay);

            // Query11: (*) What is the airline with the worst average arrival delay on flights from New York, NY? 

            var worstAverageDelayAirlineFromNY = (from flight in airlinePerformance2014
                                                 where flight.OriginCityName == "New York NY"
                                                 group flight.ArrivalDelay by flight.Carrier into arrivalDelayCarrierGroup
                                                 let averageArrivalDelay = arrivalDelayCarrierGroup.Average()
                                                 orderby averageArrivalDelay descending
                                                 select new {Delay = averageArrivalDelay, Carrier = arrivalDelayCarrierGroup.Key}).Take(1);
            Console.WriteLine("\nQuery11:\nAirline {0} had an average delay of {1: #.##} minutes\nfor flights from New York NY",
                worstAverageDelayAirlineFromNY.ElementAtOrDefault(0).Carrier, worstAverageDelayAirlineFromNY.ElementAtOrDefault(0).Delay);

            // Query 12: Which origin airport is the worst to fly from in terms of average departure delay if you are flying American Airlines
            // (airline code: "AA")?

            var worstOriginByAverageDepartureArrayForAA = (from flight in airlinePerformance2014
                                                           where flight.Carrier == "AA"
                                                           group flight.DepartureDelay by flight.OriginCityName into departureDelayOrigins
                                                           let averageDepartureDelay = departureDelayOrigins.Average()
                                                           orderby averageDepartureDelay descending
                                                           select new { Delay = averageDepartureDelay, Origin = departureDelayOrigins.Key }).Take(1);

            Console.WriteLine("\nQuery12:\nAA has an average departure delay of {0:#.##} minutes\nwhen flying out of {1}.",
                worstOriginByAverageDepartureArrayForAA.ElementAtOrDefault(0).Delay, worstOriginByAverageDepartureArrayForAA.ElementAtOrDefault(0).Origin);

            // If I want handy access to elements in a list, rather than a sequence like above, I can convert using .ToList():
            var query12list = worstOriginByAverageDepartureArrayForAA.ToList();
            Console.WriteLine("\nQuery12\nOn average, AA departs {0} with a delay of {1:#.##} minutes.", query12list[0].Origin, query12list[0].Delay);

            // Query13: For all the 1 - stop flights between Boston, MA and Los Angeles, CA (not including direct flights), what was the flight path with the lowest
            // average arrival and departure delay ? (Note that this query can take a very long time to run.)

            /*
            The following, awquard first had run for 21 minute without results when I stopped it. I assume this could have something to do with 
            queries running (each time) when their results are called.
            */

            //var query13a = from flight in airlinePerformance2014
            //               where flight.OriginCityName == "Boston MA"
            //               where flight.DestinationCityName != "Los Angeles CA"
            //               select flight;

            //var query13b = from flight in airlinePerformance2014
            //               where flight.OriginCityName != "Boston MA"
            //               where flight.DestinationCityName == "Los Angeles CA"
            //               select flight;

            //var query13c = from flight in query13a
            //               join oneStopFlight in query13b
            //               on flight.DestinationCityName
            //               equals oneStopFlight.OriginCityName
            //               select flight;

            //var query13d = (from flight in query13c
            //                let averageArrivalDelay = (double)flight.ArrivalDelay / query13c.Count()
            //                let averageDepartureDelay = (double)flight.DepartureDelay / query13c.Count()
            //                let averageDelay = (averageArrivalDelay + averageDepartureDelay) / query13c.Count()
            //                orderby averageDelay
            //                select new { flight, averageDelay }
            //                ).Take(1);
            //Console.WriteLine("\nQuery13:\nFlights from Boston to Los Angeles via {0} had an average arrival and departure delay (combined) of {1}",
            //    query13d.ElementAtOrDefault(0).flight.DestinationCityName, query13d.ElementAtOrDefault(0).averageDelay);

            // Trying to speed it up by casting the query results to lists. The following code took 9 secondes to run and displayed:
            // Query13:
            // Flights from Boston to Los Angeles via Phoenix AZ had an average arrival and dep
            // arture delay(combined) of - 4,26052906325294E-12.

            var query13a = from flight in airlinePerformance2014
                           where flight.OriginCityName == "Boston MA"
                           where flight.DestinationCityName != "Los Angeles CA"
                           select flight;
            var query13aList = query13a.ToList();

            var query13b = from flight in airlinePerformance2014
                           where flight.OriginCityName != "Boston MA"
                           where flight.DestinationCityName == "Los Angeles CA"
                           select flight;
            var query13bList = query13b.ToList();

            var query13c = from flight in query13aList
                           join oneStopFlight in query13bList
                           on flight.DestinationCityName
                           equals oneStopFlight.OriginCityName
                           select flight;
            var query13cList = query13c.ToList();

            var query13d = (from flight in query13cList
                            let averageArrivalDelay = (double)flight.ArrivalDelay / query13cList.Count()
                            let averageDepartureDelay = (double)flight.DepartureDelay / query13cList.Count()
                            let averageDelay = (averageArrivalDelay + averageDepartureDelay) / query13cList.Count()
                            orderby averageDelay
                            select new { flight, averageDelay }
                            ).Take(1);
            // I don't understand why custom numeric format strings cause the number (negative double) to not be displayed:
            //Console.WriteLine("\nQuery13:\nFlights from Boston to New york via {0} had an average arrival and departure delay (combined) of {1:#.##}.",
            //    query13d.ElementAtOrDefault(0).flight.DestinationCityName, query13d.ElementAtOrDefault(0).averageDelay);
            
            Console.WriteLine("\nQuery13:\nFlights from Boston to Los Angeles via {0} had an average arrival and departure delay (combined) of {1}.",
                query13d.ElementAtOrDefault(0).flight.DestinationCityName, query13d.ElementAtOrDefault(0).averageDelay);


            /* 
            The following solution by Shecodes took 14:45 minutes to run and produced the result: Boston via Tampa FL to Los Angeles.
            when run in their solution.
            In this solution (why?) it ran about 5 minutes (probably bc of less multitasking) and produced:
            "Query13:
            Boston MA to Los Angeles CA via Tampa FL had average
            combined arrival/departure delay of -24,3113821138211
            */
            
            var query13 = (from leg1 in airlinePerformance2014
                           where leg1.OriginCityName == "Boston MA"
                           from leg2 in airlinePerformance2014
                           where leg2.OriginCityName == leg1.DestinationCityName
                           where leg2.DestinationCityName == "Los Angeles CA"
                           let totalDelay = leg1.DepartureDelay + leg1.ArrivalDelay + leg2.DepartureDelay + leg2.ArrivalDelay
                           group totalDelay by new { O1 = leg1.OriginCityName, O2 = leg1.DestinationCityName, D = leg2.DestinationCityName } into g
                           let averageTotalDelay = g.Average()
                           orderby averageTotalDelay ascending
                           select new { FlightLegs = g.Key, averageTotalDelay = averageTotalDelay }
              ).Take(1);
            var query13List = query13.ToList();

            Console.WriteLine("{0} to {1} via {2} had average\ncombined arrival/departure delay of {3}",
                query13List[0].FlightLegs.O1, query13List[0].FlightLegs.D, query13List[0].FlightLegs.O2, query13List[0].averageTotalDelay);
            
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
