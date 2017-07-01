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
        public UserControlsViewModel UcViewModel { get; private set; }
        public DetectTabViewModel DtViewModel { get; private set; }
        public DebugViewModel DebugViewModel { get; private set; }

        public PressRockerSwitchCommand(UserControlsViewModel ucViewModel,
            DetectTabViewModel dtViewModel, DebugViewModel debugViewModel)
        {
            UcViewModel = ucViewModel;
            DtViewModel = dtViewModel;
            DebugViewModel = debugViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (UcViewModel.getDuration() != -1)
            {
                DtViewModel.sendRequest(parameter as Request);
            }
            else
            {
                DebugViewModel.AddDebugInfo("Debug", "Invalid duration");
            }
        }
    }
}
