using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommonFiles.TransferObjects;
using Frontend.ViewModel;

namespace Frontend.Commands
{
    public class CheckLEDStatusCommand : ICommand
    {
        RemoteControllerViewModel remoteVM;
        DebugViewModel debugVM;

        public CheckLEDStatusCommand()
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
            bool ledStatus = false;
            try
            {

                ledStatus = await remoteVM.RaspberryPiInstance.CheckLEDStatus();
                debugVM.AddDebugInfo("CheckLEDStatus", ledStatus.ToString());
            }
            catch (Exception e)
            {

            }

            if (ledStatus)
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