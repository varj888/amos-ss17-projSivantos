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
    public class EndlessVcDownCommand : ICommand
    {
        private DebugViewModel debugViewModel;
        private RemoteControllerViewModel remoteVM;

        public EndlessVcDownCommand()
        {
            debugViewModel = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object par)
        {
            String result;
            try
            {
                result = await remoteVM.RaspberryPiInstance.EndlessVCDown();
                debugViewModel.AddDebugInfo("Endless_VC_Down", "-1");
            }
            catch
            {
                debugViewModel.AddDebugInfo("Unknown command", "");
            }
        }
    }
}
