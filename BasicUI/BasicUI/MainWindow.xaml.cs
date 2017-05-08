using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicUI
{
    /// <summary>
    /// Simple UI for establishing connection
    /// to Raspberry-Pi
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string IPaddress { get; set; }

        private void connectIP_button_Click(object sender, RoutedEventArgs e)
        {
            //TODO:add functionality here
        }
    }
}
