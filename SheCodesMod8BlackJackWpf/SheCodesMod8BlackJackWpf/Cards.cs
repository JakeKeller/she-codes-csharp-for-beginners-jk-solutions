using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod8BlackJackWpf
{
    /// <summary>
    /// Handles and verifies all possible cards in the deck, calculates their values etc.
    /// </summary>
    public class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int ImageIdentifier { get; set; }

        public Card(string suit, string rank)
        {
            this.Suit = suit;
            this.Rank = rank;
            VerifySuitOrRank(suit, rank);
        }

        public static string[] ValidSuits()
        {
            string[] validSuits = new string[] { "Diamonds", "Hearts", "Spades", "Clubs" };
            return validSuits;
        }

        public static string[] ValidRanks()
        {
            string[] validRanks = new string[]
            {
                "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"
            };

            return validRanks;
        }
        
        public void VerifySuitOrRank(string suit, string rank)
        {
            // Now that I've created the Card array in Decks.cs I don't know why this method would still be needed!

            bool quit = false;
            if (ValidSuits().Contains(suit) == false)
            {
                Console.WriteLine("{0} is not a valid {1}.", suit, "suit");
                quit = true;
            }

            if (ValidRanks().Contains(rank) == false)
            {
                Console.WriteLine("{0} is not a valid {1}.", rank, "rank");
                quit = true;
            }
            if (quit)
            {
                Console.WriteLine("Program is terminating.");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        public int GetValue()
        {
            if (this.Rank == "Ace")
            {
                return 1;
            }
            if (this.Rank == "Queen" || this.Rank == "Jack")
            {
                return 10;
            }
            else if (this.Rank == "King")
            {
                return 13;
            }
            else
                return int.Parse(this.Rank);

            /* 
            Note: I wonder why shecodes doesn't suggest making value a property of the card class instead
            of a method.
            */
        }

        public string GetFace()
        {
            string face = string.Format("{0} of {1}",
                this.Rank,
                this.Suit);

            return face;
        }

        public string GetImage()
        {
            string applicationPath = @"C:\Users\Jake\Documents\Visual Studio 2015\Projects\she-codes-csharp-for-beginners-jk-solutions\SheCodesMod8BlackJackWpf\SheCodesMod8BlackJackWpf\Resources\classic-cards\";
            string imageFileExtension = ".png";
            string ImageUri = string.Format(applicationPath + this.ImageIdentifier + imageFileExtension);
            return ImageUri;
        }

    }
}

