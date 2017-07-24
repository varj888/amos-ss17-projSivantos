using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Frontend.ViewModel;

namespace Frontend.Commands
{
    class ToggleRPIListViewCommand : ICommand
    {
        private MainWindowViewModel mainVM;
        public event EventHandler CanExecuteChanged;

        public ToggleRPIListViewCommand()
        {
            //remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
            mainVM = MainWindowViewModel.Instance;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Toggling RPIList");
            //  remoteVM.RPIListVisible = Visibility.Visible;
            if (mainVM.RPIListVisible == Visibility.Hidden)
            {
                mainVM.RPIListVisible = Visibility.Visible;
                
            }
            else
            {
                mainVM.RPIListVisible = Visibility.Hidden;
            }
        }
    }
}
