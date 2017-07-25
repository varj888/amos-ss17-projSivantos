using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    class DisconnectCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;

        public event EventHandler CanExecuteChanged;

        public DisconnectCommand()
        {
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await remoteVM.SelectedRaspiItem.raspi.Disconnect();
        }
    }
}
