using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using Shared_ShipContentManager.Interfaces;
using Shared_ShipContentManager.Models;
using ShipContentManager;
using ShipContentManager.Services;
using FontAwesome.WPF;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string AddQuestion = "ADD QUESTION";
        private const string AddPack = "ADD PACK";
        private bool hamburgerMenuSwitch = true;

        private ContentManagerDataService dataService;
        public MainWindow(){}
        public MainWindow(IShipClientService clientService)
        {
            dataService = new ContentManagerDataService(clientService);
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
                btnPacks.Visibility = Visibility.Visible;
                btnQuestions.Visibility = Visibility.Visible;
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
        
        private void showWrapPanelLoading()
        {
            contentWrapPanel.Children.Clear();
            contentWrapPanel.Children.Add(iconLoading);
            contentWrapPanel.VerticalAlignment = VerticalAlignment.Center;
        }

        private async void btnPacks_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu();
            showWrapPanelLoading();
            displayPacks(await dataService.GetPacksFromServer());
        }

        private async void btnQuestions_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu();
            showWrapPanelLoading();
            displayQuestions(await dataService.GetQuestionsFromServer());
        }

        private  void displayQuestions(List<Question> questions)
        {
            btnAddContent.ToolTip = AddQuestion;
            contentWrapPanel.Children.Clear();
            contentWrapPanel.VerticalAlignment = VerticalAlignment.Stretch;
            int questionCount = 0;
            foreach(Question question in questions)
            {
                questionCount++;
                QuestionsUserControl questionControl = new QuestionsUserControl();
                questionControl.SetQuestionNumberLabel(questionCount.ToString());
                questionControl.SetQuestionTextLabel(question.QuestionText);
                questionControl.SetDateCreatedLabel(question.DateCreatedToString());
                questionControl.SetPacks(dataService.GetLocalPacks(), question.Packs);
                questionControl.Margin = new Thickness(10, 10, 0, 0);
                contentWrapPanel.Children.Add(questionControl);
            }
        }

        private void displayPacks(List<Pack> packs)
        {
            btnAddContent.ToolTip = AddPack;
            contentWrapPanel.Children.Clear();
            contentWrapPanel.VerticalAlignment = VerticalAlignment.Stretch;
            foreach (Pack pack in packs)
            {
                PacksUserControl packControl = new PacksUserControl();
                packControl.SetPackNameLabelText(pack.Name.ToUpper());
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

        private async void mainGrid_Initialized(object sender, EventArgs e)
        {
            //On start - show packs screen
            if(dataService.GetLocalPacks() != null)
            {
                displayPacks(await dataService.GetPacksFromServer());
            }
            else
            {
                //Query Db for packs and store to Global list
                displayPacks(dataService.GetLocalPacks());
            }
        }

        private void btnAddContent_Click(object sender, RoutedEventArgs e)
        {
            if(btnAddContent.ToolTip.ToString() == AddPack)
            {
                CreatePackWindow createPackWindow= new CreatePackWindow();
                createPackWindow.Show();
            }
            else
            {
                CreateQuestionWindow createQuestionWindow = new CreateQuestionWindow();
                createQuestionWindow.Show();
            }
        }
    }
}

