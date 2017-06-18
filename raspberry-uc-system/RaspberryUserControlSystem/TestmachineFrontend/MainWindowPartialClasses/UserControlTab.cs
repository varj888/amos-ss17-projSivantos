﻿using CommonFiles.TransferObjects;
using System.Windows;
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
            if (getDuration() != -1)
            {
                sendRequest(new Request("PressPushButton", getDuration()));
            }
            else
            {
                this.addMessage("Debug", "Invalid duration");
            }
        }

        private void press_Rocker_Switch_Down(object sender, RoutedEventArgs e)
        {
            if (getDuration() != -1)
            {
                sendRequest(new Request("PressRockerSwitch", new int[] { 0, getDuration() }));
            }
            else
            {
                this.addMessage("Debug", "Invalid duration");
            }
        }

        private void press_Rocker_Switch_Up(object sender, RoutedEventArgs e)
        {
            if (getDuration() != -1)
            {
                sendRequest(new Request("PressRockerSwitch", new int[] { 1, getDuration() }));
            }
            else
            {
                this.addMessage("Debug", "Invalid duration");
            }
        }

        private void press_Combination(object sender, RoutedEventArgs e)
        {
            if (getDuration() != -1)
            {
                int[] param = new int[4];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = 0;
                }
                param[param.Length - 1] = getDuration();

                int duration = getDuration();
                if (rockerswitch_Down_Checkbox.IsChecked == true)
                {
                    param[0] = 1;
                }
                if (rockerswitch_Up_Checkbox.IsChecked == true)
                {
                    param[1] = 1;
                }
                if (pushButton_Checkbox.IsChecked == true)
                {
                    param[2] = 1;
                }
                sendRequest(new Request("PressCombination", param));
            }
            else
            {
                this.addMessage("Debug", "Invalid duration");
            }
        }

        private void DetectTCol_Button_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("EnableTeleCoil", 1));
        }

        private void DetectAudioShoe_Button_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("EnableAudioShoe", 1));
        }

        private void UndetectAudioShoe_Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("EnableAudioShoe", 0));
        }

        private void UndetectTCol_Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            sendRequest(new Request("EnableTeleCoil", 0));
        }

        private void receiverUpdate_Click(object sender, RoutedEventArgs e)
        {
            int a = this.receiverBox.SelectedIndex;
            ComboBoxItem s = (ComboBoxItem)receiverBox.Items[a];
            sendRequest(new Request("SetARDVoltage", s.Content));
        }
    }
}
