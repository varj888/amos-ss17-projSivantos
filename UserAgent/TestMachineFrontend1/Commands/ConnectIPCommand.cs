using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.Helpers;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class ConnectIPCommand : ICommand
    {
        private DetectTabViewModel viewModel;

        public ConnectIPCommand()
        {
            viewModel = MainWindowViewModel.CurrentViewModelDetectTab;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.connectIP();
        }
    }
}
