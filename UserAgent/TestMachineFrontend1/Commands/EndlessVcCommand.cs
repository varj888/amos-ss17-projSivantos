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
        public DetectTabViewModel DtViewModel { get; private set; }
        public DebugViewModel DebugViewModel { get; private set; }

        public EndlessVcCommand(DetectTabViewModel dtViewModel, DebugViewModel debugViewModel)
        {
            DtViewModel = dtViewModel;
            DebugViewModel = debugViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object par)
        {
            DtViewModel.sendRequest(par as Request);

            if((par as Request).command.Equals("EndlessVCUp"))
            {
                DebugViewModel.AddDebugInfo("Endless_VC_Up", "+1");
            }
            else if((par as Request).command.Equals("EndlessVCUp"))
            {
                DebugViewModel.AddDebugInfo("Endless_VC_Down", "-1");
            }
            else
            {
                DebugViewModel.AddDebugInfo("Unknown command", "");
            }
        }
    }
}
