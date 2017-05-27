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
