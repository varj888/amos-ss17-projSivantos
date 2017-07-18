using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.Helpers;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class ConnectIPCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;

        public ConnectIPCommand()
        {
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            remoteVM.connectIP();
        }
    }
}
