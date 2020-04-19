using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using Shared_ShipContentManager.Interfaces;
using Shared_ShipContentManager.Models;
using ShipContentManager.Services;
using Shared_ShipContentManager.Services;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IShipClientService shipClientService;
        private ContentManagerDataService dataService;
        private bool hamburgerMenuSwitch = true;
        private readonly Visibility hidden = Visibility.Hidden;
        private readonly Visibility visible = Visibility.Visible;
        private const string AddQuestion = "ADD QUESTION";
        private const string AddPack = "ADD PACK";
        public MainWindow()
        {
            shipClientService = new ShipClient();
            dataService = new ContentManagerDataService(shipClientService);
            InitializeComponent();
            btnPacks.Visibility = hidden;
            btnQuestions.Visibility = hidden;
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
                btnPacks.Visibility = visible;
                btnQuestions.Visibility = visible;
                hamburgerMenuSwitch = false;
            }
            else
            {
                sb = Resources["sbHideHamburgerMenu"] as Storyboard;
                sb.Begin(pnlLeftMenu);
                btnPacks.Visibility = hidden;
                btnQuestions.Visibility = hidden;
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

        private void displayQuestions(List<Question> questions)
        {
            btnAddContent.ToolTip = AddQuestion;
            contentWrapPanel.Children.Clear();
            contentWrapPanel.VerticalAlignment = VerticalAlignment.Stretch;
            int questionCount = 0;
            foreach(Question question in questions)
            {
                questionCount++;
                QuestionsUserControl questionControl = new QuestionsUserControl(dataService, question);
                questionControl.SetQuestionNumberLabel(questionCount.ToString());
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
                PacksUserControl packControl = new PacksUserControl(dataService, pack);
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
            if(dataService.GetLocalPacks() == null)
            {
                //Query Db for packs and store to Global list
                displayPacks(await dataService.GetPacksFromServer());
            }
            else
            {
                //Pull packs from stored list
                displayPacks(dataService.GetLocalPacks());
            }
        }

        private void btnAddContent_Click(object sender, RoutedEventArgs e)
        {
            if(btnAddContent.ToolTip.ToString() == AddPack)
            {
                CreatePackWindow createPackWindow = new CreatePackWindow(dataService);
                createPackWindow.Show();
            }
            else
            {
                CreateQuestionWindow createQuestionWindow = new CreateQuestionWindow(dataService);
                createQuestionWindow.Show();
            }
        }
        public async void RefreshQuestionsFromDb()
        {
            displayQuestions(await dataService.GetQuestionsFromServer());
        }
        public async void RefreshPacksFromDb()
        {
            displayPacks(await dataService.GetPacksFromServer());
        }
    }
}

