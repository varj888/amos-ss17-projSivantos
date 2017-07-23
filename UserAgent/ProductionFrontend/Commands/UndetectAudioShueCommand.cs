using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonFiles.TransferObjects;
using TestMachineFrontend1.ViewModel;
using System.Diagnostics;

namespace TestMachineFrontend1.Commands
{
    public class UndetectAudioShueCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public UndetectAudioShueCommand()
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
                result = await remoteVM.SelectedRaspiItem.raspi.UndetectAudioShoe();
                //TODO Property for color-binding
                //remoteVM.TCoilDetected = false;
                debugVM.AddDebugInfo("UndetectAudioShue", result);
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("UndetectAudioShue :", e.Message);
                return;
            }
        }
    }
}

