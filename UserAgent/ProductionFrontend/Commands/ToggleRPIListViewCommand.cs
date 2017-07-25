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
                Application.Current.MainWindow.MinWidth = 260;
                Application.Current.MainWindow.Width = 260;
                Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
                rctbVM.ToggleMenuButton_Off_Visibility = Visibility.Hidden;
                rctbVM.ToggleMenuButton_On_Visibility = Visibility.Visible;
                rcVM.MinimalViewVis = Visibility.Hidden;
                rcVM.MinimalViewGrids = new GridLength(0, GridUnitType.Auto);
            }
            else
            {
                rcVM.RPIListVisible = Visibility.Visible;
                Application.Current.MainWindow.MinWidth = 800;
                Application.Current.MainWindow.Width = WindowWidth;
                rctbVM.ToggleMenuButton_Off_Visibility = Visibility.Visible;
                rctbVM.ToggleMenuButton_On_Visibility = Visibility.Hidden;
                Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                rcVM.MinimalViewVis = Visibility.Visible;
                rcVM.MinimalViewGrids = new GridLength(1, GridUnitType.Auto);
            }
        }
    }
}
