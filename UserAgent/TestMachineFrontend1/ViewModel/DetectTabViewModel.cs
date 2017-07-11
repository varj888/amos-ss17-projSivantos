using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TestmachineFrontend1;
using TestMachineFrontend1.Helpers;
using TestMachineFrontend1.Model;

namespace TestMachineFrontend1.ViewModel
{
    public class DetectTabViewModel : ObservableObject
    {
        private Dictionary<String, RaspberryPi> raspberryPis = new Dictionary<string, RaspberryPi>();

        #region Properties
        public string PinID { get; set; }

        private string ipAdressConnect;
        public string IPAdressConnect
        {
            get { return ipAdressConnect; }
            set
            {
                ipAdressConnect = value;
                OnPropertyChanged("IPAdressConnect");
            }
        }

        private bool isPiConnected;
        public bool IsPiConnected
        {
            get { return isPiConnected; }
            set
            {
                isPiConnected = value;
                OnPropertyChanged("IsPiConnected");
            }
        }

        #region Requests
        public Request RequestLEDOn
        {
            get { return new Request("LightLED", 1); }
        }

        public Request RequestLEDOff
        {
            get { return new Request("LightLED", 0); }
        }

        public Request ConnectPins
        {
            get { return new Request("ConnectPins", 0); }
        }

        public Request ReadPin
        {
            get { return new Request("ReadPin", PinID); }
        }

        public Request WritePin
        {
            get { return new Request("WritePin", PinID); }
        }

        public Request ResetPin
        {
            get { return new Request("ResetPin", PinID); }
        }
        #endregion

        public Dictionary<String, RaspberryPi> RaspberryPis
        {
            get { return raspberryPis; }
            set
            {
                if (value != this.raspberryPis)
                {
                    this.raspberryPis = value;
                    OnPropertyChanged("RaspberryPis");
                }
            }
        }
        #endregion

        private ObservableCollection<RaspberryPiItem> backendList;

        //private DebugViewModel debugVM;
        private RaspberryPiItem detectModel;
        private DebugViewModel debugVM;

        public DetectTabViewModel(/*DebugViewModel debugVM,*/)
        {
            //this.debugVM = debugVM;
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
            ItemSelected = new DelegateCommand(o =>
            {
                SelectedRaspiItem = o as RaspberryPiItem;
            });
            backendList = new ObservableCollection<RaspberryPiItem>();

            printRegisteredDevices();
        }

        private async void printRegisteredDevices()
        {
            Dictionary<string, string> registeredDevices;
            try
            {
                registeredDevices = await getRegisteredDevices();
            }
            catch (Exception e)
            {
                Debug.WriteLine("getRegisteredDevices: " + e.Message);
                return;
            }

            foreach (var device in registeredDevices)
            {
                Debug.WriteLine(device.Key);
                Debug.WriteLine(device.Value);
            }
        }

        public RaspberryPiItem SelectedRaspiItem
        {
            get
            { return this.detectModel; }

            set
            {
                this.detectModel = value;
                OnPropertyChanged("SelectedRaspiItem");
            }
        }

        public ICommand ItemSelected { get; private set; }

        public ObservableCollection<RaspberryPiItem> BackendList
        {
            get { return backendList; }
            set
            {
                backendList = value;
                OnPropertyChanged("BackendList");
            }
        }

        public Request GetAvailableHI
        {
            get { return new Request("GetAvailableHI", 0); }
        }

        public async void connectIP()
        {
            try
            {
                var pi1 = await RaspberryPi.Create(new IPEndPoint(IPAddress.Parse(IPAdressConnect), 54321));
                IsPiConnected = pi1.IsConnected;
                raspberryPis.Add(IPAdressConnect, pi1);
                RaspberryPiItem raspiItem = new RaspberryPiItem() { Name = IPAdressConnect, Id = 45, Status = "OK", raspi = pi1 };
                backendList.Add(raspiItem);
                SelectedRaspiItem = raspiItem;
                debugVM.AddDebugInfo("[SUCCESS]", "Connection established");
                //sendRequest(GetAvailableHI);
                //Result result = getResult(GetAvailableHI);
                //MainWindowViewModel.CurrentViewModelMultiplexer.getAvailableHI(result);
                SynchronizationContext uiContext = SynchronizationContext.Current;
                await Task.Run(() => ReceiveResultLoop(uiContext));
            }
            catch (FormatException fx)
            {
                debugVM.AddDebugInfo("[ERROR]", "Invalid IP Address Format: " + fx.Message);

                //TODO check
                IsPiConnected = false;
            }
            catch (SocketException sx)
            {
                debugVM.AddDebugInfo("[ERROR]", "Couldn't establish connection: " + sx.Message);
                //TODO check
                IsPiConnected = false;

            }
            catch (Exception any)
            {
                debugVM.AddDebugInfo("[ERROR]", "Unknown Error. " + any.Message);
                //TODO check
                IsPiConnected = false;
            }
        }

        public void sendRequest(Request request)
        {
            if (this.SelectedRaspiItem == null)
            {
                debugVM.AddDebugInfo("Debug", "No raspi selected");
                return;
            }

            try
            {
                Transfer.sendObject(getClientconnection().GetStream(), request);
            }
            catch (Exception ex)
            {
                debugVM.AddDebugInfo(request.command, "Request could not be sent: " + ex.Message);
                return;
            }
        }

        private async Task ReceiveResultLoop(SynchronizationContext uiContext)
        {
            while (true)
            {
                Result result;

                try
                {
                    result = await Transfer.receiveObjectAsync<Result>(getClientconnection().GetStream());
                }catch(Exception e)
                {
                    uiContext.Send((object state) => debugVM.AddDebugInfo("ResultLoop", "Result could not be received: " + e.Message), null);
                    return;
                }

                if(result.exceptionMessage == null)
                {
                    uiContext.Send((object state) => debugVM.AddDebugInfo(result.value.ToString(), "sucess"), null);
                    string updateMethodName = "updateGui_" + result.obj.ToString();
                    typeof(MainWindowViewModel).GetMethod(updateMethodName).Invoke(MainWindowViewModel.Instance, new object[] { result });
                }
                else
                {
                    uiContext.Send((object state) => debugVM.AddDebugInfo(result.value.ToString(), result.exceptionMessage), null);
                }
            }
        }

        private async Task<Dictionary<string, string>> getRegisteredDevices()
        {
            TcpClient registryServerSocket = new TcpClient();
            await registryServerSocket.ConnectAsync("MarcoPC", 54320);
            Request request = new Request("getRegisteredDevices", new object[] { });
            Transfer.sendObject(registryServerSocket.GetStream(), request);
            Result result = await Transfer.receiveObjectAsync<Result>(registryServerSocket.GetStream());
            registryServerSocket.Close();
            return (Dictionary<string, string>)result.value;
        }

        public TcpClient getClientconnection()
        {
            if (this.SelectedRaspiItem == null && this.BackendList.Count > 0)
            {
                this.SelectedRaspiItem = this.BackendList.ElementAt(0);
            }
            var c = (RaspberryPiItem)this.SelectedRaspiItem;
            return c.raspi.socket;
        }
    }
}
