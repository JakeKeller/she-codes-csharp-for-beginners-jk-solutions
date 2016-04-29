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
    public class Game
    {
        public bool UserWon { get; set; }
        public bool ComputerWon { get; set; }
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }
        public Deck Deck { get; set; }
        public MainWindow mainWin { get; set; }
        public int NumberOfCardsDrawnByUser { get; set; }
        public int NumberOfCardsDrawnByComputer { get; set; }
        public bool GameOverFlag { get; set; }

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

            if (UserWon | ComputerWon)
                return;

            Card drawnCard = Deck.DrawCard();
            this.NumberOfCardsDrawnByComputer++;
            this.ComputerScore += drawnCard.GetValue();
            mainWin.TxbBaeBotScore.Text = this.ComputerScore.ToString();
            
            mainWin.GrdBaesDeck.Children.Add(drawnCard.GetImage(firstCard, NumberOfCardsDrawnByComputer));

            mainWin.TxtBlGameMessages.Text =  String.Format("BAE-BOT drew: \"{0}\". His current score is: {1}.", drawnCard.GetFace(), this.ComputerScore);


            //mainWin.TxtBlGameMessages.Text = drawnCard.GetImage();

            if (ComputerScore == 21)
            {
                this.ComputerWon = true; // Why do I still have this property?
                mainWin.TxtBlGameMessages.Text = gameMessages.BlackJackBanner;
                mainWin.TxtBlGameMessages.Text += gameMessages.RandomComputerWonMessage(Deck);
                GameOver(UserWon);
            }

            if (ComputerScore > 21)
            {
                this.UserWon = true;
                mainWin.TxtBlGameMessages.Text = gameMessages.BustBanner;
                mainWin.TxtBlGameMessages.Text += gameMessages.RandomUserWonMessage(Deck);
                GameOver(UserWon);
            }
            else
            {
                mainWin.BtnPass.IsEnabled = true;
                mainWin.BtnDraw.IsEnabled = true;
            }
        }

        public void UserDrawsCard(Messages gameMessages, bool firstCard = false)
        {
            if (UserWon | ComputerWon)
                return;

            Card drawnCard = Deck.DrawCard();
            this.NumberOfCardsDrawnByUser++;
            this.UserScore += drawnCard.GetValue();
            mainWin.TxbUserScore.Text = this.UserScore.ToString();
            mainWin.GrdMyDeck.Children.Add(drawnCard.GetImage(firstCard, NumberOfCardsDrawnByUser));
            mainWin.TxtBlGameMessages.Text = String.Format("You drew: \"{0}\". Your current score is: {1}.", drawnCard.GetFace(), this.UserScore);

            if (UserScore == 21)
            {
                mainWin.TxtBlGameMessages.Text = (gameMessages.BlackJackBanner);
                this.UserWon = true;
                Console.WriteLine(gameMessages.RandomUserWonMessage(Deck));
                GameOver(UserWon);
            }

            if (UserScore > 21)
            {
                mainWin.TxtBlGameMessages.Text = (gameMessages.BustBanner);
                this.ComputerWon = true;
                Console.WriteLine(gameMessages.RandomComputerWonMessage(Deck));
                GameOver(UserWon);
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
                this.UserWon = true;
                GameOver(UserWon);
            }
        }

        public void CheckIfComputerIsCloserTo21(Messages gameMessages)
        {
            if (ComputerScore > UserScore)
            {
                Console.WriteLine(gameMessages.ComputerCloserTo21WinMessage);
                GameOver(UserWon);
            }
        }
        public void GameOver(bool userWon)
        {
            string gameOverMessageBoxCaption = "Bae Bot won!";
            if (userWon)
            {
                gameOverMessageBoxCaption = "You won!";
            }            

            MessageBoxResult result = MessageBox.Show("Would you like to restart the Game?", gameOverMessageBoxCaption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                mainWin.TxtBlGameMessages.Text = "Please start a new Game!";
                mainWin.TxbBaeBotScore.Text = null;
                mainWin.TxbUserScore.Text = null;
                UserScore = 0;
                ComputerScore = 15;
                mainWin.GrdBaesDeck.Children.Clear();
                mainWin.GrdMyDeck.Children.Clear();
                mainWin.BtnStartGame.IsEnabled = true;
                //this.UserWon = false;
                //this.ComputerWon = false;
                this.NumberOfCardsDrawnByUser = 0;
                this.NumberOfCardsDrawnByComputer = 0;
            }
            else
                mainWin.Close();
        }
    }
}
