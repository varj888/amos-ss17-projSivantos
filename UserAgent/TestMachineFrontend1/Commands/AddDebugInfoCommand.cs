using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.Model;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class AddDebugInfoCommand : ICommand
    {
        public DebugViewModel ViewModel { get; private set; }

        public AddDebugInfoCommand(DebugViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.AddDebugInfo((parameter as DebugModel).Origin, (parameter as DebugModel).Text);
        }
    }
}
