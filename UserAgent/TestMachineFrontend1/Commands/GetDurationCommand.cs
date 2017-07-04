using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class GetDurationCommand : ICommand
    {
        private UserControlsViewModel viewModel;

        public GetDurationCommand()
        {
            viewModel = MainWindowViewModel.CurrentViewModelUserControls;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.getDuration();
        }
    }
}
