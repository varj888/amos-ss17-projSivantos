using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.View
{
    /// <summary>
    /// Interaction logic for RemoteControllerView.xaml
    /// </summary>
    public partial class RemoteControllerView : UserControl
    {
        DebugViewModel vmDebug;
        RemoteControllerViewModel remoteVM;
        public RemoteControllerView()
        {
            InitializeComponent();
            vmDebug = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        //TODO
        private async void press_Combination(object sender, RoutedEventArgs e)
        {
            if (remoteVM.getDuration() != -1)
            {
                int[] param = new int[4];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = 0;
                }
                param[param.Length - 1] = remoteVM.getDuration();

                int duration = remoteVM.getDuration();
                if (Push_Checkbox.IsChecked == true)
                {
                    param[0] = 1;
                }
                if (RockerSwitchUp_Checkbox.IsChecked == true)
                {
                    param[1] = 1;
                }
                if (RockerSwitchDown_Checkbox.IsChecked == true)
                {
                    param[2] = 1;
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

        private async void Power_Slider_OnValueChanged(object sender, RoutedEventArgs e)
        {
            Slider slide = sender as Slider;
            await remoteVM.RaspberryPiInstance.ChangePowerVoltage(slide.Value);
            vmDebug.AddDebugInfo("ChangePowerVoltage", slide.Value.ToString());
        }

        private void Button_Power_On_OnClick(object sender, RoutedEventArgs e)
        {
            remoteVM.CurrentPowerVoltage = 1.3;
        }

        private void Button_Power_OFF_OnClick(object sender, RoutedEventArgs e)
        {
            remoteVM.CurrentPowerVoltage = 0.0;
        }
    }
}
