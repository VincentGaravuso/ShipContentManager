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
using System.Windows.Media.Animation;
using System.Threading;
using System.Reflection;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool hamburgerMenuOpened = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHamburger_Click(object sender, RoutedEventArgs e)
        {
            if (hamburgerMenuOpened)
            {
                ShowHideMenu("sbHideHamburgerMenu", btnPacks, pnlLeftMenu);
                hamburgerMenuOpened = false;
            }
            else
            {
                ShowHideMenu("sbShowHamburgerMenu", btnPacks, pnlLeftMenu);
                hamburgerMenuOpened = true;
            }
        }
        private void ShowHideMenu(string Storyboard, Button btnPacks, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnPacks.Visibility = System.Windows.Visibility.Visible;
                btnQuestions.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnPacks.Visibility = Visibility.Hidden;
                btnQuestions.Visibility = Visibility.Hidden;
            }
        }

        private void btnPacks_Click(object sender, RoutedEventArgs e)
        {
            generatePacks();
        }

        private void btnQuestions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //On start - show packs screen
            generatePacks();
        }

        private void generatePacks()
        {
            int numOfColumns = getNumberOfColumns();
            int rowNumber = 0;
            int columnNumber = 0;

            //Clear previous pack content
            packContentGrid.ColumnDefinitions.Clear();
            packContentGrid.RowDefinitions.Clear();
            packContentGrid.Children.Clear();

            //TODO: Pull packs from DB

            //Dynamically create packs and assign data
            for (int i = 0; i < 15; i++)
            {
                PacksUserControl packsUserControl = new PacksUserControl();
                packsUserControl.lblPackName.Content = $"Pack {i + 1}";
                packsUserControl.lblDateCreated.Content = DateTime.Now.ToString("dd/MM/yyyy");
                packsUserControl.Margin = new Thickness(20, 20, 0, 0);
                if (columnNumber == numOfColumns)
                {
                    columnNumber = 0;
                    rowNumber++;
                }
                RowDefinition rowDefinition = new RowDefinition();
                packContentGrid.RowDefinitions.Add(rowDefinition);
                rowDefinition.Height = GridLength.Auto;
                Grid.SetRow(packsUserControl, rowNumber);
                ColumnDefinition columnDefinition = new ColumnDefinition();
                packContentGrid.ColumnDefinitions.Add(columnDefinition);
                columnDefinition.Width = GridLength.Auto;
                Grid.SetColumn(packsUserControl, columnNumber);

                packContentGrid.Children.Add(packsUserControl);
                columnNumber++;
            }
        }
        private int getNumberOfColumns()
        {
            var mainGridWidth = mainGrid.ActualWidth;
            var numberOfColumns = Convert.ToInt32(mainGridWidth / 190);
            return numberOfColumns;
        }

        private void SlideTrayGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            btnHamburger_Click(sender, e);
        }
    }
}

