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
    public class DetectAudioShoeCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public DetectAudioShoeCommand()
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
                result = await remoteVM.RaspberryPiInstance.DetectAudioShoe();
                remoteVM.AudioShueUpdate = Visibility.Visible;
                debugVM.AddDebugInfo("DetectAudioShoe", result);
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("DetectAudioShoe :", e.Message);
                return;
            }
        }
    }
}

