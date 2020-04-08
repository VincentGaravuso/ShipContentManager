using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShipContentManager.Models;

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
        public void SetPacksLabel(List<Pack> packs)
        {
            if(packs.Count > 0)
            {
                foreach(Pack pack in packs)
                {
                    lblPackNames.Content = pack.Name;
                }
            }
        }
    }
}
