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
    public class ResetMuxCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public ResetMuxCommand()
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
            bool result;
            try
            {
                result = await remoteVM.RaspberryPiInstance.ResetMux();
                debugVM.AddDebugInfo("Multiplexer is resetted: ", result.ToString());
            }
            catch (Exception exc)
            {
                debugVM.AddDebugInfo("Reset Mux failed. ", exc.Message);
                Debug.WriteLine("Reset Mux failed. " + exc.Message + " " + exc.Source);
            }
        }
    }
}
