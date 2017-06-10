using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using CommonFiles.TransferObjects;
using System.Windows.Controls;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        private void soundSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void press_PushButton(object sender, RoutedEventArgs e)
        {
            getClientconnection().sendObject(new Request("PressPushButton", getDuration()));
        }

        private void press_Rocker_Switch_Down(object sender, RoutedEventArgs e)
        {
            if(getDuration() != -1)
            {
                getClientconnection().sendObject(new Request("PressRockerSwitch", new int[] { 0, getDuration() }));
            }
        }

        private void press_Rocker_Switch_Up(object sender, RoutedEventArgs e)
        {
            if (getDuration() != -1)
            {
                getClientconnection().sendObject(new Request("PressRockerSwitch", new int[] { 1, getDuration() }));
            }
        }
    }
}
