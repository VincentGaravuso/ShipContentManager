using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using Shared_ShipContentManager.Interfaces;
using Shared_ShipContentManager.Services;
using ShipContentManager.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace ShipContentManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ServiceProvider serviceProvider;
        private static ServiceCollection serviceCollection;
        private static HttpClient httpClient;
        public App()
        {
            serviceCollection = new ServiceCollection();
            httpClient = new HttpClient();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services
              .AddSingleton<HttpClient>()
              .AddSingleton<IShipClientService>(x => new ShipClient(httpClient))
              .AddSingleton<MainWindow>();
        }
    }
}

