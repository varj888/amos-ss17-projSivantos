using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Frontend.ViewModel;

namespace Frontend.Commands
{
    public class EndlessVcUpCommand : ICommand
    {
        private DebugViewModel debugViewModel;
        private RemoteControllerViewModel remoteVM;

        public EndlessVcUpCommand()
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
            int ticks = 0;
            try
            {
                ticks = Convert.ToInt32((string)par);
                if(ticks < 0) throw new ArgumentException();
            }
            catch (Exception)
            {
                debugViewModel.AddDebugInfo("EndlessVcUp", "Given ticks input is not valid.");
                return;
            }

            try
            {
                await remoteVM.RaspberryPiInstance.EndlessVCUp(ticks);
                debugViewModel.AddDebugInfo("EndlessVcUp", ticks + " times");
            }
            catch
            {
                debugViewModel.AddDebugInfo("EndlessVcUp", "failed");
            }           
        }
    }
}
