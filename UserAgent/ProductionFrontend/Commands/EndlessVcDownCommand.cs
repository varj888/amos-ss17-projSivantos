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
            int ticks = 0;
            try
            {
                ticks = Convert.ToInt32((string) par);
                if (ticks < 0) throw new ArgumentException();
            }
            catch (Exception)
            {
                debugViewModel.AddDebugInfo("EndlessVcDown", "Given ticks input is not valid.");
                return;
            }
            
            try
            {
                await remoteVM.RaspberryPiInstance.EndlessVCDown(ticks);
                debugViewModel.AddDebugInfo("EndlessVcDown", ticks +" times");
            }
            catch
            {
                debugViewModel.AddDebugInfo("EndlessVcDown", "Failed");
            }
        }
    }
}
