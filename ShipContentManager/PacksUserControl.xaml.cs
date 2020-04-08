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

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for PacksUserControl.xaml
    /// </summary>
    public partial class PacksUserControl : UserControl
    {
        public PacksUserControl()
        {
            InitializeComponent();
        }

        public void SetPackNameLabelText(string packName)
        {
            if (!string.IsNullOrEmpty(packName))
            {
                lblPackName.Content = packName;
            }
        }
        public void SetDateCreatedLabelText(string dateCreated)
        {
            if(!string.IsNullOrEmpty(dateCreated))
            {
                lblDateCreated.Content = dateCreated;
            }
        }
        public void SetIsMiniPackCheckbox(bool isMiniPack)
        {
            checkboxIsMiniPack.IsChecked = isMiniPack;
        }
    }
}
