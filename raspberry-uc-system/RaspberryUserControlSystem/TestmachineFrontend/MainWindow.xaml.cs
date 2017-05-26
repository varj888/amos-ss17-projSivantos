using CommonFiles.Networking;
using CommonFiles.TransferObjects;
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
        private List<ClientConn<Request>> connections = new List<ClientConn<Request>>();
        private ClientConn<Request> clientConnection;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public UInt16 PinID { get; set; }
        public string IPaddress { get; set; }
        public string Port { get; set; }
        public string DeviceName { get; set; }


        public List<ClientConn<Request>> Connections {
             get { return connections;}
             set { connections = value; }
        }

        private void connectIP_button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Connections.Add(new ClientConn<Request>(IPaddress, 13370));
            //}
            //catch (Exception)
            //{
            //    this.debug.Items.Add(new DebugContent { origin = "TCP Connection", text = "Couldn't establish connection" });
            //}
            connectToBackend();

        }

        public void connectToBackend()
        {
            try
            {
                clientConnection = new ClientConn<Request>(IPaddress, 13370);
                this.addMessage("TCP", "Connection to " + IPaddress + " established.");
            }
            catch (Exception e)
            {
                this.addMessage("TCP", e.Message);
            }
        }

        public void addMessage(string origin, string msg)
        {
            this.debug.Items.Insert(0, new DebugContent { origin = origin, text = msg });
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
            try
            {
                //Connections[0].sendObject(new Request("ReadPin", PinID));
                clientConnection.sendObject(new Request("ReadPin", PinID));
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }
        }

        private void writePin_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Connections[0].sendObject(new Request("WritePin", PinID));
                clientConnection.sendObject(new Request("WritePin", PinID));
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Connections[0].sendObject(new Request("ResetPin", PinID));
                clientConnection.sendObject(new Request("ResetPin", PinID));
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }
        }

        private void ledOFF_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clientConnection.sendObject(new Request("LightLED", 0));
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }
        }

        private void ledON_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clientConnection.sendObject(new Request("LightLED", 1));
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }

        }

    }
}