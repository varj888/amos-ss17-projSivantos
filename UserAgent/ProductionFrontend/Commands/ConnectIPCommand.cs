using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Frontend.Helpers;
using Frontend.ViewModel;

namespace Frontend.Commands
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
