using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
        public bool GameOverByDraw { get; set; }
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

        public async void ComputerDrawsCard(Messages gameMessages, bool firstCard = false)
        {

            if (UserWon | ComputerWon)
                return;

            Card drawnCard = Deck.DrawCard();
            this.NumberOfCardsDrawnByComputer++;
            this.ComputerScore += drawnCard.GetValue();
            mainWin.TxbBaeBotScore.Text = this.ComputerScore.ToString();
            //drawnCard.DisplayCardCover(firstCard, NumberOfCardsDrawnByComputer); // Displays backside of card.
            mainWin.GrdBaesDeck.Children.Add(drawnCard.DisplayCardCover(firstCard, NumberOfCardsDrawnByComputer));
            await Task.Delay(150);
            ////Thread.Sleep(200);
            mainWin.GrdBaesDeck.Children.Add(drawnCard.GetImage(firstCard, NumberOfCardsDrawnByComputer));

            if (ComputerScore == 21)
            {
                this.ComputerWon = true;
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
        }

        public async void UserDrawsCard(Messages gameMessages, bool firstCard = false)
        {
            if (UserWon | ComputerWon)
                return;

            Card drawnCard = Deck.DrawCard();
            this.NumberOfCardsDrawnByUser++;
            this.UserScore += drawnCard.GetValue();
            mainWin.TxbUserScore.Text = this.UserScore.ToString();
            //drawnCard.DisplayCardCover(firstCard, NumberOfCardsDrawnByUser); // Displays backside of card.
            mainWin.GrdMyDeck.Children.Add(drawnCard.DisplayCardCover(firstCard, NumberOfCardsDrawnByUser));
            await Task.Delay(150);
            mainWin.GrdMyDeck.Children.Add(drawnCard.GetImage(firstCard, NumberOfCardsDrawnByUser));
            //drawnCard.DisplayCardCover(firstCard, NumberOfCardsDrawnByUser); // Displays backside of card.
            //await Task.Delay(100);
            //mainWin.GrdMyDeck.Children.Add(drawnCard.GetImage(firstCard, NumberOfCardsDrawnByUser));
            //mainWin.TxtBlGameMessages.Text = String.Format("You drew: \"{0}\". Your current score is: {1}.", drawnCard.GetFace(), this.UserScore);

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
            if (UserWon | ComputerWon)
            {
                return;
            }
            mainWin.TxtBlGameMessages.Text = ("\nBAE BOT is making his move.");

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
            mainWin.TxtBlGameMessages.Text = (gameMessages.ComputerStandsMessage);
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
                mainWin.TxtBlGameMessages.Text = (gameMessages.ComputerCloserTo21WinMessage);
                GameOver(UserWon);
            }
            if (ComputerScore == UserScore)
            {
                mainWin.TxtBlGameMessages.Text = (gameMessages.DrawBanner);
                this.GameOverByDraw = true;
                GameOver(UserWon, GameOverByDraw);
            }
                
            
        }
        public void GameOver(bool userWon, bool gameOverByDraw = false)
        {
            string gameOverMessageBoxCaption;
            if (gameOverByDraw)
                gameOverMessageBoxCaption = "It's a draw!";
            else if (userWon)
                gameOverMessageBoxCaption = "You won!";
            else
            gameOverMessageBoxCaption = "Bae Bot won!";

            MessageBoxResult result = MessageBox.Show("Would you like to restart the Game?", gameOverMessageBoxCaption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                mainWin.TxtBlGameMessages.Text = "Please start a new Game!";
                mainWin.TxbBaeBotScore.Text = "0";
                mainWin.TxbUserScore.Text = "0";
                UserScore = 0;
                ComputerScore = 0;
                mainWin.GrdBaesDeck.Children.Clear();
                mainWin.GrdMyDeck.Children.Clear();
                mainWin.BtnStartGame.IsEnabled = true;
                this.NumberOfCardsDrawnByUser = 0;
                this.NumberOfCardsDrawnByComputer = 0;
                mainWin.BtnDraw.IsEnabled = false;
                mainWin.BtnPass.IsEnabled = false;
                userWon = false;
                ComputerWon = false;
                GameOverByDraw = false;
            }
            else
                mainWin.Close();
        }
    }
}
