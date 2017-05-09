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
            connectionTest("minwinpc");
        }

        public string IPaddress { get; set; }
        public string DeviceName { get; set; }

        private void connectIP_button_Click(object sender, RoutedEventArgs e)
        {
            //TODO:add functionality here
        }

        /// <summary>
        /// 1. creates a TCP connection the the echoserver
        /// 2. sends string "test" to the echoserver
        /// 3. trys to receive a string from the echoserver
        /// 4. writes the received string on debug
        /// </summary>
        /// <param name="hostname">name of the echoserver to connect to</param>
        public void connectionTest(string hostname)
        {
            try
            {
                //connect to the server
                int port = 7777;
                TcpClient client = new TcpClient(hostname, port);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

                //send string test to the echoserver
                writer.WriteLine("test");

                //receive a string from the echoserver
                string lineReceived = reader.ReadLine();

                //write the received string on debug
                Debug.WriteLine("Received from server: " + lineReceived);
            }catch(Exception e)
            {
                Debug.WriteLine("connection Test failed");
            }
           
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
    }
}
