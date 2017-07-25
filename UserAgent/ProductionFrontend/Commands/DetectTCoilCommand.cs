using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonFiles.TransferObjects;
using TestMachineFrontend1.ViewModel;
using System.Diagnostics;
using System.Windows;

namespace TestMachineFrontend1.Commands
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
                result = await remoteVM.SelectedRaspiItem.raspi.DetectTeleCoil();
                remoteVM.TCoilDetected = true;
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

