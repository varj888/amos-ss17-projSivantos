using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class PressRockerSwitchUpCommand : ICommand
    {
        private DebugViewModel debugViewModel;
        private RemoteControllerViewModel remoteVM;

        public PressRockerSwitchUpCommand()
        {
            debugViewModel = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (remoteVM.getDuration() != -1)
            {
                await remoteVM.RaspberryPiInstance.PressRockerSwitchUp(remoteVM.getDuration());
                debugViewModel.AddDebugInfo("PressRockerSwitchUp", "success");
            }
            else
            {
                debugViewModel.AddDebugInfo("Debug", "Invalid duration");
            }
        }
    }
}
