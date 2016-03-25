using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod7WordCount
{
    class Program
    {
        static void Main(string[] args)
        {
            const string textfileName = "pg219.txt";
            Dictionary<string, int> wordFrequencies = WordFrequencies(AllWords(textfileName));

            var mostfrequent100words = (from pair in wordFrequencies
                                        orderby pair.Value descending
                                        select pair).Take(100);
            foreach (var pair in mostfrequent100words)
                // Used custom string formatting for a nicer looking output to the console.
                Console.WriteLine("{0:-30} : {1}", pair.Key, pair.Value);

            Console.ReadLine();
        }
        static List<string> AllWords(string textfileName)
        {
            List<string> wordList = new List<string>();
            StreamReader streamReader = new StreamReader(textfileName);
            string line = string.Empty;

            /* 
            Trying to make streamReader skip the lines before the actual book text begins:
            The following didn't work as ReadLine only reads in one line at a time and .Skip skips the 
            given number of items in a sequence (in this case there was only one thing/line in the sequence.
            */
            // streamReader.ReadLine().Skip(19); 

            /* 
            The following works, but you have to count the lines per hand and feed them in
            */
            // Skips 19 lines at the beginning:
            //for (int i = 0; i < 19; i++)
            //{
            //    streamReader.ReadLine().Skip(1);
            //}

            // This works:
            while ((line = streamReader.ReadLine()) != null)
            {
                if (line.Contains("*** START OF THIS PROJECT GUTENBERG EBOOK"))
                {
                    streamReader.ReadLine().Skip(1);
                    break;
                }
                break;
            }

            int lineCounter = 0;
            while (((line = streamReader.ReadLine()) != null)
                && !(line.Contains("*** END OF THIS PROJECT GUTENBERG EBOOK")))
            {
                lineCounter++;
                /*
                string[] wordsInLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries); will not work, 
                as overloads of the .Split() method will not take a  simple char or string but only an array.
                According to Stack Overflow, I have to resort to: 
                */
                string[] wordsInLine = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                /* 
                Will a foreach loop to trim all punctuation characters within the for loop below
                radically slow things down? It did, 
                */

            char[] punctuation = new char[] { '!', '?', '.', ',',':', ';','(',')','/','-','"','[',']','{','}','\''};
                for (int i = 0; i < wordsInLine.Length; i++)
                {
                    wordsInLine[i] = wordsInLine[i].ToLowerInvariant();
                 
                    // The following turns "You!" to "you" but leaves "--well" at "-well" :(
                    foreach (char punctuationChar in punctuation)
                    {
                        wordsInLine[i] = wordsInLine[i].Trim(punctuationChar);
                    }

                    // The following would turn words like "other's" into "other", but also "I've" to "I" and don't to don :(.
                    //if (wordsInLine[i].Contains('\''))
                    //{
                    //    int index = wordsInLine[i].IndexOf('\'');
                    //    wordsInLine[i].Remove(index);
                    //}

                    wordList.Add(wordsInLine[i]);
                }
            }
            streamReader.Close();
            Console.WriteLine("Lines counted: " + lineCounter);
            return wordList;

        }
        static Dictionary<string, int> WordFrequencies(List<string> words)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int frequency = 1;
            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                    dict.Add(word, frequency);
                else if (dict.ContainsKey(word))
                    dict[word]++;
            }
            return dict;
        }
    }
}
