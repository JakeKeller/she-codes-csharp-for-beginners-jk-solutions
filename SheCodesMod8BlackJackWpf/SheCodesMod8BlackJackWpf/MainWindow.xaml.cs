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

            string appFolderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string resourcesFolderPath = System.IO.Path.Combine(System.IO.Directory.GetParent(appFolderPath).Parent.FullName, "Resources\\classic-cards\\");

            var TestImage = new Image();
            var TestImageFile = new BitmapImage();
            TestImageFile.BeginInit();
            TestImageFile.UriSource = new Uri(String.Format(resourcesFolderPath + "52.png"));
            TestImageFile.EndInit();
            TestImage.Source = TestImageFile;

            var TestImage2 = new Image();
            //TestImage2.Margin = new Thickness(30);


            //var TestImageFile2 = new BitmapImage();
            //TestImageFile2.BeginInit();
            //TestImageFile2.UriSource = new Uri(String.Format(resourcesFolderPath + "4.png"));
            //TestImageFile2.EndInit();
            //TestImage2.Source = TestImageFile2;
            //TestImage2.Margin = new Thickness(60, 0, 0, 0);          

            CnvMyDeck.Children.Add(TestImage2);

        }

        private async void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            bool firstCard = true;
            Game newGame = new Game();
            TxtBlGameMessages.Text = gameMessages.StartMessage;
            newGame.ComputerDrawsCard(gameMessages, firstCard);
            await Task.Delay(1000); // Pausing 1 second before next line is executed. See: http://stackoverflow.com/questions/15599884/how-to-put-delay-before-doing-an-operation-in-wpf
            newGame.ComputerDrawsCard(gameMessages);
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            TxtBlGameMessages.Text = gameMessages.GameRules;
        }

        
    }
}
