using Shared_ShipContentManager.Models;
using ShipContentManager.Services;
using System;
using System.Windows;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for CreatePackWindow.xaml
    /// </summary>
    public partial class CreatePackWindow : Window
    {
        private ContentManagerDataService dataService;
        public CreatePackWindow(ContentManagerDataService ds)
        {
            dataService = ds;
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(validateFields())
            {
                Pack p = new Pack();
                p.Name = txtBoxPackName.Text;
                p.IsMiniPack = checkBoxIsMiniPack.IsChecked.GetValueOrDefault();
                await dataService.CreatePack(p);
                btnSave.IsEnabled = false;
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                btnSave.IsEnabled = true;
                main.RefreshPacksFromDb();
                this.Close();

            }
        }

        private bool validateFields()
        {
            if(string.IsNullOrWhiteSpace(txtBoxPackName.Text))
            {
                return false;
            }
            return true;
        }
    }

}
