using System.Collections.Generic;
using System.Windows.Controls;
using Shared_ShipContentManager.Models;
using ShipContentManager.Services;
using System.Windows;
using System.Threading.Tasks;
using System.Drawing;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for QuestionsUserControl.xaml
    /// </summary>
    public partial class QuestionsUserControl : UserControl
    {
        private ContentManagerDataService dataService;
        private Question question;
        private List<string> userSelectedPacks;
        public QuestionsUserControl(ContentManagerDataService ds, Question q)
        {
            dataService = ds;
            question = q;
            userSelectedPacks = new List<string>();
            InitializeComponent();
            SetQuestionTextLabel(question.QuestionText);
            SetDateCreatedLabel(question.DateCreatedToString());
            populateAffiliatedPacks();

        }
        public void SetQuestionNumberLabel(string questionNumber)
        {
            if (!string.IsNullOrEmpty(questionNumber))
            {
                lblQuestionNumber.Content = questionNumber;
            }
        }
        public void SetQuestionTextLabel(string questionText)
        {
            if (!string.IsNullOrEmpty(questionText))
            {
                txtBoxQuestionText.Text = questionText;
            }
        }
        public void SetDateCreatedLabel(string dateCreated)
        {
            if (!string.IsNullOrEmpty(dateCreated))
            {
                lblDateCreated.Content = dateCreated;
            }
        }
        public void populateAffiliatedPacks()
        {
            List<Pack> packs = dataService.GetLocalPacks();
            List<string> packObjectIds = question.Packs;
            HashSet<string> ids = new HashSet<string>();
            foreach(string objId in packObjectIds)
            {
                ids.Add(objId);
            }
            foreach(Pack p in packs)
            {
                if(ids.Contains(p.PackObjectId))
                {
                    listViewPacks.Items.Add(p);
                    userSelectedPacks.Add(p.PackObjectId);
                }
            }
        }

        private async void populateAllPacks()
        {
            //TODO: Add check for response
            List<Pack> packsList = await dataService.GetPacksFromServer();
            foreach (Pack p in packsList)
            {
                listViewPacks.Items.Add(p);
            }
        }
        private async void btnEditSaveQuestion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (iconSaveEdit.Icon == FontAwesome5.EFontAwesomeIcon.Regular_Save)
            {
                //On save
                listViewPacks.IsEnabled = false;
                txtBoxQuestionText.IsEnabled = false;
                btnEditSaveQuestion.IsEnabled = false;
                question.QuestionText = txtBoxQuestionText.Text;
                question.Packs = userSelectedPacks;
                await dataService.UpdateQuestion(question);
                btnEditSaveQuestion.IsEnabled = true;
                populateAffiliatedPacks();
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.RefreshQuestionsFromDb();
                iconSaveEdit.Icon = FontAwesome5.EFontAwesomeIcon.Regular_Edit;
            }
            else
            {
                //On edit
                iconSaveEdit.Icon = FontAwesome5.EFontAwesomeIcon.Regular_Save;
                txtBoxQuestionText.IsEnabled = true;
                listViewPacks.IsEnabled = true;
                populateAllPacks();
            }
        }

        private void listViewPacks_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
