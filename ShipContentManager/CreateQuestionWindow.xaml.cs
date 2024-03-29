﻿using Shared_ShipContentManager.Models;
using ShipContentManager.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for CreateQuestionWindow.xaml
    /// </summary>
    public partial class CreateQuestionWindow : Window
    {
        private ContentManagerDataService dataService;
        private List<string> userSelectedPacks;
        public CreateQuestionWindow(ContentManagerDataService ds)
        {
            dataService = ds;
            InitializeComponent();
            populatePacksList(); 
            userSelectedPacks = new List<string>();
        }

        private async void populatePacksList()
        {
            //TODO: Add check for response
            List<Pack> packsList = await dataService.GetPacksFromServer();
            foreach (Pack p in packsList)
            {
                lstViewQuestionPacks.Items.Add(p);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateFileds())
            {
                Question q = new Question();
                q.DateCreated = DateTime.Now;
                q.Packs = userSelectedPacks;
                q.QuestionText = txtBlockQuestionText.Text;
                //TODO: Add check for response
                var createResponse = await dataService.CreateQuestion(q);
                if (createResponse != null)
                {
                    btnSave.IsEnabled = false;
                    MainWindow main = (MainWindow)Application.Current.MainWindow;
                    btnSave.IsEnabled = true;
                    main.RefreshQuestionsFromDb();
                    
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("An Error has occurred with creating this question.");
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled out!", "Error", MessageBoxButton.OK);
            }
        }
        private bool validateFileds()
        {
            if (string.IsNullOrWhiteSpace(txtBlockQuestionText.Text) || userSelectedPacks.Count == 0)
            {
                return false;
            }
            return true;
        }
        private void lstViewQuestionPacks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Pack p in e.RemovedItems)
            {
                userSelectedPacks.Remove(p.PackObjectId);
            }

            foreach (Pack p in e.AddedItems)
            {
                userSelectedPacks.Add(p.PackObjectId);
            }
        }
    }
}
