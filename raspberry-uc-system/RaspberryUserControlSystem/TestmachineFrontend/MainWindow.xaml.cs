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
using System.Threading;
using Windows.UI.Xaml.Controls.Primitives;

namespace TestmachineFrontend
{
    /// <summary>
    /// Simple UI for establishing connection
    /// to Raspberry-Pi
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string hostname = "minwinpc";
        private List<ClientConn<Request>> connections = new List<ClientConn<Request>>();
        private ClientConn<Request> clientConnection;

        public MainWindow()
        {
            InitializeComponent();

            //test for the class RequestConnClient
            //try
            //{
            //    clientConnection = new ClientConn<Request>(hostname, 13370);
            //    Debug.WriteLine("Connection to " + hostname + " established.");
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e);
            //    Debug.WriteLine("Connection to " + hostname + " failed.");
            //}


            this.DataContext = this;
        }

        public UInt16 PinID { get; set; }
        public string IPaddress { get; set; }
        public string Port { get; set; }
        public string DeviceName { get; set; }


        public List<ClientConn<Request>> Connections
        {
            get { return connections; }
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

        private int lcdBacklightState = 0;
        private void toggleBacklightButton_Click(object sender, RoutedEventArgs e)
        {

            //lcd.toggleBacklight();
            lcdBacklightState = lcdBacklightState == 0 ? 1 : 0;
            try
            {
                clientConnection.sendObject(new Request("ToggleBacklight_LCD", lcdBacklightState));
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }
        }

        private void displayEingabeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private int _scrollSpeed;
        CancellationTokenSource _cts;

        private void sendToLcdButton_Click(object sender, RoutedEventArgs e)
        {
            ////Task.Factory.StartNew(() => sendTextToLcd()); ==> Funktioniert nicht!!!
            //lcd.cts = new CancellationTokenSource();
            //string text = displayEingabeTextBox.Text;
            //lcd.sendTextToLcd(text);
        }



        public void sendTextToLcd()
        {

        }

        private void addText(string text)
        {
            displayEingabeTextBox.Text = text;
        }

        private void sample16Button_Click(object sender, RoutedEventArgs e)
        {
            addText("Das ist ein Text");
        }

        private void sample32Button_Click(object sender, RoutedEventArgs e)
        {
            addText("Das ist ein Text mit 32 Zeichen!");
        }

        private void sampleGT32Button_Click(object sender, RoutedEventArgs e)
        {
            addText("Das ist ein Beispieltext mit mehr als 16 Zeichen");
        }

        private void scrollSlider_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void scrollSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;

            //lcd.scrollSpeed = getSpeed((int)slider.Value);
            ////Task<int> CalculateScrollSpeed = Task.Factory.StartNew(() => getSpeed((int)slider.Value));
            ////this._scrollSpeed = CalculateScrollSpeed.Result;

        }

        private int getSpeed(int value)
        {
            int scrollSpeed = 0;

            if (value < 26)
            {
                scrollSpeed = 1;
            }
            else if (value > 25 && value < 51)
            {
                scrollSpeed = 2;
            }
            else if (value > 50 && value < 75)
            {
                scrollSpeed = 3;
            }
            else
            {
                scrollSpeed = 4;
            }

            return scrollSpeed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            };
        }

        private void scrollSlider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void HI_ON_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                clientConnection.sendObject(new Request("TurnOnHI", 127));
                this.addMessage("TurnOnHI", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("TurnOnHI", "Request could not be sent: " + ex.Message);
            }
        }

        private void HI_OFF_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                clientConnection.sendObject(new Request("TurnOnHI", 0));
                this.addMessage("TurnOnHI", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("TurnOnHI", "Request could not be sent: " + ex.Message);
            }
        }


        private void setVoltage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderValue = Convert.ToByte(setVoltage_Slider.Value);
        }

        private byte sliderValue = 0;

        private void setVoltage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            sliderValue = Convert.ToByte(setVoltage_Slider.Value);
        }

        private void sendVoltageValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clientConnection.sendObject(new Request("TurnOnHI", sliderValue));
                this.addMessage("TurnOnHI", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("TurnOnHI", "Request could not be sent: " + ex.Message);
            }
        }
    }
}