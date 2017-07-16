using System;
﻿using CommonFiles.TransferObjects;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class SendRequestCommand : ICommand
    {
        //private UserControlsViewModel ucVM;
        //private DetectTabViewModel dtVM;
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public SendRequestCommand()
        {
            //ucVM = MainWindowViewModel.CurrentViewModelUserControls;
            //dtVM = MainWindowViewModel.CurrentViewModelDetectTab;
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            remoteVM.sendRequest(parameter as Request);
            Result result = remoteVM.getResult(parameter as Request);

            if (((parameter as Request).command.Equals(remoteVM.DetectTCol.command))
                && result.value.ToString() == "High")
            {
                remoteVM.TCoilDetected = true;
                debugVM.AddDebugInfo("Update", "ToggleTeleCoil completed");

            }
            else if ((parameter as Request).command.Equals(remoteVM.UndetectTCol.command)
                && result.value.ToString() == "Low")
            {
                remoteVM.TCoilDetected = false;
                debugVM.AddDebugInfo("Update", "ToggleTeleCoil completed");
            }
        }
    }
}
