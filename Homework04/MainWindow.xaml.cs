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
using Homework04.Models;

namespace Homework04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Zipcode zpcode = new Zipcode();
        //private Models.Zipcode zpcode = new Models.Zipcode();

        public MainWindow()
        {
            InitializeComponent();
        }


        public override void EndInit()
        {
            base.EndInit();
            uxZpcode.DataContext = zpcode;
            uxZpcodeError.DataContext = zpcode;
        }

        private void OnNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New command");
        }

        private void OnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Click Ctrl-N to execute the shortcut.
        private void OnNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Set this to false if the New command is not available
            e.CanExecute = true;
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Your Zipcode or Postal-Code is\n\n {uxZpcode.Text.ToUpper()}", "Zip Code - Postal Code");
        }

        private void ZipCodeChanged(object sender, TextChangedEventArgs e)
        {
            uxSubmit.IsEnabled = zpcode.ZipCodeIsCorrect;
        }

        private void EnableDisableSubmitButton()
        {
            ////string password = uxPassword.Text;
            string name = uxZpcode.Text;
            uxSubmit.IsEnabled = !string.IsNullOrEmpty(name);
        }
    }
}
