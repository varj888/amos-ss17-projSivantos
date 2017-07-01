using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using Windows.UI.Xaml.Controls.Primitives;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {

        private int lcdBacklightState = 0;
        private int _scrollSpeed;

        //private void reconnectI2C_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        sendRequest(new Request("SendToLCD", "#reset"));
        //        this.addMessage("GPIO", "Request sent");
        //    }
        //    catch (Exception ex)
        //    {
        //        this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
        //    }
        //}

        //private void toggleBacklightButton_Click(object sender, RoutedEventArgs e)
        //{


        //    lcdBacklightState = lcdBacklightState == 0 ? 1 : 0;
        //    try
        //    {
        //        sendRequest(new Request("ToggleBacklight_LCD", lcdBacklightState));
        //        this.addMessage("GPIO", "Request sent");
        //    }
        //    catch (Exception ex)
        //    {
        //        this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
        //    }
        //}

        //private void sendToLcdButton_Click(object sender, RoutedEventArgs e)
        //{

        //    string text = displayEingabeTextBox.Text;

        //    try
        //    {
        //        sendRequest(new Request("SendToLCD", text));
        //        this.addMessage("GPIO", "Request sent");
        //    }
        //    catch (Exception ex)
        //    {
        //        this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
        //    }

        //}

        //private void addText(string text)
        //{
        //    displayEingabeTextBox.Text = text;
        //}

        //private void sample16Button_Click(object sender, RoutedEventArgs e)
        //{
        //    addText("Das ist ein Text");
        //}

        //private void sample32Button_Click(object sender, RoutedEventArgs e)
        //{
        //    addText("Das ist ein Text mit 32 Zeichen!");
        //}

        //private void sampleGT32Button_Click(object sender, RoutedEventArgs e)
        //{
        //    addText("Das ist ein Beispieltext mit mehr als 16 Zeichen. ");
        //}


        //private void cancelButton_Click(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        sendRequest(new Request("SendToLCD", "#cancel"));
        //        this.addMessage("GPIO", "Request sent");
        //    }
        //    catch (Exception ex)
        //    {
        //        this.addMessage("GPIO", "Request could not be sent: " + ex.Message);
        //    }
        //}

        private void scrollSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //not implemented yet
            //Slider slider = sender as Slider;

            //_scrollSpeed = getSpeed((int)slider.Value);

        }
        //private int getSpeed(int value)
        //{
        //    int scrollSpeed = 0;

        //    if (value < 26)
        //    {
        //        scrollSpeed = 1;
        //    }
        //    else if (value > 25 && value < 51)
        //    {
        //        scrollSpeed = 2;
        //    }
        //    else if (value > 50 && value < 75)
        //    {
        //        scrollSpeed = 3;
        //    }
        //    else
        //    {
        //        scrollSpeed = 4;
        //    }

        //    return scrollSpeed;
        //}

    }

}

