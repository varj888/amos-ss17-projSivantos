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
    public class PressRockerSwitchCommand : ICommand
    {
        //private UserControlsViewModel ucViewModel;
        //private DetectTabViewModel dtViewModel;
        private DebugViewModel debugViewModel;
        private RemoteControllerViewModel remoteVM;

        public PressRockerSwitchCommand()
        {
            //ucViewModel = MainWindowViewModel.CurrentViewModelUserControls;
            //dtViewModel = MainWindowViewModel.CurrentViewModelDetectTab;
            debugViewModel = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (remoteVM.getDuration() != -1)
            {
                remoteVM.sendRequest(parameter as Request);
                remoteVM.getResult(parameter as Request);
            }
            else
            {
                debugViewModel.AddDebugInfo("Debug", "Invalid duration");
            }
        }
    }
}
