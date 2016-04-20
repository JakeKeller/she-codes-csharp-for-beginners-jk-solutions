using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SheCodesMod8BlackJackWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Messages gameMessages { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            gameMessages = new Messages();
            TxtBlGameMessages.Text = gameMessages.WelcomeMessage;

            //TxtBlGameMessages.Text = gameMessages.WelcomeMessage;
            //TxtBlGameMessages.Text = gameMessages.WelcomeMessage;
            // Do I programmatically add images in the code-behind like this?
            // https://arcanecode.com/2007/09/07/adding-wpf-controls-progrrammatically/
            // Image image = new Image();
            // image.Source = new BitmapImage(new Uri(@"\Path..."))
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            Game newGame = new Game();
            TxtBlGameMessages.Text += gameMessages.StartMessage;
            newGame.ComputerDrawsCard(gameMessages);
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            TxtBlGameMessages.Text += gameMessages.GameRules;
        }
    }
}
