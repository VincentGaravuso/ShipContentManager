using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
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
        private static ShipClient shipClient;
        protected override void OnStartup(StartupEventArgs e)
        {
            serviceProvider = new ServiceCollection()
              .AddSingleton<HttpClient>()
              .AddTransient<ShipClient>()
              .BuildServiceProvider();
            shipClient = serviceProvider.GetService<ShipClient>();
            ContentManagerDataService.Init(shipClient);
        }
    }
}

