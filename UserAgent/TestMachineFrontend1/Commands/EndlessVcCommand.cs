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
    public class EndlessVcCommand : ICommand
    {
        private DetectTabViewModel dtViewModel;
        private DebugViewModel debugViewModel;

        public EndlessVcCommand()
        {
            dtViewModel = MainWindowViewModel.CurrentViewModelDetectTab;
            debugViewModel = MainWindowViewModel.CurrentViewModelDebug;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object par)
        {
            dtViewModel.sendRequest(par as Request);
            dtViewModel.getResult(par as Request);

            if ((par as Request).command.Equals("EndlessVCUp"))
            {
                debugViewModel.AddDebugInfo("Endless_VC_Up", "+1");
            }
            else if((par as Request).command.Equals("EndlessVCUp"))
            {
                debugViewModel.AddDebugInfo("Endless_VC_Down", "-1");
            }
            else
            {
                debugViewModel.AddDebugInfo("Unknown command", "");
            }
        }
    }
}
