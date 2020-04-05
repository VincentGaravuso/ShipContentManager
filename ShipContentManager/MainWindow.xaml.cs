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
            if(hamburgerMenuOpened)
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
                btnPacks.Visibility = Visibility.Visible;
                btnQuestions.Visibility = Visibility.Visible;
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnPacks.Visibility = Visibility.Hidden;
                btnQuestions.Visibility = Visibility.Hidden;
            }
        }

        private void btnPacks_Click(object sender, RoutedEventArgs e)
        {
            packContentGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < 3; i++)
            {
                PacksUserControl packsUserControl = new PacksUserControl();
                packsUserControl.lblPackName.Content = $"Pack {i}";
                packsUserControl.lblDateCreated.Content = DateTime.Now.ToString("dd/MM/yyyy");
                packsUserControl.Margin = new Thickness(20, 0, 0, 0);
                ColumnDefinition columnDefinition = new ColumnDefinition();
                packContentGrid.ColumnDefinitions.Add(columnDefinition);
                Grid.SetColumn(packsUserControl, i);
                packContentGrid.Children.Add(packsUserControl);
            }
        }

        private void btnQuestions_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
