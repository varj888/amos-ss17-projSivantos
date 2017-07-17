using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    class LEDOnCommand : ICommand
    {
        RemoteControllerViewModel remoteVM;
        DebugViewModel debugVM;
        public LEDOnCommand()
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
            String result;
            try
            {
                result = await remoteVM.RaspberryPiInstance.LightLED(1);
                debugVM.AddDebugInfo("LightLED", result);
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("LightLED :", e.Message);
                return;
            }
            //Debug.WriteLine("LEDOn: ", result);
        }
    }
}
