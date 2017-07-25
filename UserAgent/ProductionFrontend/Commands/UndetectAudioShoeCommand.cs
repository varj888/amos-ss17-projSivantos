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
    public class UndetectAudioShoeCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public UndetectAudioShoeCommand()
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
<<<<<<< HEAD:UserAgent/ProductionFrontend/Commands/UndetectAudioShueCommand.cs
                result = await remoteVM.SelectedRaspiItem.raspi.UndetectAudioShoe();
                //TODO Property for color-binding
                //remoteVM.TCoilDetected = false;
                debugVM.AddDebugInfo("UndetectAudioShue", result);
=======
                result = await remoteVM.RaspberryPiInstance.UndetectAudioShoe();
                remoteVM.AudioShoeUpdate = Visibility.Hidden;
                debugVM.AddDebugInfo("UndetectAudioShoe", result);
>>>>>>> master:UserAgent/ProductionFrontend/Commands/UndetectAudioShoeCommand.cs
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("UndetectAudioShoe :", e.Message);
                return;
            }
        }
    }
}

