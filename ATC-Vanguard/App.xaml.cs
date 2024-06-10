using ATC_Vanguard.Vanguard.Services;
using ATC_Vanguard.Vanguard;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;

namespace ATC_Vanguard
{
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            Program.Main();
        }
    }
}

