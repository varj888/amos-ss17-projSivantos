using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;
using TestmachineFrontend;
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
                sendRequest(GetAvailableHI);
                Result result = getResult(GetAvailableHI);
                MainWindowViewModel.CurrentViewModelMultiplexer.getAvailableHI(result);
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
            //Result result = getResult(request);
            //Result result;

            //try
            //{
            //    result = getClientconnection().receiveObject();
            //}
            //catch (Exception e)
            //{
            //    debugVM.AddDebugInfo(request.command, "Result could not be received: " + e.Message);
            //    return;
            //}

            //if (result.exceptionMessage == null)
            //{
            //    debugVM.AddDebugInfo(request.command, "sucess");
            //}
            //else
            //{
            //    debugVM.AddDebugInfo(request.command, result.exceptionMessage);
            //}

        }

        public Result getResult(Request request)
        {
            Result result = null;

            try
            {
                result = Transfer.receiveObject<Result>(getClientconnection().GetStream());
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo(request.command, "Result could not be received: " + e.Message);
            }

            if (result.exceptionMessage == null)
            {
                debugVM.AddDebugInfo(request.command, "sucess");
            }
            else
            {
                debugVM.AddDebugInfo(request.command, result.exceptionMessage);
            }
            return result;
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
