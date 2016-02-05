using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod5FlightDelaysJk
{
    class Program
    {
        /* 
        This const is one thing I put in after completing my solution and looking at the version from shecodes.
        It allows me to change from the original csv file to test versions by just changing the filename once.
        */
        const string airlinePerformanceFileName = "airline-on-time-performance-sep2014-us.csv";

        static bool CheckCsvLines(string[] splitLine)
        {
            /* 
            This helper method checks the lines in the csv whether they are fit to be analyzed.
            (lines that have no value rather than a "0" in flight delays and such)
            There is an overloaded version of this method below it.
            */

            bool linesCheckOut = false;
            if ((splitLine.Length == 8) // Weeds out lines that have less than 7 commas (which is less than 8 elements in the array)
                    && (splitLine[4] != "") // Weeds out lines that have an empty string in element 4
                    && (splitLine[0] != "") // Weeds out lines that have an empty string in element 0
                    && (splitLine[0] != "CARRIER")) // Ignores line one with the column names like CARRIER
            {
                linesCheckOut = true;
            }
            return linesCheckOut;
        }

        static bool CheckCsvLines(string[] splitLine, string origin, string destination)
        {
            // This overloaded version makes sure only lines are used that contain the user-defined origin and destination
            bool linesCheckOut = false;
            if ((splitLine.Length == 8) // Weeds out lines that have less than 7 commas (which is less than 8 elements in the array)
                    && (splitLine[1] == origin) // Weeds out lines where splitLine[1] does not match origin
                    && (splitLine[2] == destination) // Weeds out lines where splitLine[2] does not match destination
                    && (splitLine[4] != "") // Weeds out lines that have an empty string in element 4
                    && (splitLine[0] != "") // Weeds out lines that have an empty string in element 0
                    && (splitLine[0] != "CARRIER")) // Ignores line one with the column names like CARRIER
            {
                linesCheckOut = true;
            }

            return linesCheckOut;
        }

        static bool DisplayMoreInfo()
        {
            // This makeshift method uses syntax I've learned so far to display a continue-or-quit-option to the user
            bool displayMoreInfo = true;
            string continueOrQuit = "";
            Console.WriteLine("\nWould you like to continue? Press \"C\" followed by Enter to continue \nor any other key and Enter to close this program.");
            // Using toUpper() here so if user unsuspectingly types lower case c rather than C program still continues.
            continueOrQuit = Console.ReadLine().ToUpper();
            if (continueOrQuit != "C")
            {
                displayMoreInfo = false;
            }
            return displayMoreInfo;
        }

        static void CountRelevantLines(string origin, string destination)
        {
            int lineCounter = 0;
            int delay = 0;
            int maxDelay = 0;
            string line = "";

            StreamReader reader = new StreamReader(airlinePerformanceFileName);

            // This while statement makes sure the loop runs only until reader returns a null value at the end of the csv file
            while ((line = reader.ReadLine()) != null)
            {
                /*
                This method splits the string (line) - that the reader keeps feeding each new line of the csv 
                file to - into an array of strings that are like the cells for each line of a csv spreadsheet
                */
                string[] splitLine = line.Split(',');

                if (CheckCsvLines(splitLine, origin, destination)) // performs checks on each line of the csv to weed out invalid lines, returns bool
                {
                    delay = delay + int.Parse(splitLine[4]);
                    lineCounter++;
                    if (int.Parse(splitLine[4]) > maxDelay)
                    {
                        maxDelay = int.Parse(splitLine[4]);
                    }

                }
            }
            reader.Close();
            // The following makes sure program gives feedback when user input is invalid
            if (lineCounter == 0)
            {
                Console.WriteLine("I'm sorry, there were no flights matching your airports.");
                Console.WriteLine("Please exit the program by pressing Enter");
                Console.ReadLine();
                // Method I googled which exits the program:
                Environment.Exit(1);
            }
            // Calculates average delay:
            else
            {
                /* 
                I needed to make the variable verageDelay a double, as well as one of the numbers
                in my division, because an integer division will always results in an integer 
                (and I wouldn't get any decimal places)
                */
                double averageDelay;
                averageDelay = Convert.ToDouble(delay) / lineCounter;
                // The F1 operator formats my double value so only one decimal place is displayed
                Console.WriteLine("\n***\nUsing data from {0} flights from {1} to {2} in 2014, \nthe average arrival delay "
                    + "calculated is {3} minutes. \n\nThe maximum delay that was experienced was {4} minutes. \n\n" +
                    "(A negative number means the flight arrived ahead of time, \na positive number means it arrived late.)\n***"
                    , lineCounter.ToString(), origin, destination, String.Format("{0:F1}", averageDelay), maxDelay.ToString());
            }
        }

        static void CountRelevantAirlines(string origin, string destination)
        {
            int airlineCounter = 0;
            string airlineName = "";
            string line = "";

            StreamReader reader = new StreamReader(airlinePerformanceFileName);

            // This while statement makes sure the loop runs only til reader returns a null value at the end of the txt file
            while ((line = reader.ReadLine()) != null)
            {
                /*
                This method splits the string (line) - that the reader keeps feeding each new line of the csv 
                file to - into an array of strings that are like the cells for each line of a csv spreadsheet
                */
                string[] splitLine = line.Split(',');

                if (CheckCsvLines(splitLine, origin, destination)) // Method performs checks on each line of the csv to weed out invalid lines, returns bool
                {
                    if (splitLine[0] != airlineName)
                    {
                        airlineCounter++;
                        airlineName = splitLine[0];
                    }

                }
            }
            reader.Close();
            // Debug message
            if (airlineCounter != 0)
            {
                Console.WriteLine("\n***\nHere are the results grouped by airline:");
                Console.WriteLine("Number of airlines that flew from {0} to {1}: {2}", origin, destination, airlineCounter.ToString());
                ListRelevantAirlines(origin, destination, airlineCounter);
            }
        }

        static void ListRelevantAirlines(string origin, string destination,
            int airlineCounter) // Gives me needed size for the relAirlines[] array
        {
            // Creating an array of size airLineCounter
            string[] relAirlines = new string[airlineCounter];
            int i = 0;
            string line = "";

            StreamReader reader = new StreamReader(airlinePerformanceFileName);

            // This while statement makes sure the loop runs only til reader returns a null value at the end of the txt file
            while ((line = reader.ReadLine()) != null)
            {
                /*
                This method splits the string (line) - that the reader keeps feeding each new line of the csv 
                file to - into an array of strings that are like the cells for each line of a csv spreadsheet
                */
                string[] splitLine = line.Split(',');

                if (CheckCsvLines(splitLine, origin, destination)) // Method performs checks on each line of the csv to weed out invalid lines, returns bool
                {
                    if (relAirlines[0] == null)
                    {
                        relAirlines[0] = splitLine[0];
                    }
                    if ((relAirlines[i] != splitLine[0]) && (i < (airlineCounter - 1)))
                    {
                        i++;
                        relAirlines[i] = splitLine[0];
                    }

                }
            }
            reader.Close();
            // Debug message:
            // Using a new method here to convert a string array to a string I can even insert a comma between each former array element:    
            string airlineList = string.Join(",", relAirlines);
            Console.WriteLine("Your connection was offered by these airlines: " + airlineList);
            DelayPerAirline(origin, destination, airlineCounter, relAirlines);
        }

        static void DelayPerAirline(string origin, string destination,
            int airlineCounter, // Gives me the needed size for the delayPerAirline[] array
            string[] relAirlines) // List of airlines needed to print out in conjunction with delayPerAirline[] array
        {
            int[] delayPerAirline = new int[airlineCounter]; // creates int array of size airlineCounter
            int[] flightsPerAirline = new int[airlineCounter];
            int i = 0;
            string line = "";

            StreamReader reader = new StreamReader(airlinePerformanceFileName);

            // this while statement makes sure the loop runs only til reader returns a null value at the end of the txt file
            while ((line = reader.ReadLine()) != null)
            {
                /*
                This method splits the string (line) - that the reader keeps feeding each new line of the csv 
                file to - into an array of strings that are like the cells for each line of a csv spreadsheet
                */
                string[] splitLine = line.Split(',');

                if (CheckCsvLines(splitLine, origin, destination)) // Performs checks on each line of the csv to weed out invalid lines, returns bool

                {
                    if (relAirlines[i] == splitLine[0])
                    {
                        delayPerAirline[i] = delayPerAirline[i] + int.Parse(splitLine[4]);
                        flightsPerAirline[i]++;
                    }
                    else if (i < airlineCounter)
                    {
                        i++;
                        delayPerAirline[i] = delayPerAirline[i] + int.Parse(splitLine[4]);
                        flightsPerAirline[i]++;
                    }
                }
            }
            reader.Close();

            // Stringbuilder to display both items of list of airline and of delay per airline next to each other in a list
            for (int j = 0; j < airlineCounter; j++)
            {
                StringBuilder sb = new StringBuilder();
                {
                    /*
                    This is a bit overkill, I converted one of the numbers into a double so I would get a division result with decimal places
                    then put in a String.Format sequence that gives me just one decimal place.
                    I suspect in this instance it would have sufficed to just divide ints and get a cut-off (not rounded afaik) int result
                    Also, I could have used the smaller float data type.
                    Also, I cheated a little using a fancy String.Format trick to shorten the displayed floating point value that hadn't been covered.
                    */
                    sb.Append(relAirlines[j] + " had an average delay of: " + String.Format("{0:F1}", (Convert.ToDouble(delayPerAirline[j]) / flightsPerAirline[j])) + " minutes.");
                }
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine("***");
        }

        static int AirportChangeCounter(int airportTypeIndex)
        {
            /* 
            This method serves to find a suitable maximum size for an array of airports that I intend to create.
            This method counts the number of times the airport changes in the csv (airportNameChanges). This serves to later
            build an array of size airportNameChanges and store the departure/arrival airports (but only distinct ones) in it.
            I could create an array of size "number of lines in the csv" but that would a huge overkill. I could guess but that
            would create problems if I were to use bigger csv files in the future.
            This way I at least get the size of that array down as much as possible I count the changes because there 
            cannot be more airports in the csv than there are changes of airports in it (but there can be and in fact clearly 
            are a lot fewer than lines in the csv.)
            */

            int airportNameChanges = 0;
            string airportname = "";
            string line = "";

            StreamReader reader = new StreamReader(airlinePerformanceFileName);

            // This while statement makes sure the loop runs only til reader returns a null value at the end of the txt file
            while ((line = reader.ReadLine()) != null)
            {
                /*
                This method splits the string (line) - that the reader keeps feeding each new line of the txt 
                file to - into an array of strings that are like the cells of a csv spreadsheet
                */
                string[] splitLine = line.Split(',');

                if (CheckCsvLines(splitLine)) // performs checks on each line of the csv to weed out invalid lines, returns bool
                {
                    if ((splitLine[airportTypeIndex] != airportname))
                    {
                        airportNameChanges++;
                        airportname = splitLine[airportTypeIndex];
                    }
                }
            }
            reader.Close();

            if (airportNameChanges != 0)
            {
                airportNameChanges--; // Decrement by 1 because the first change counted was from "" to first airport in csv
                // Optional debug message:
                //Console.WriteLine("\nNumber of airport changes airport in the csv is: {1}.", airportNameChanges);
            }
            return airportNameChanges;
        }

        static void RankAirports(int airportNameChanges, int airportTypeIndex)
        {
            string[] airportsArr = new string[airportNameChanges];
            int i = 0;
            string line = "";

            StreamReader reader = new StreamReader(airlinePerformanceFileName);

            // This while statement makes sure the loop runs only til reader returns a null value at the end of the txt file
            while ((line = reader.ReadLine()) != null)
            {
                /*
                This method splits the string (line) - that the reader keeps feeding each new line of the txt 
                file to - into an array of strings that are like the cells of a csv spreadsheet
                */
                string[] splitLine = line.Split(',');

                if (CheckCsvLines(splitLine)) // Performs checks on each line of the csv to weed out invalid lines, returns bool

                {
                    if (airportsArr[0] == null)
                    {
                        airportsArr[0] = splitLine[airportTypeIndex];
                    }
                    if ((airportsArr[i] != splitLine[airportTypeIndex])
                        && (i < (airportNameChanges - 1)) // Unnecessary?
                        && (!airportsArr.Contains(splitLine[airportTypeIndex]))) // Makes sure airport is not already included in the array
                    {
                        i++;
                        airportsArr[i] = splitLine[airportTypeIndex];
                    }

                }
            }
            reader.Close();

            /*
            This for-loop lists all departure/arrival airports in the csv. it also counts each element 
            in the array that isn't null (remember how I didn't know the size for the array and made it huge
            (number of changes in departure/arrival airport in the csv) and thus arrives at the actual number 
            of departure/arrival airports. I have a problem where the output to the console is now too long 
            and the beginning gets cut off. Display page by page (like I used to be able on a DOS prompt) 
            would be nice...or I could write all results to a to text file...
            */
            int actualAirpNumber = 0;
            for (int j = 0; j < airportNameChanges; j++)
            {
                if (airportsArr[j] != null)
                {
                    actualAirpNumber++;
                    // Optional display of all airports in the array:
                    //StringBuilder sb = new StringBuilder();
                    //{
                    //    sb.Append(departureAirportsArr[j]);
                    //}
                    //Console.WriteLine(sb.ToString());
                }

            }
            // Another debug message for me to test whether prog is counting airports correctly with a small csv file
            // Console.WriteLine("Number of airports in AirportsArr: {0}", actualAirpNumber);

            /*
            Since AirportsArr is an unwieldy monster of an string array (size 170603 lines with the original csv)
            I have to get rid of it or it will slow the program down immensely. I now know the maximum number 
            of departure/arrival airports, so I'll use that to create airportsArrClean which is a version of the array 
            without thousands of elements of value "null". This is a workaround I'm sure there is a method to trim 
            off "null" elements at the end of an array and again, lists would have been easier.
            */
            string[] airportsArrClean = new string[actualAirpNumber];
            foreach (var item in airportsArr)
            {
                if (item != null)
                {
                    airportsArrClean[Array.IndexOf(airportsArr, item)] = item;
                }
            }
            //This calls the last method, which calculates and displays the delays by departure/arrival airport
            DelaySumPerAirport(airportsArrClean, airportTypeIndex);

            // Debug messages printing the length of airportsArrClean. Cheat: used the nice nameof method here.
            // Console.WriteLine("Array \"{0}\" has a length of {1}", nameof(airportsArrClean), airportsArrClean.Length);

        }

        static void DelaySumPerAirport(string[] airportsArrClean, int airportTypeIndex)
        {
            int[] delaySum = new int[airportsArrClean.Length];
            int[] flightsPerAirport = new int[airportsArrClean.Length];
            int i = 0;
            string line = "";

            StreamReader reader = new StreamReader(airlinePerformanceFileName);

            // this while statement makes sure the loop runs only til reader returns a null value at the end of the txt file
            while ((line = reader.ReadLine()) != null)
            {
                /*
                This method splits the string (line) - that the reader keeps feeding each new line of the txt 
                file to - into an array of strings that are like the cells of a csv spreadsheet
                */
                string[] splitLine = line.Split(',');

                if (CheckCsvLines(splitLine)) // Performs checks on each line of the csv to weed out invalid lines, returns bool

                /*
                Below, code that - for each airport that the reader (above) comes across in the csv - 
                looks for its match in airportsArrClean[] and once found, adds the delays of splitline[4] 
                in a corresponding element (same index) of delaySum[]
                Possible performance issue: for each of the hundreds of thousands of lines in the csv, this program runs through 
                (up to) the entire airportsArrClean array. How can I avoid that? Would using .Contains and .IndexOf lead to a more 
                optimized result?
                */

                {
                    if (splitLine[airportTypeIndex] != airportsArrClean[i])
                    {
                        // Runs through the array until it finds a match, then sets the index for delaySum to the index of the match
                        for (int j = 0; j < airportsArrClean.Length; j++)
                        {
                            if (airportsArrClean[j] == splitLine[airportTypeIndex])
                            {
                                i = j;
                                // A missing break keyword had the program running for minutes before I fixed it!!
                                break;
                            }
                        }
                    }

                    // When matched, the delay from that line of the csv is added to delaySum and flightsPerAirport are counted
                    delaySum[i] += int.Parse(splitLine[4]);
                    flightsPerAirport[i] += 1;
                }
            }
            reader.Close();

            // Debug message which displays a full list of departure/arrival airports and their average delays
            //Console.WriteLine("***\nHere are the results grouped by airports:");
            //for (int j = 0; j < airportsArrClean.Length; j++)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    {
            //        sb.Append("airport: " + airportsArrClean[j]);
            //        // Makes sure there is no case of divide 0
            //        if (delaySum[j] != 0)
            //        {
            //            sb.Append(" had an average delay of: " + String.Format("{0:F1}", (Convert.ToDouble(delaySum[j]) / flightsPerAirport[j])));
            //        }

            //        // ´Makes sure there is a message for cases with 0 delay
            //        else
            //        {
            //            sb.Append(" had an average delay of: 0");
            //        }
            //    }
            //    Console.WriteLine(sb);
            //}

            // Creates an array for the average delays of each airport
            double[] averageDelay = new double[airportsArrClean.Length];

            for (int k = 0; k < airportsArrClean.Length; k++)
            {
                averageDelay[k] = (Convert.ToDouble(delaySum[k]) / flightsPerAirport[k]);
            }

            /* 
            Sorting the array using Array.Sort to sort two arrays in an order determined 
            by the first-mentioned(called the key) array
            */
            Array.Sort(averageDelay, airportsArrClean);

            // Prints out worst airports, some nicer formatting would be cool (printing in columns)
            Console.WriteLine("WORST AIRPORTS:");

            for (int l = airportsArrClean.Length - 1; l > airportsArrClean.Length - 4; l--)
            {
                StringBuilder sbWorstAirports = new StringBuilder();
                sbWorstAirports.Append(airportsArrClean[l]);
                sbWorstAirports.Append(": average delay " + String.Format("{0:F1}", (averageDelay[l])) + " minutes.");
                Console.WriteLine(sbWorstAirports);
            }

            // Prints out the best airports

            Console.WriteLine("BEST AIRPORTS:");

            for (int m = 0; m < 3; m++)
            {
                StringBuilder sbBestAirports = new StringBuilder();
                sbBestAirports.Append(airportsArrClean[m]);
                sbBestAirports.Append(": average delay " + String.Format("{0:F1}", (averageDelay[m])) + " minutes.");
                Console.WriteLine(sbBestAirports);
            }
            Console.WriteLine("(A negative value means the flight arrived early!)\n***");
        }

        static void Main(string[] args)
        {
            // Just another way to declare empty (but not null) variables, I could have simply used string origin = ""; 
            string origin = string.Empty;
            string destination = string.Empty;
            int airportTypeIndex = 1;

            // The following four lines get input from the user. I put two line breaks \n in there, for readability.
            Console.WriteLine("Please enter departure airport, for example: \"New York NY\" and press Enter.");
            origin = Console.ReadLine();
            Console.WriteLine("Please enter arrival airport, for example: \"Los Angeles CA\" and press Enter.");
            destination = Console.ReadLine();

            /*
            Counts all lines that contain origin and destination as picked by the user AND calculates average 
            delay for his connection
            */
            CountRelevantLines(origin, destination);

            /* To avoid sudden wall of text, this workaround method lets the user decide whether to display more info.
            I'd rather have an option to display text in the console page by page, as output length may vary depending 
            on csv file used.
            */
            if (!DisplayMoreInfo())
                Environment.Exit(1);

            /*
            This method counts all airlines that offered flights from the origin to destination picked 
            by the user and prints the number. As of now, it then also calls the method
            DelayPerAirline(origin, destination, airlineCounter, relAirlines); 
            It's probably not very good style to create chains of methods that call each other(?) 
            but done because DelayPerAirline() uses a variable from CountRelevantAirlines() that isn't public
            and thus can't be called from the Main.
            */
            CountRelevantAirlines(origin, destination);

            if (!DisplayMoreInfo())
                Environment.Exit(1);

            /*
            Exciting, I'm called a method and putting in another method as a parameter and it worked.
            The RankAirports method below in turn calls the method DelaySumPerAirport, probably bad style?
            But same as above, to call DelaySumPerAirport from here I'd have to introduce its parameters to the Main, 
            making it more cluttered.
            airportTypeIndex runs my methods for either departure airports (Index 1 in the csv lines) or arrival airports (2)
            */
            Console.WriteLine("\n***\nThe top three worst/best airports to fly from (departure airports):");
            RankAirports(AirportChangeCounter(airportTypeIndex), airportTypeIndex);
            airportTypeIndex = 2;
            Console.WriteLine("***\nThe top three worst/best airports to fly to (arrival airports):");
            RankAirports(AirportChangeCounter(airportTypeIndex), airportTypeIndex);

            Console.ReadLine();
        }
    }
}

