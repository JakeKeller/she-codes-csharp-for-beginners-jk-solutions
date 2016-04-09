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
using System.Windows.Shapes;

namespace SheCodesMod8Question2
{
    /// <summary>
    /// Interaction logic for PasswordBoxWindow.xaml
    /// </summary>
    public partial class PasswordBoxWindow : Window
    {
        public PasswordBoxWindow()
        {
            InitializeComponent();
        }

        public MainWindow AppMainWindow { get; set; }

        private void passwordOK_Click(object sender, RoutedEventArgs e)
        {
            if (passWordBox.Password == "shecodes")
            {
                AppMainWindow.displayGreeting();
                this.Close();
            }
            else
                MessageBox.Show("Wrong password, try again!");
        }
    }
}
