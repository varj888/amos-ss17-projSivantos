using System;
using System.Windows.Input;
using Frontend.ViewModel;

namespace Frontend.Commands
{
    public class SetHICommand : ICommand
    {
        RemoteControllerViewModel remoteVM;
        DebugViewModel debugVM;

        public SetHICommand()
        {
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {

            string model = remoteVM.SelectedHI.Content.ToString();
            string family = remoteVM.SelectedHI.Name;

            if (model == "" || family == "")
            {
                debugVM.AddDebugInfo("SetHICommand", "selected item is not valid.");
            }

            try
            {

                await remoteVM.RaspberryPiInstance.SetHI(family, model);
                debugVM.AddDebugInfo("SetHICommand", family +","+model);
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("SetHICommand", "Failed");
            }
        }
    }
}
