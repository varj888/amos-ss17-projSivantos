using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
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
using TestMachineFrontend1.Model;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.View
{
    /// <summary>
    /// Interaction logic for LCDControlsView.xaml
    /// </summary>
    public partial class LCDControlsView : UserControl
    {
        public LCDControlsView()
        {
            InitializeComponent();
        }

        private int lcdBacklightState = 0;
        private int _scrollSpeed;

        private void reconnectI2C_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindowViewModel.SendRequestCommand.Execute
                    (new Request("SendToLCD", "#reset"));
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request sent" });
            }
            catch (Exception ex)
            {
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request could not be sent: " + ex.Message });
            }
        }

        private void toggleBacklightButton_Click(object sender, RoutedEventArgs e)
        {
            lcdBacklightState = lcdBacklightState == 0 ? 1 : 0;
            try
            {
                MainWindowViewModel.SendRequestCommand.Execute
                    (new Request("ToggleBacklight_LCD", lcdBacklightState));
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request sent" });
            }
            catch (Exception ex)
            {
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request could not be sent: " + ex.Message });
            }
        }

        private void sendToLcdButton_Click(object sender, RoutedEventArgs e)
        {

            string text = displayEingabeTextBox.Text;

            try
            {
                MainWindowViewModel.SendRequestCommand.Execute(new Request("SendToLCD", text));
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request sent" });
            }
            catch (Exception ex)
            {
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request could not be sent: " + ex.Message });
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

            try
            {
                MainWindowViewModel.SendRequestCommand.Execute(new Request("SendToLCD", "#cancel"));
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request sent" });
            }
            catch (Exception ex)
            {
                MainWindowViewModel.AddDebugInfoCommand.Execute
                    (new DebugModel { Origin = "GPIO", Text = "Request could not be sent: " + ex.Message });
            }
        }

        private void scrollSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //not implemented yet
            //Slider slider = sender as Slider;

            //_scrollSpeed = getSpeed((int)slider.Value);
        }
    }
}
