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
        private List<Pack> storedPacks;
        private bool hamburgerMenuSwitch = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHamburger_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu();
        }
        private void ShowHideMenu()
        {
            Storyboard sb;

            if (hamburgerMenuSwitch)
            {
                sb = Resources["sbShowHamburgerMenu"] as Storyboard; 
                sb.Begin(pnlLeftMenu);
                btnPacks.Visibility = System.Windows.Visibility.Visible;
                btnQuestions.Visibility = System.Windows.Visibility.Visible;
                hamburgerMenuSwitch = false;
            }
            else
            {
                sb = Resources["sbHideHamburgerMenu"] as Storyboard;
                sb.Begin(pnlLeftMenu);
                btnPacks.Visibility = Visibility.Hidden;
                btnQuestions.Visibility = Visibility.Hidden;
                hamburgerMenuSwitch = true;
            }
        }

        private void btnPacks_Click(object sender, RoutedEventArgs e)
        {
            //Query Db for packs and store to Global list
            storedPacks = queryForPacks();
            generatePacks(storedPacks);
        }

        private void btnQuestions_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //On start - show packs screen
            if(storedPacks != null)
            {
                generatePacks(storedPacks);
            }
            else
            {
                //Query Db for packs and store to Global list
                storedPacks = queryForPacks();
                generatePacks(storedPacks);
            }
        }
        private List<Pack> queryForPacks()
        {
            //Querying database for list
            List<Pack> packs = new List<Pack>();
            for (int i = 0; i < 15; i++)
            {
                Pack p = new Pack();
                p.Name = $"Pack {i}";
                p.IsMiniPack = true;
                packs.Add(p);
            }
            return packs;
        }
        private void generatePacks(List<Pack> packs)
        {
            int numOfColumns = getNumberOfColumns();
            int rowNumber = 0;
            int columnNumber = 0;

            //Clear previous pack content
            packContentGrid.ColumnDefinitions.Clear();
            packContentGrid.RowDefinitions.Clear();
            packContentGrid.Children.Clear();

            //TODO: Pull packs from DB
            //TODO: Change to wrap panel
            //Dynamically create packs and assign data
            for (int i = 0; i < 15; i++)
            {
                PacksUserControl packsUserControl = new PacksUserControl();
                //TODO: Encapsulate
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
            //If menu is open
            if(!hamburgerMenuSwitch)
            {
                //Hide menu
                ShowHideMenu();
            }
        }
    }
}

