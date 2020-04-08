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
using ShipContentManager.Models;
using ShipContentManager.Services;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            displayPacks(ContentManagerService.GetPacksFromDb());
        }

        private void btnQuestions_Click(object sender, RoutedEventArgs e)
        {
            displayQuestions(ContentManagerService.GetQuestionsFromDb());
        }
        private void displayQuestions(List<Question> questions)
        {
            contentWrapPanel.Children.Clear();
            int questionCount = 0;
            foreach(Question question in questions)
            {
                questionCount++;
                QuestionsUserControl questionControl = new QuestionsUserControl();
                questionControl.SetQuestionNumberLabel(questionCount.ToString());
                questionControl.SetQuestionTextLabel(question.QuestionText);
                questionControl.SetDateCreatedLabel(question.DateCreatedToString());
                questionControl.SetPacksLabel(question.Packs);
                questionControl.Margin = new Thickness(10, 10, 0, 0);

                contentWrapPanel.Children.Add(questionControl);
            }
        }

        private void displayPacks(List<Pack> packs)
        {
            contentWrapPanel.Children.Clear();
            foreach(Pack pack in packs)
            {
                PacksUserControl packControl = new PacksUserControl();
                packControl.SetPackNameLabelText(pack.Name);
                packControl.SetDateCreatedLabelText(pack.DateCreatedToString());
                packControl.SetIsMiniPackCheckbox(pack.IsMiniPack);
                packControl.Margin = new Thickness(10, 10, 0, 0);

                contentWrapPanel.Children.Add(packControl);
            }
        }
        private void MainWindowScrollView_GotFocus(object sender, RoutedEventArgs e)
        {
            //If menu is open
            if (!hamburgerMenuSwitch)
            {
                //Hide menu
                ShowHideMenu();
            }
        }

        private void mainGrid_Initialized(object sender, EventArgs e)
        {
            //On start - show packs screen
            if(ContentManagerService.GetStoredPacks() != null)
            {
                displayPacks(ContentManagerService.GetPacksFromDb());
            }
            else
            {
                //Query Db for packs and store to Global list
                displayPacks(ContentManagerService.GetStoredPacks());
            }
        }
    }
}

