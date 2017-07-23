using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMachineFrontend1.Model;

namespace TestMachineFrontend1.ViewModel
{
    public class TestDebugTabViewModel
    {
        private MultiplexerModel muxModel;
        private DebugViewModel debugVM;
        private RemoteControllerViewModel remoteVM;

        public MultiplexerModel MuxModel
        { get { return muxModel; } }

        public TestDebugTabViewModel()
        {
            muxModel = new MultiplexerModel();
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public async Task ConnectPins()
        {
            String result = await remoteVM.RaspberryPiInstance.ConnectPins
                ((int)muxModel.ValueX, (int)muxModel.ValueY);
            debugVM.AddDebugInfo("Connecting pins: ", result);
        }

        public async Task ReadPin()
        {
            String result;
            try
            {
                result = await remoteVM.RaspberryPiInstance.ReadPin(muxModel.PinID);
                debugVM.AddDebugInfo("Read pin: ", result);
            }
            catch (Exception exc)
            {
                debugVM.AddDebugInfo("Read pin failed. ", exc.Message);
            }
        }

        public async Task WritePin()
        {
            String result;
            try
            {
                result = await remoteVM.RaspberryPiInstance.WritePin(muxModel.PinID);
                debugVM.AddDebugInfo("Write pin: ", result);
            }
            catch (Exception exc)
            {
                debugVM.AddDebugInfo("Write pin failed. ", exc.Message);
                Debug.WriteLine("Write pin failed. " + exc.Message);
            }
        }

        public async Task ResetPin()
        {
            String result;
            try
            {
                result = await remoteVM.RaspberryPiInstance.ResetPin(muxModel.PinID);
                debugVM.AddDebugInfo("Reset pin: ", result);
            }
            catch (Exception exc)
            {
                debugVM.AddDebugInfo("Reset pin failed. ", exc.Message);
            }
        }
    }
}
