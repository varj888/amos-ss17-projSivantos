using System;
﻿using CommonFiles.TransferObjects;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class SendRequestCommand : ICommand
    {
        public DetectTabViewModel ViewModel { get; private set; }

        public SendRequestCommand(DetectTabViewModel viewModel)

        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.sendRequest(parameter as Request);
        }
    }
}
