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

namespace Homework05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int counter = 0;
        static string[,] grid = null;


        public MainWindow()
        {
            InitializeComponent();
            reset();
        }

        // reset the tic-tac-toe grid 
        // to empty new
        private void reset()
        {
            grid = new string[,] { { "", "", "" }, { "", "", "" }, { "", "", "" } };

            uxTurn.Text = "X's turn";
            uxGrid.IsEnabled = true;

            counter = 1;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject control) where T : DependencyObject
        {
            if (control != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(control, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        // 
        // check if tic-tac-toe entries have winners
        private bool CheckGameEntries()
        {
            if (!string.IsNullOrEmpty(grid[0, 0]) && !string.IsNullOrEmpty(grid[1, 1]) &&
                !string.IsNullOrEmpty(grid[2, 2]) && grid[0, 0] == grid[1, 1] &&
                grid[1, 1] == grid[2, 2])
            {
                uxTurn.Text = grid[2, 2] + " is a Winner";
                return true;
            }
            else if (!string.IsNullOrEmpty(grid[0, 2]) && !string.IsNullOrEmpty(grid[1, 1]) &&
                     !string.IsNullOrEmpty(grid[2, 0]) && grid[0, 2] == grid[1, 1] &&
                      grid[1, 1] == grid[2, 0])
            {
                uxTurn.Text = grid[2, 0] + " is a Winner";
                return true;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    string row = (!string.IsNullOrEmpty(grid[i, 0])) ? grid[i, 0] : "";
                    bool find = false;

                    for (int j = 1; j < 3; j++)
                    {
                        if (grid[i, j] == row && !find && !string.IsNullOrEmpty(grid[i, j]))
                        {
                            find = true;
                        }
                        else if (grid[i, j] == row && find && !string.IsNullOrEmpty(grid[i, j]))
                        {
                            uxTurn.Text = row + " is a Winner";
                            return find;
                        }
                        else
                        {
                            find = false;
                        }
                    }
                }

                for (int j = 0; j < 3; j++)
                {
                    string col = (!string.IsNullOrEmpty(grid[0, j])) ? grid[0, j] : "";

                    bool find = false;

                    for (int i = 1; i < 3; i++)
                    {
                        if (grid[i, j] == col && !find && !string.IsNullOrEmpty(grid[i, j]))
                        {
                            find = true;
                        }
                        else if (grid[i, j] == col && find && !string.IsNullOrEmpty(grid[i, j]))
                        {
                            uxTurn.Text = col + " is a Winner";
                            return find;
                        }
                        else
                        {
                            find = false;
                        }
                    }
                }
                return false;
            }
        }

        private bool Won(string win)
        {
            int[] found = { 0, 0, 0 };

            foreach (Button b in FindVisualChildren<Button>(uxGrid))
            {
                if (b.Content != null)
                {
                    int Row = int.Parse(b.Tag.ToString().Split(',')[0]);
                    int Col = int.Parse(b.Tag.ToString().Split(',')[1]);
                    grid[Row, Col] = b.Content.ToString();
                }
            }

            foreach (Button b in FindVisualChildren<Button>(uxGrid))
            {
                if (b.Content != null)
                {
                    int Row = int.Parse(b.Tag.ToString().Split(',')[0]);
                    int Col = int.Parse(b.Tag.ToString().Split(',')[1]);

                    if (Row == 0 || Col == 0)
                    {
                        return CheckGameEntries();
                    }
                }
            }

            return false;
        }

        // 
        // button-click event. check if the tic-tac-toe has winners 
        // everytime a button is clicked.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            string tic = (counter % 2 == 0) ? "X" : "O";
            string tac = (counter % 2 == 0) ? "O" : "X";

            if (button.Content == null)
            {
                button.Content = (counter % 2 == 0) ? "O" : "X";

                counter++;
                if (counter > 5)
                {
                    if (Won(tac))
                    {
                        uxGrid.IsEnabled = false;
                        return;
                    }
                }

                uxTurn.Text = tic + "'s turn";
            }
            else
            {
                MessageBox.Show("Please only click on empty spot!", "Illegal Spot!");
            }
        }

        private void OnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // start new game
        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button b in FindVisualChildren<Button>(uxGrid))
            {
                if (b.Content != null)
                {
                    b.Content = null;
                }
            }

            reset();
        }

        // Click Ctrl-N to execute the shortcut.
        private void OnNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Set this to false if the New command is not available
            e.CanExecute = true;
        }
    }
}
