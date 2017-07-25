using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    class ToggleRPIListViewCommand : ICommand
    {
        private MainWindowViewModel mainVM;
        public event EventHandler CanExecuteChanged;
        private RemoteControllerViewModel rcVM;
        private RemoteControllerTitleBarViewModel rctbVM;
        private double WindowWidth;

        public ToggleRPIListViewCommand()
        {
            //remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
            mainVM = MainWindowViewModel.Instance;
            rcVM = MainWindowViewModel.CurrentViewModelRemoteController;
            rctbVM = MainWindowViewModel.CurrentViewModelRemoteControllerTitleBar;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("[x] Toggling RPIList");
            //  remoteVM.RPIListVisible = Visibility.Visible;
            if (rcVM.RPIListVisible == Visibility.Visible)
            {
                rcVM.RPIListVisible = Visibility.Collapsed;
                WindowWidth = Application.Current.MainWindow.Width;
                Application.Current.MainWindow.MinWidth = 520;
                Application.Current.MainWindow.Width = Application.Current.MainWindow.Width - 520;
                rctbVM.ToggleMenuButton_Off_Visibility = Visibility.Hidden;
                rctbVM.ToggleMenuButton_On_Visibility = Visibility.Visible;
            }
            else
            {
                rcVM.RPIListVisible = Visibility.Visible;
                Application.Current.MainWindow.MinWidth = 800;
                Application.Current.MainWindow.Width = WindowWidth;
                rctbVM.ToggleMenuButton_Off_Visibility = Visibility.Visible;
                rctbVM.ToggleMenuButton_On_Visibility = Visibility.Hidden;
            }
        }
    }
}
