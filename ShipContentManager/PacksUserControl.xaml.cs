using System.Windows.Controls;

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
