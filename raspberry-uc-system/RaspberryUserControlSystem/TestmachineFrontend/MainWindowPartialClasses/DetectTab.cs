using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        //UI Bindings
        public UInt16 PinID { get; set; }
        public string IPaddress { get; set; }

        // Using a DependencyProperty as the backing store for 
        //IsCheckBoxChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckBoxCheckedProperty =
            DependencyProperty.Register("IsCheckBoxChecked", typeof(bool),
              typeof(MainWindow), new UIPropertyMetadata(false));

        private async void connectIP_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pi1 = await RaspberryPi.Create(new IPEndPoint(IPAddress.Parse(IPaddress), 54321)); // asynchronously creates and initializes an instance of RaspberryPi
                connected_checkbox.IsChecked = pi1.IsConnected;
                raspberryPis.Add(IPaddress,pi1);
                this.BackendList.Items.Add(new RaspberryPiItem() { Name = IPaddress, Id = 45, Status = "OK", raspi = pi1 });
            }
            catch (FormatException fx)
            {
                this.addMessage("[ERROR]", "Invalid IP Address Format: " + fx.Message);
                connected_checkbox.IsChecked = false;
            }
            catch (SocketException sx)
            {
                this.addMessage("[ERROR]", "Couldn't establish connection: " + sx.Message);
                connected_checkbox.IsChecked = false;

            }
            catch (Exception any)
            {
                this.addMessage("[ERROR]", "Unknown Error. " + any.Message);
                connected_checkbox.IsChecked = false;
            }
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

        private void connect_Pins_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("ConnectPins", 0));
        }

        private void setVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider el = sender as Slider;
            sendRequest(new Request("SetAnalogVolume", Convert.ToByte(el.Value)));
        }

        private void vcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider el = sender as Slider;
            sendRequest(new Request("TurnHIOn", el.Value));
        }
    }
}
