using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestmachineFrontend1;
using TestMachineFrontend1.Model;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    class RefreshRPListCommand : ICommand
    {
        private DebugViewModel vmDebug;
        private RemoteControllerViewModel remoteVM;

        public RefreshRPListCommand()
        {
            vmDebug = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            TcpClient registryServerSocket = new TcpClient();
            Request request = new Request("getRegisteredDevices", null);
            try
            {
                await registryServerSocket.ConnectAsync("MarcoPC", 54320);
            }
            catch (Exception e)
            {
                vmDebug.AddDebugInfo("[ERROR]", "Error connecting to the Registry Server: " + e.Message);
                return;
            }
            try
            {
                Transfer.sendObject(registryServerSocket.GetStream(), request);
            }catch (Exception e)
            {
                vmDebug.AddDebugInfo("[ERROR]", "Error sending to the RegistryServer: " + e.Message);
                return;
            }
            Object result;
            try
            {
                result = Transfer.receiveObject(registryServerSocket.GetStream());
            }catch(Exception e)
            {
                vmDebug.AddDebugInfo("[ERROR]", "Error receiving from the RegistryServer: " + e.Message);
                return;
            }

            if(result.GetType() == typeof(SuccessResult))
            {
                List<RaspberryPiItem> connectedList = remoteVM.BackendList.Where(item => item.Connected).ToList();
                remoteVM.BackendList.Clear();
                foreach(var entry in connectedList)
                {
                    remoteVM.BackendList.Add(entry);
                }

                SuccessResult successResult = (SuccessResult)result;
                Dictionary<string, string> dictionary = (Dictionary<string, string>)successResult.result;

                foreach (var entry in dictionary)
                {
                    remoteVM.addRaspberryPi(entry.Key, entry.Value);
                }
            }else if(result.GetType() == typeof(ExceptionResult))
            {
                vmDebug.AddDebugInfo("[ERROR]", "Error Result from the RegistryServer: " + ((ExceptionResult)result).exceptionMessage);
            }
            else
            {
                vmDebug.AddDebugInfo("[ERROR]", "Unhandled Registry Server Result Type: " + result.GetType().ToString());
            }
            
            registryServerSocket.Close();
        }
    }
}
