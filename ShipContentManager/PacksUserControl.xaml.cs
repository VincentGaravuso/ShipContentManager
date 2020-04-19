using System.Windows.Controls;
using System.Windows;
using ShipContentManager.Services;
using Shared_ShipContentManager.Models;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for PacksUserControl.xaml
    /// </summary>
    public partial class PacksUserControl : UserControl
    {
        private ContentManagerDataService dataService;
        private Pack pack;
        public PacksUserControl(ContentManagerDataService ds, Pack p)
        {
            pack = p;
            dataService = ds;
            InitializeComponent();
            SetPackNameLabelText(p.Name.ToUpper());
            SetDateCreatedLabelText(p.DateCreatedToString());
            SetIsMiniPackCheckbox(p.IsMiniPack);
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
        private async void btnEditSavePack_Click(object sender, RoutedEventArgs e)
        {
            if(iconSaveEdit.Icon == FontAwesome5.EFontAwesomeIcon.Regular_Save)
            {
                iconSaveEdit.Icon = FontAwesome5.EFontAwesomeIcon.Regular_Edit;
                txtBoxPackName.Visibility = Visibility.Hidden;
                lblPackName.Visibility = Visibility.Visible;
                btnEditSavePack.IsEnabled = false;
                await dataService.UpdatePack(pack.PackObjectId, txtBoxPackName.Text.ToLower());
                btnEditSavePack.IsEnabled = true;
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.RefreshPacksFromDb();
            }
            else
            {
                iconSaveEdit.Icon = FontAwesome5.EFontAwesomeIcon.Regular_Save;
                txtBoxPackName.Text = lblPackName.Content.ToString();
                txtBoxPackName.Visibility = Visibility.Visible;
                lblPackName.Visibility = Visibility.Hidden;
            }

        }
    }
}
