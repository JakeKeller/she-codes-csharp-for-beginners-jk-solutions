using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SheCodesMod8BlackJackWpf
{
    /// <summary>
    /// Handles computer and player moves ("decision making") and checking the scores.
    /// </summary>
    class Game
    {
        public bool UserWon { get; set; }
        public bool ComputerWon { get; set; }
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }
        public Deck Deck { get; set; }
        public MainWindow mainWin { get; set; }

        public Game()
        {
            this.UserWon = false;
            this.ComputerWon = false;
            this.UserScore = 0;
            this.ComputerScore = 0;

            this.Deck = new Deck();

            mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        }

        /* 
        Note: I wonder why shecodes didn't suggest making a player class or a class for each player.
        Also, if we had "drawn" as a property of a card, we could handle the values of that card more easily here, like a "flag".
        Also, should I make just one method called move rather than one for each player?
        Make as many methods universal as possible and leave the specificity in the messages?
        But the more I do that, the more I have to "pass around" parameters calling methods from classes all over 
        */

        public void ComputerDrawsCard(Messages gameMessages, bool firstCard = false)
        {
            Card drawnCard = Deck.DrawCard();
            this.ComputerScore += drawnCard.GetValue();
            mainWin.TxbBaeBotScore.Text = this.ComputerScore.ToString();
            
            mainWin.GrdBaesDeck.Children.Add(drawnCard.GetImage(firstCard));

            mainWin.TxtBlGameMessages.Text =  String.Format("BAE-BOT drew: \"{0}\". His current score is: {1}.", drawnCard.GetFace(), this.ComputerScore);


            //mainWin.TxtBlGameMessages.Text = drawnCard.GetImage();

            if (ComputerScore == 21)
            {
                Console.WriteLine(gameMessages.BlackJackBanner);
                this.ComputerWon = true; // Why do I still have this?
                mainWin.TxtBlGameMessages.Text = gameMessages.BlackJackBanner;
                mainWin.TxtBlGameMessages.Text += gameMessages.RandomComputerWonMessage(Deck);
                QuitGame(); // Make this display a pop up window as required... and make start game clear the deck I guess?
            }

            if (ComputerScore > 21)
            {
                this.UserWon = true;
                mainWin.TxtBlGameMessages.Text = gameMessages.BlackJackBanner;
                mainWin.TxtBlGameMessages.Text += gameMessages.RandomUserWonMessage(Deck);
                QuitGame();
            }
        }

        public void UserDrawsCard(Messages gameMessages, bool firstCard = false)
        {
            Card drawnCard = Deck.DrawCard();
            this.UserScore += drawnCard.GetValue();
            mainWin.TxbUserScore.Text = this.UserScore.ToString();
            mainWin.GrdMyDeck.Children.Add(drawnCard.GetImage(firstCard));
            mainWin.TxtBlGameMessages.Text = String.Format("You drew: \"{0}\". Your current score is: {1}.", drawnCard.GetFace(), this.UserScore);

            if (UserScore == 21)
            {
                Console.WriteLine(gameMessages.BlackJackBanner);
                this.UserWon = true;
                Console.WriteLine(gameMessages.RandomUserWonMessage(Deck));
                QuitGame();
            }

            if (UserScore > 21)
            {
                Console.WriteLine(gameMessages.BustBanner);
                this.ComputerWon = true;
                Console.WriteLine(gameMessages.RandomComputerWonMessage(Deck));
                QuitGame();
            }
        }

        public void UserDrawDecision(Messages gameMessages)
        {
            Console.WriteLine("\nMake Your Choice: Press Enter to draw a card \n(or press \"S\" and Enter to \"stand\".)");
            if (Console.ReadLine().ToUpperInvariant() == "S")
            {
                Console.WriteLine(gameMessages.UserStandsMessage);
                this.CheckIfUserIsCloserTo21(gameMessages);
            }
            else
                this.UserDrawsCard(gameMessages);
        }

        public void ComputerDrawDecision(Messages gameMessages)
        {
            Console.WriteLine("\nBAE BOT is making his move.");

            int randomNumber = Deck.RandomGenerator.Next(0, 2);

            if (UserScore == 20)
                this.ComputerDrawsCard(gameMessages);
            else if (ComputerScore < 18)
                this.ComputerDrawsCard(gameMessages);
            else if (ComputerScore < 18 && ComputerScore > 15 && randomNumber == 1)
                this.ComputerDrawsCard(gameMessages);
            else if (ComputerScore == UserScore)
                this.ComputerDrawsCard(gameMessages);
            else
                this.ComputerStands(gameMessages);
        }

        public void ComputerStands(Messages gameMessages)
        {
            Console.WriteLine(gameMessages.ComputerStandsMessage);
            this.CheckIfComputerIsCloserTo21(gameMessages);
        }

        public void CheckIfUserIsCloserTo21(Messages gameMessages)
        {
            if (UserScore > ComputerScore)
            {
                Console.WriteLine(gameMessages.UserCloserTo21WinMessage);
                QuitGame();
            }
        }

        public void CheckIfComputerIsCloserTo21(Messages gameMessages)
        {
            if (ComputerScore > UserScore)
            {
                Console.WriteLine(gameMessages.ComputerCloserTo21WinMessage);
                QuitGame();
            }
        }
        public void QuitGame()
        {
            //MessageBox MsbQuitGame = new MessageBox;
            //mainWin.GrdMainGrid.Children.Add(var MsbQuitGame = new MessageBox());
            //Console.ReadLine();
            //Environment.Exit(1);

            MessageBoxResult result = MessageBox.Show("Would you like to restart the Game?", "Game Over!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                mainWin.TxtBlGameMessages.Text = "Please start a new Game!"; // is there a "clear this control (property) method?
                mainWin.TxbBaeBotScore.Text = null;
                mainWin.TxbUserScore.Text = null;
                mainWin.GrdBaesDeck.Children.Clear();
                mainWin.GrdMyDeck.Children.Clear();
            }
            else
                mainWin.Close();
        }
    }
}
