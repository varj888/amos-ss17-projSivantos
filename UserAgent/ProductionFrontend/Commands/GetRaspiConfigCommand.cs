using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonFiles.TransferObjects;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class GetRaspiConfigCommand : ICommand
    {
        private RemoteControllerViewModel remoteVM;
        private DebugViewModel debugVM;

        public GetRaspiConfigCommand()
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
            string raspiConfig = "";
            try
            {

                raspiConfig = await remoteVM.SelectedRaspiItem.raspi.GetRaspiConfig();
                debugVM.AddDebugInfo("ReadRaspiConfig", "Success");
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo("ReadRaspiConfig", "Failed");
            }

            remoteVM.RaspiConfigString = raspiConfig;
        }
    }
}