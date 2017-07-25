using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class SetReceiverCommand : ICommand
    {
        RemoteControllerViewModel remoteVM;
        DebugViewModel debugVM;

        public SetReceiverCommand()
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
            string receiverDevice = remoteVM.SelectedReceiverItem.Content.ToString();

            try
            {
                await remoteVM.SelectedRaspiItem.raspi.SetARDVoltage(receiverDevice);
                debugVM.AddDebugInfo("SetReceiverCommand", receiverDevice);
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("SetReceiverCommand", "Failed");
            }
        }
    }
}
