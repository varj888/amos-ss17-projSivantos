using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Frontend.ViewModel;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for RemoteControllerView.xaml
    /// </summary>
    public partial class RemoteControllerView : UserControl
    {
        DebugViewModel vmDebug;
        RemoteControllerViewModel remoteVM;

        private readonly double _POWER_OFF = 0.0;
        private readonly double _POWER_ON = 1.3;
        public RemoteControllerView()
        {
            InitializeComponent();
            vmDebug = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        private async void press_Combination(object sender, RoutedEventArgs e)
        {
            object selectedDurationCategorie = MainWindowViewModel.CurrentViewModelRemoteController.SelectedDuration.Content;

            if (selectedDurationCategorie != null)
            {
                string[] param = new string[4];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = null;
                }

                param[param.Length - 1] = selectedDurationCategorie.ToString();

                if (Push_Checkbox.IsChecked == true)
                {
                    param[0] = "PB";
                }
                if (RockerSwitchUp_Checkbox.IsChecked == true)
                {
                    param[1] = "RSU";
                }
                if (RockerSwitchDown_Checkbox.IsChecked == true)
                {
                    param[2] = "RSD";
                }
                await remoteVM.RaspberryPiInstance.PressCombination(param);
                vmDebug.AddDebugInfo("PressCombination", "success");
                //Request request = new Request("PressCombination", param);
                //remoteVM.sendRequest(request);
                //remoteVM.getResult(request);
            }
            else
            {
                vmDebug.AddDebugInfo("Debug", "Invalid duration");
            }
        }

        private async void Power_Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Slider slide = sender as Slider;
            if (remoteVM != null)
            {
                try
                {
                    await remoteVM.RaspberryPiInstance.ChangePowerVoltage(slide.Value);
                    vmDebug.AddDebugInfo("ChangePowerVoltage", slide.Value.ToString());
                }
                catch (Exception ex)
                {
                    vmDebug.AddDebugInfo("ChangePowerVoltage", "Failed");
                }
            }
        }

        private async void Button_Power_On_OnClick(object sender, RoutedEventArgs e)
        {
            Slider slide = sender as Slider;
            if (remoteVM != null)
            {
                try
                {
                    double value = await remoteVM.RaspberryPiInstance.ChangePowerVoltage(_POWER_ON);
                    vmDebug.AddDebugInfo("ChangePowerVoltage", slide.Value.ToString());
                    remoteVM.CurrentPowerVoltage = value;
                }
                catch (Exception ex)
                {
                    vmDebug.AddDebugInfo("ChangePowerVoltage", "Failed");
                }
            }
        }

        private async void Button_Power_OFF_OnClick(object sender, RoutedEventArgs e)
        {
            Slider slide = sender as Slider;
            if (remoteVM != null)
            {
                try
                {
                    double value = await remoteVM.RaspberryPiInstance.ChangePowerVoltage(0.0);
                    vmDebug.AddDebugInfo("ChangePowerVoltage", slide.Value.ToString());
                    remoteVM.CurrentPowerVoltage = value;
                }
                catch (Exception ex)
                {
                    vmDebug.AddDebugInfo("ChangePowerVoltage", "Failed");
                }
            }
        }

        private async void SetVolume_Slider_DragCompleted(object sender, DragCompletedEventArgs dragCompletedEventArgs)
        {
            Slider slide = sender as Slider;
            if (remoteVM != null)
            {
                try
                {
                    await remoteVM.RaspberryPiInstance.SetAnalogVolume((byte)slide.Value);
                    vmDebug.AddDebugInfo("SetAnalogVolume", slide.Value.ToString());
                }
                catch (Exception ex)
                {
                    vmDebug.AddDebugInfo("SetAnalogVolume", "Failed");
                }
            }
        }

       
    }
}
