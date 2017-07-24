using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonFiles.TransferObjects;
using Frontend.ViewModel;
using System.Diagnostics;
using System.Windows;

namespace Frontend.Commands
{
    public class DetectTCoilCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public DetectTCoilCommand()
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
            try
            {
                result = await remoteVM.RaspberryPiInstance.DetectTeleCoil();
                remoteVM.TcoilUpdate = Visibility.Visible;
                debugVM.AddDebugInfo("DetectTeleCoil", result);
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("DetectTeleCoil :", e.Message);
                return;
            }
        }
    }
}

