using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
            var type = ContentEnumerations.CreateContentType.Pack;
            renderContentCreateBtn(type);
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

        private void btnPacks_Click(object sender, RoutedEventArgs e)
        {
            //Query Db for packs and store to Global list
            displayPacks(ContentManagerDataService.GetPacksFromServer());
            var type = ContentEnumerations.CreateContentType.Pack;
            renderContentCreateBtn(type);
        }

        private void btnQuestions_Click(object sender, RoutedEventArgs e)
        {
            displayQuestions(ContentManagerDataService.GetQuestionsFromServer());
            var type = ContentEnumerations.CreateContentType.Question;
            renderContentCreateBtn(type);
        }

        private void renderContentCreateBtn(ContentEnumerations.CreateContentType content)
        {
            Button createButton = new Button();
            BrushConverter bc = new BrushConverter();
            createButton.VerticalAlignment = VerticalAlignment.Top;
            createButton.HorizontalAlignment = HorizontalAlignment.Right;
            createButton.Width = 100;
            createButton.Background = (Brush)bc.ConvertFrom("#474747");
            createButton.Foreground = Brushes.White;

            if (content == ContentEnumerations.CreateContentType.Question)
            {
                createButton.Content = "Create Question";
                createButton.Click += createQuestionsBtn_Click;
            }
            else if (content == ContentEnumerations.CreateContentType.Pack)
            {
                createButton.Content = "Create Pack";
                createButton.Click += CreatePackButton_Click; ;
            }
            mainGrid.Children.Remove(createButton);
            mainGrid.Children.Add(createButton);
        }

        private void CreatePackButton_Click(object sender, RoutedEventArgs e)
        {
            Window createPackWindow = new Window();
            createPackWindow.Title = "Create Pack";

            createPackWindow.Show();
        }

        private void createQuestionsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window createQuestionWindow = new Window();
            createQuestionWindow.Title = "Create Question";
            createQuestionWindow.Show();
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
                questionControl.SetPacks(question.Packs);
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
            if(ContentManagerDataService.GetLocalPacks() != null)
            {
                displayPacks(ContentManagerDataService.GetPacksFromServer());
            }
            else
            {
                //Query Db for packs and store to Global list
                displayPacks(ContentManagerDataService.GetLocalPacks());
            }
        }
    }
}

