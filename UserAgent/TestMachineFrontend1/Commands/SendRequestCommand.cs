using System;
﻿using CommonFiles.TransferObjects;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class SendRequestCommand : ICommand
    {
        private UserControlsViewModel ucVM;
        //private DetectTabViewModel dtVM;
        private DebugViewModel debugVM;
        MainWindowViewModel mwVM = MainWindowViewModel.Instance;

        public SendRequestCommand()
        {
            ucVM = MainWindowViewModel.CurrentViewModelUserControls;
            //dtVM = MainWindowViewModel.CurrentViewModelDetectTab;
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //dtVM.sendRequest(parameter as Request);
            //mwVM.sendRequest(parameter as Request);

            //Result result = dtVM.getResult(parameter as Request);

            //if (((parameter as Request).command.Equals(ucVM.DetectTCol.command))
            //    && result.value.ToString() == "High")
            //{
            //    ucVM.TCoilDetected = true;
            //    debugVM.AddDebugInfo("Update", "ToggleTeleCoil completed");

            //}
            //else if ((parameter as Request).command.Equals(ucVM.UndetectTCol.command)
            //    && result.value.ToString() == "Low")
            //{
            //    ucVM.TCoilDetected = false;
            //    debugVM.AddDebugInfo("Update", "ToggleTeleCoil completed");
            //}
        }
    }
}
