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

    }

}
