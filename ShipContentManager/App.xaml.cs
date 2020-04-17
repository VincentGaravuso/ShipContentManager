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
        private void OnStartup(object sender, StartupEventArgs e)
        {

        }
    }
}

