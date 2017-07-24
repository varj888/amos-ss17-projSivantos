using System;
using System.Diagnostics;
using System.Windows.Input;
using Frontend.ViewModel;

namespace Frontend.Commands
{
    public class PressRockerSwitchDownCommand : ICommand
    {
        private DebugViewModel debugViewModel;
        private RemoteControllerViewModel remoteVM;

        public PressRockerSwitchDownCommand()
        {
            debugViewModel = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            object selectedDuration = MainWindowViewModel.CurrentViewModelRemoteController.SelectedDuration.Content;

            if (selectedDuration != null)
            {
                Debug.WriteLine("The selected Duration for {0} is: {1}", this.GetType().Name, selectedDuration.ToString());
                await remoteVM.RaspberryPiInstance.PressRockerSwitchDown(selectedDuration.ToString());
                debugViewModel.AddDebugInfo("PressRockerSwitchDown", "success");
            }
            else
            {
                debugViewModel.AddDebugInfo("Debug", "Invalid duration");
            }
        }
    }
}
