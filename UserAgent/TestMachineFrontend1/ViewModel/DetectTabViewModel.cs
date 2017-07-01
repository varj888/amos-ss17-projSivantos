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

        private DebugViewModel debugVM;
        private RaspberryPiItem detectModel;

        public DetectTabViewModel(DebugViewModel debugVM)
        {
            this.debugVM = debugVM;
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
                if (value != this.detectModel)
                {
                    this.detectModel = value;
                    OnPropertyChanged("SelectedRaspiItem");
                }
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

        public async void connectIP()
        {
            try
            {
                var pi1 = await RaspberryPi.Create(new IPEndPoint(IPAddress.Parse(IPAdressConnect), 54321)); // asynchronously creates and initializes an instance of RaspberryPi
                //TODO check
                IsPiConnected = pi1.IsConnected;
                raspberryPis.Add(IPAdressConnect, pi1);
                backendList.Add(new RaspberryPiItem() { Name = IPAdressConnect, Id = 45, Status = "OK", raspi = pi1 });
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
            try
            {
                //ClientSkeleton clientSkeletion = await ClientSkeleton.createClientSkeletonAsync(new IPEndPoint(IPAddress.Parse(IPaddress), 54322));
                //await Task.Factory.StartNew(() => clientSkeletion.runRequestLoop(testCallee));
            }
            catch (Exception any)
            {
                debugVM.AddDebugInfo("Error", "Error connecting the ClientSkeleton: " + any.Message);
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
                getClientconnection().sendObject(request);
            }
            catch (Exception ex)
            {
                debugVM.AddDebugInfo(request.command, "Request could not be sent: " + ex.Message);
                return;
            }

            Result result;

            try
            {
                result = getClientconnection().receiveObject();
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo(request.command, "Result could not be received: " + e.Message);
                return;
            }

            if (result.exceptionMessage == null)
            {
                debugVM.AddDebugInfo(request.command, "sucess");
            }
            else
            {
                debugVM.AddDebugInfo(request.command, result.exceptionMessage);
            }
        }

        private ClientConn<Result, Request> getClientconnection()
        {
            if (this.SelectedRaspiItem == null && this.BackendList.Count > 0)
            {
                this.SelectedRaspiItem = this.BackendList.ElementAt(0);
            }
            var c = (RaspberryPiItem)this.SelectedRaspiItem;
            return c.raspi.clientConnection;
        }
    }
}
