using System;
using System.Diagnostics;
using System.Windows.Input;
using Frontend.ViewModel;

namespace Frontend.Commands
{
    public class PressPushButtonCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public PressPushButtonCommand()
        {
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            String result;
            object selectedDuration = MainWindowViewModel.CurrentViewModelRemoteController.SelectedDuration.Content;

            try
            {
                Debug.WriteLine("The selected Duration for {0} is: {1}", this.GetType().Name, selectedDuration.ToString());
                result = await remoteVM.RaspberryPiInstance.PressPushButton(selectedDuration.ToString());
                debugVM.AddDebugInfo("PressPushButton", result);
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("PressPushButton :", e.Message);
                return;
            }
        }
    }
}
