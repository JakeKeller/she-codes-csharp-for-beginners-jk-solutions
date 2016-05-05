using System.Threading.Tasks;
using System.Windows;

namespace SheCodesMod8BlackJackWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Messages gameMessages { get; set; }
        public Game newGame { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            gameMessages = new Messages();
        }
        /// <summary>
        /// This method replaces part of the "Main method" of my non-wpf version of BlackJack.
        /// Async wasn't covered in the course. I snuck that in to enable Task.Delay, see below.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            // Bool firstCard exists because the first card's image needs a different margin to properly display.
            bool firstCard = true;

            StartGame();

            BtnStartGame.IsEnabled = false;
            newGame.ComputerWon = false;
            newGame.UserWon = false;
            newGame.ComputerDrawsCard(gameMessages, firstCard);
            /*
            Added a delay to enable player to read messages that would otherwise be 
            displayed too fast after each otehr and for the faintest hint of graphical "animation", e.g.
            when cards are displayed with a slight delay.
            See: http://stackoverflow.com/questions/15599884/how-to-put-delay-before-doing-an-operation-in-wpf
            */
            await Task.Delay(1000);
            newGame.ComputerDrawsCard(gameMessages);
            await Task.Delay(1000);
            newGame.UserDrawsCard(gameMessages, firstCard);
            await Task.Delay(1000);
            newGame.UserDrawsCard(gameMessages);
            BtnDraw.IsEnabled = true;
            BtnPass.IsEnabled = true;
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            TxtBlGameMessages.Text = gameMessages.GameRules;
            // Change this to a tool tip?
        }

        private async void BtnDraw_Click(object sender, RoutedEventArgs e)
        {
            newGame.UserDrawsCard(gameMessages);
            await Task.Delay(1000);
            newGame.ComputerDrawDecision(gameMessages);
        }

        private async void BtnPass_Click(object sender, RoutedEventArgs e)
        {
            TxtBlGameMessages.Text = gameMessages.UserStandsMessage;
            await Task.Delay(1000);
            newGame.CheckIfUserIsCloserTo21(gameMessages);
            newGame.CheckIfComputerIsCloserTo21(gameMessages);
        }

        public void StartGame()
        {
            newGame = new Game();
            TxtBlGameMessages.Text = gameMessages.WelcomeMessage;
            BtnDraw.IsEnabled = false;
            BtnPass.IsEnabled = false;
        }
    }
}
