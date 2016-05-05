using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace SheCodesMod8BlackJackWpf
{

    /// <summary>
    /// Handles and verifies all possible cards in the deck, calculates their values, provides the image etc.
    /// </summary>
    public class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int ImageIdentifier { get; set; }
        public MainWindow mainWin { get; set; }

        public Card(string suit, string rank)
        {
            this.Suit = suit;
            this.Rank = rank;
            VerifySuitOrRank(suit, rank);
            mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
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
        }

        public string GetFace()
        {
            string face = string.Format("{0} of {1}",
                this.Rank,
                this.Suit);

            return face;
        }

        public Image DisplayCardCover(bool firstCard, int numberOfCardsDrawnByPlayer) 
        {
            string appFolderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string resourcesFolderPath = System.IO.Path.Combine(System.IO.Directory.GetParent(appFolderPath).Parent.FullName, "Resources\\classic-cards\\");
            string imageFileExtension = ".png";
            string imageUri = string.Format(resourcesFolderPath + "b2fv" + imageFileExtension);
            int imageOffset = ((numberOfCardsDrawnByPlayer - 1) * 60); // This is part of a messy workaround in order to get the images to overlap correctly.

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(imageUri));
            if (firstCard)
            {
                image.Margin = new System.Windows.Thickness(0, 0, 0, 0);
            }
            else
            {
                image.Margin = new System.Windows.Thickness(imageOffset, 0, 0, 0);
            }
            return image;
        }

        public Image GetImage(bool firstCard, int numberOfCardsDrawnByPlayer)
        {

            string appFolderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string resourcesFolderPath = System.IO.Path.Combine(System.IO.Directory.GetParent(appFolderPath).Parent.FullName, "Resources\\classic-cards\\");
            string imageFileExtension = ".png";
            string imageUri = string.Format(resourcesFolderPath + this.ImageIdentifier + imageFileExtension);
            int imageOffset = ((numberOfCardsDrawnByPlayer - 1) * 60); // This is part of a messy workaround in order to get the images to overlap correctly.

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(imageUri));
            if (firstCard)
            {
                image.Margin = new System.Windows.Thickness(0, 0, 0, 0);
            }
            else
            {
                image.Margin = new System.Windows.Thickness(imageOffset, 0, 0, 0);
            }

            return image;

        }

    }
}

