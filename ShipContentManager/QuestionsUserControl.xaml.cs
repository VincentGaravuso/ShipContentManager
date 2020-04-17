using System.Collections.Generic;
using System.Windows.Controls;
using Shared_ShipContentManager.Models;
namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for QuestionsUserControl.xaml
    /// </summary>
    public partial class QuestionsUserControl : UserControl
    {
        public QuestionsUserControl()
        {
            InitializeComponent();
            
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
                txtblockQuestionText.Text = questionText;
            }
        }
        public void SetDateCreatedLabel(string dateCreated)
        {
            if (!string.IsNullOrEmpty(dateCreated))
            {
                lblDateCreated.Content = dateCreated;
            }
        }
        public void SetPacks(List<Pack> packs, List<string> packObjectIds)
        {
            foreach (var q in packObjectIds)
            {
                foreach (var p in packs)
                {
                    if (q == p.PackObjectId)
                    {
                        checkedListBox.Items.Add(p.Name);
                    }
                    
                }
            }
        }
    }
}
