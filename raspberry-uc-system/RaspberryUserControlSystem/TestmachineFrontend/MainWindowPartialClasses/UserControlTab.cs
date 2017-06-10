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
            if(durationBox.SelectedIndex < 0)
            {
                this.addMessage("Debug", "You need to select an item");
                return;
            }
            var a = (ComboBoxItem) durationBox.Items.GetItemAt(durationBox.SelectedIndex);
            UInt16 duration;
            switch(a.Content)
            {
                case "Short":
                    duration = 300;
                    break;
                case "Medium":
                    duration = 1000;
                    break;
                case "Long":
                    duration = 5000;
                    break;
                default:
                    return;
            }
            getClientconnection().sendObject(new Request("PressPushButton", duration));
        }
    }
}
