﻿using System;
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

namespace SheCodesMod8Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtBx_YourNameHere.SelectAll();
            txtBx_YourNameHere.Focus();
        }

        private void btnGreetMe_Click(object sender, RoutedEventArgs e)
        {
            PasswordBoxWindow passwordWindow = new PasswordBoxWindow();
            passwordWindow.Owner = this;
            passwordWindow.AppMainWindow = this;
            passwordWindow.ShowDialog();          
        }

        public void displayGreeting()
        {
            lblGreeting.Content = string.Format("You go, {0}!", UserName);
        }

         public string UserName
        {
            get { return txtBx_YourNameHere.Text; }
        }
    }
}