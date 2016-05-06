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
    /// Handles and verifies all possible cards in the deck. Includes methods to calculate a card's value, provide a card's image, etc.
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
        /// <summary>
        /// This method provides an image of the backside of a card for "animation purposes".
        /// </summary>
        /// <param name="firstCard"></param>
        /// <param name="numberOfCardsDrawnByPlayer"></param>
        /// <returns></returns>
        public Image ProvideCardCoverImage(bool firstCard, int numberOfCardsDrawnByPlayer) 
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

        public Image GetCardImage(bool firstCard, int numberOfCardsDrawnByPlayer)
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

