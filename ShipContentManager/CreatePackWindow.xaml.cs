using ShipContentManager.Services;
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
    }
}
