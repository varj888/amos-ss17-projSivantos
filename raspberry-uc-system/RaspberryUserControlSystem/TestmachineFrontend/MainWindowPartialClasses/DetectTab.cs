using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestmachineFrontend
{

    public partial class MainWindow : Window
    {

        public UInt16 PinID { get; set; }
        public string IPaddress { get; set; }
        public string Port { get; set; }
        public string DeviceName { get; set; }

        private List<ClientConn<Request>> connections = new List<ClientConn<Request>>();
        private ClientConn<Request> clientConnection;

        public List<ClientConn<Request>> Connections
        {
            get { return connections; }
            set { connections = value; }
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

        private void readPin_button_Click(object sender, RoutedEventArgs e)
        {
           sendRequest(new Request("ReadPin", PinID));
        }

        private void writePin_button_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("WritePin", PinID));
              
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("ResetPin", PinID));
        }

        private void ledOFF_button_Click(object sender, RoutedEventArgs e)
        {
           sendRequest(new Request("LightLED", 0));
        }

        private void ledON_button_Click(object sender, RoutedEventArgs e)
        {
               sendRequest(new Request("LightLED", 1));
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

        private void setVoltage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderValue = Convert.ToByte(setVoltage_Slider.Value);
        }

        private byte sliderValue = 0;

        private void setVoltage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            sliderValue = Convert.ToByte(setVoltage_Slider.Value);
        }
    }
}
