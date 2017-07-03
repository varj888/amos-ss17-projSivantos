using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ////base.OnStartup(e);

            //var window = new MainWindow();
            ////window.DataContext = new MainWindowViewModel();
            ////{ DataContext = new MainWindowViewModel() };
            ////window.Show();
            //MainWindow = window;
            //MainWindow.Show();

            base.OnStartup(e);

            //MainWindow app = new MainWindow();
            //MainWindowViewModel context = new MainWindowViewModel();
            //TestMachineFrontend1.MainWindow.Instance.DataContext = context;
            TestMachineFrontend1.MainWindow.Instance.Show();
        }

        //public App()
        //{
        //    Startup += App_Startup;
        //}

        //void App_Startup(object sender, StartupEventArgs e)
        //{
        //    TestMachineFrontend1.MainWindow.Instance.Show();
        //}
    }
}
