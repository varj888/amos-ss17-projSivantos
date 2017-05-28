using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {

        private int lcdBacklightState = 0;
        private int _scrollSpeed;
        // no valid code anymore
        //CancellationTokenSource sendToLCDcancelToken;


        private void toggleBacklightButton_Click(object sender, RoutedEventArgs e)
        {


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


        private void sendToLcdButton_Click(object sender, RoutedEventArgs e)
        {
            // no valid code anymore
            //Task.Factory.StartNew(() => sendTextToLcd()); //==> Funktioniert nicht!!!
            //sendToLCDcancelToken = new CancellationTokenSource();

            string text = displayEingabeTextBox.Text;

            try
            {
                clientConnection.sendObject(new Request("SendToLCD", text)); //sendToLCDcancelToken
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }

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
            addText("Das ist ein Beispieltext mit mehr als 16 Zeichen. ");
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            // no valid code anymore
            //if (sendToLCDcancelToken != null)
            //{
            //    sendToLCDcancelToken.Cancel();
            //};

            try
            {
                clientConnection.sendObject(new Request("SendToLCD", "cancel")); //sendToLCDcancelToken
                this.addMessage("GPIO", "Request sent");
            }
            catch (Exception ex)
            {
                this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
            }
        }

        private void scrollSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Slider slider = sender as Slider;

            _scrollSpeed = getSpeed((int)slider.Value);

            //Task<int> CalculateScrollSpeed = Task.Factory.StartNew(() => getSpeed((int)slider.Value));
            //this._scrollSpeed = CalculateScrollSpeed.Result;

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

    }

}

