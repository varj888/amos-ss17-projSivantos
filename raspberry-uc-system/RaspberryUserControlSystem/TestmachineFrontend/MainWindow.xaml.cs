using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Xml;

namespace TestmachineFrontend
{
    /// <summary>
    /// Simple UI for establishing connection
    /// to Raspberry-Pi
    /// </summary>
    public partial class MainWindow : Window
    {

        private string hostname = "192.168.137.193";
        private RequestConnClient clientConnection;

        public MainWindow()
        {
            InitializeComponent();
            //test for the class RequestConnClient
            try
            {
                clientConnection = new RequestConnClient(hostname);
                Debug.WriteLine("Connection to " + hostname + " established.");
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("Connection to " + hostname + " failed.");
            }
            
            //clientConnection.send(new Request("LightLED", 1));
            this.DataContext = this;
        }

        public UInt16 PinID { get; set; }
        public string IPaddress { get; set; }
        public string DeviceName { get; set; }

        private void connectIP_button_Click(object sender, RoutedEventArgs e)
        {
            //TODO:add functionality here
        }

        private void vcSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void vcSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void vcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void soundSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void readPin_button_Click(object sender, RoutedEventArgs e)
        {
            clientConnection.send(new Request("read", PinID));
        }

        private void writePin_button_Click(object sender, RoutedEventArgs e)
        {
            clientConnection.send(new Request("write", PinID));
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            clientConnection.send(new Request("reset", PinID));
        }
    }

}