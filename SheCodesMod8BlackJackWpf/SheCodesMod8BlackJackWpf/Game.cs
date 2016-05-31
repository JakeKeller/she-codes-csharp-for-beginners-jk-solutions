using System;
using System.Linq;
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
        public bool GameOverByDraw { get; set; }
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }
        public Deck Deck { get; set; }
        public MainWindow mainWin { get; set; }
        public int NumberOfCardsDrawnByUser { get; set; }
        public int NumberOfCardsDrawnByComputer { get; set; }
        public bool GameOverFlag { get; set; }
        public bool UserChoseToStand { get; set; }
        public bool ComputerChoseToStand { get; set; }

        public Game()
        {
            this.UserWon = false;
            this.ComputerWon = false;
            this.UserScore = 0;
            this.ComputerScore = 0;
            this.Deck = new Deck();
            // Changing window content from within another class, see: http://the--semicolon.blogspot.de/p/change-wpf-window-label-content-from.html 
            mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        }

        public async void ComputerDrawsCard(Messages gameMessages, bool firstCard = false)
        {

            if (UserWon | ComputerWon)
                return;

            Card drawnCard = Deck.DrawCard();
            this.NumberOfCardsDrawnByComputer++;
            this.ComputerScore += drawnCard.GetValue();
            mainWin.TxbComputerScore.Text = this.ComputerScore.ToString();
            drawnCard.ProvideCardCoverImage(firstCard, NumberOfCardsDrawnByComputer); // Displays backside of card.
            mainWin.GrdComputerDeck.Children.Add(drawnCard.ProvideCardCoverImage(firstCard, NumberOfCardsDrawnByComputer));
            await Task.Delay(150);
            mainWin.GrdComputerDeck.Children.Add(drawnCard.GetCardImage(firstCard, NumberOfCardsDrawnByComputer));

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
            mainWin.GrdMyDeck.Children.Add(drawnCard.ProvideCardCoverImage(firstCard, NumberOfCardsDrawnByUser));
            await Task.Delay(150);
            mainWin.GrdMyDeck.Children.Add(drawnCard.GetCardImage(firstCard, NumberOfCardsDrawnByUser));

            if (UserScore == 21)
            {
                mainWin.TxtBlGameMessages.Text = (gameMessages.BlackJackBanner);
                this.UserWon = true;
                GameOver(UserWon);
            }

            if (UserScore > 21)
            {
                mainWin.TxtBlGameMessages.Text = (gameMessages.BustBanner);
                this.ComputerWon = true;
                GameOver(UserWon);
            }
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
                GameOver(UserWon);
            }
            if (ComputerScore == UserScore)
            {
                mainWin.TxtBlGameMessages.Text = (gameMessages.DrawBanner);
                this.GameOverByDraw = true;
                GameOver(UserWon, GameOverByDraw);
            }       
        }

        /// <summary>
        /// Handles behavior when the game is lost/won or has ended by draw.
        /// Is needed to display messagebox and to call StartGame() in MainWindow.xaml.cs
        /// which resets the game and deck classes. It also clears the decks and score controls on the GUI.
        /// </summary>
        /// <param name="userWon"></param>
        /// <param name="gameOverByDraw"></param>
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
                mainWin.TxbComputerScore.Text = "0";
                mainWin.TxbUserScore.Text = "0";
                mainWin.GrdComputerDeck.Children.Clear();
                mainWin.GrdMyDeck.Children.Clear();
                mainWin.BtnStartGame.IsEnabled = true;
                this.NumberOfCardsDrawnByUser = 0;
                this.NumberOfCardsDrawnByComputer = 0;
                mainWin.BtnDraw.IsEnabled = false;
                mainWin.BtnStand.IsEnabled = false;
            }
            else
                mainWin.Close();
        }
    }
}
