using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class ToggleLEDCommand : ICommand
    {
        RemoteControllerViewModel remoteVM;
        DebugViewModel debugVM;

        public ToggleLEDCommand()
        {
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public async void Execute(object parameter)
        {
            String result = "";
            try
            {
                
                result = await remoteVM.RaspberryPiInstance.ToggleLED();
                debugVM.AddDebugInfo("ToggleLED", result);
            }
            catch (Exception e)
            {

            }

            if (result == "High")
            {
                remoteVM.ToggleLEDButton = Visibility.Visible;
            }
            else
            {
                remoteVM.ToggleLEDButton = Visibility.Hidden;
            }


        }
    }
}
