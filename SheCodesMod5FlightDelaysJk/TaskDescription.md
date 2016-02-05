## Lab
_In this lab, you will help yourself and your friends plan a vacation in the United States in 
September, during our holidays. As you probably know, flights within the United States are very 
often delayed, and knowing in advance about expected delays can help plan your trip 
accordingly_

Create a Console application. Write code that asks the user for the origin and destination 
airports for their September flight. 

Write a helper method with the following signature: 

static void PrintAverageDelay(string origin, string destination) 

This method should open the .csv file with the flight performance information, and look for all 
records matching the origin and destination airports specified by the user. It should calculate the 
number of such records and the sum of the arrival delays, in minutes, and display the average ­­ 
which is the sum of arrival delays divided by the number of records. If there were no flights 
between the two airports, display an appropriate message

You can reuse the code you wrote in previous labs for reading lines from the .csv file. However, 
this time you need to parse the line to extract specific parts from it. To split a line of values 
separated by commas (,) into its parts, use the S​plit​ method, as follows: 
 
string[] parts = line.Split( 
         new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); 

Note that some rows might be invalid, and have fewer than seven parts. You should check that 
right after splitting, and simply ignore these lines. 

Here are some ideas for improvement if you have spare time: 

● Display the maximum delay in addition to the average.

● Some people say not all airlines are equally bad. Display the results grouped by airline, 
  so you can make more informed decisions.
  
● Determine which is the worst airport to fly from, and which is the worst airport to fly to. 

Source:
https://github.com/MicrosoftLearning/CSharpForAbsBeginners.git
