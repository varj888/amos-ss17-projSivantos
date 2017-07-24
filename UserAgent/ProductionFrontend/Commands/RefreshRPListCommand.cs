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
                remoteVM.BackendList = new ObservableCollection<RaspberryPiItem>(remoteVM.BackendList.Where(item => item.Connected));
                Dictionary<IPEndPoint, RaspberryPiItem> backendListDictionary = remoteVM.BackendList.ToDictionary(item => item.endpoint, item => item);
  

                SuccessResult successResult = (SuccessResult)result;
                Dictionary<string, string> dictionary = (Dictionary<string, string>)successResult.result;

                foreach (var entry in dictionary)
                {
                    IPAddress address;
                    try
                    {
                        address = IPAddress.Parse(entry.Key);
                    }
                    catch (FormatException fx)
                    {
                        vmDebug.AddDebugInfo("[ERROR]", "Invalid IP Address Format from the RegistryServer: " + fx.Message);
                        continue;
                    }
                    IPEndPoint endpoint = new IPEndPoint(address, 54321);
                    if (!backendListDictionary.ContainsKey(endpoint)){
                        RaspberryPi raspi = new RaspberryPi();
                        RaspberryPiItem raspiItem = new RaspberryPiItem() { endpoint = endpoint, Status = entry.Value, raspi = raspi };
                        remoteVM.BackendList.Add(raspiItem);
                    }
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
