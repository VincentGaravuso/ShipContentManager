using Shared_ShipContentManager.Models;
using ShipContentManager.Services;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for CreateQuestionWindow.xaml
    /// </summary>
    public partial class CreateQuestionWindow : Window
    {
        private ContentManagerDataService dataService;
        private List<string> selectedPacks;
        public CreateQuestionWindow(ContentManagerDataService ds)
        {
            dataService = ds;
            InitializeComponent();
            populatePacksList(); 
            selectedPacks = new List<string>();
        }

        private async void populatePacksList()
        {
            //TODO: Add check for response
            List<Pack> packsList = await dataService.GetPacksFromServer();
            foreach (Pack p in packsList)
            {
                listView1.Items.Add(p);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Question q = new Question();
            q.DateCreated = DateTime.Now;
            q.Packs = selectedPacks;
            q.QuestionText = txtBlockQuestionText.Text;
            //TODO: Add check for response
            await dataService.CreateQuestion(q); 
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.RefreshQuestionsFromDb();
            this.Close();
        }

        private void listView1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (Pack p in e.RemovedItems)
            {
                selectedPacks.Remove(p.PackObjectId);
            }

            foreach (Pack p in e.AddedItems)
            {
                selectedPacks.Add(p.PackObjectId);
            }
        }
    }
}
