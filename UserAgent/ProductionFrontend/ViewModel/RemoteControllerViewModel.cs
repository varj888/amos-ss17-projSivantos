using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TestmachineFrontend1;
using TestMachineFrontend1.Helpers;
using TestMachineFrontend1.Model;

namespace TestMachineFrontend1.ViewModel
{
    public class RemoteControllerViewModel : ObservableObject
    {
        #region VarDefinitions
        private RaspberryPiItem detectModel;
        private DebugViewModel debugVM;
        private Dictionary<string, List<string>> availableHI;
        private HelperXML helperXML;
        private Dictionary<String, RaspberryPi> raspberryPis = new Dictionary<string, RaspberryPi>();
        #endregion

        public RemoteControllerViewModel()
        {
            //this.debugVM = debugVM;
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
            availableHI = new Dictionary<string, List<string>>();
            HIListItems = new ObservableCollection<ComboBoxItem>();
            helperXML = new HelperXML();
            ItemSelected = new DelegateCommand(o =>
            {
                SelectedRaspiItem = o as RaspberryPiItem;
            });
            backendList = new ObservableCollection<RaspberryPiItem>();

            initDurationComboBox();
            initReceiverComboBox();
        }

        #region Properties

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

        private ObservableCollection<ComboBoxItem> _hiListItems;

        public ObservableCollection<ComboBoxItem> HIListItems
        {
            get { return _hiListItems; }
            set
            {
                _hiListItems = value;
                OnPropertyChanged("HIListItems");
            }
        }

        private int _selectedHIIndex;
        public int SelectedHIIndex
        {
            get { return _selectedHIIndex; }
            set
            {
                _selectedHIIndex = value;
                OnPropertyChanged("SelectedHIIndex");
            }
        }

        private ComboBoxItem _selectedHI;
        public ComboBoxItem SelectedHI
        {
            get { return _selectedHI; }
            set
            {
                _selectedHI = value;
                _selectedHIIndex = HIListItems.IndexOf(_selectedHI);
                OnPropertyChanged("SelectedHI");
                //setHI();
            }
        }

        private ObservableCollection<ComboBoxItem> _receiverItems;

        public ObservableCollection<ComboBoxItem> ReceiverItems
        {
            get { return _receiverItems; }
            set
            {
                _receiverItems = value;
                OnPropertyChanged("ReceiverItems");
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

        private ObservableCollection<RaspberryPiItem> backendList;
        public ObservableCollection<RaspberryPiItem> BackendList
        {
            get { return backendList; }
            set
            {
                backendList = value;
                OnPropertyChanged("BackendList");
            }
        }

        private ObservableCollection<ComboBoxItem> _durationItems;
        public ObservableCollection<ComboBoxItem> DurationItems
        {
            get { return _durationItems; }
            set
            {
                _durationItems = value;
                OnPropertyChanged("DurationItems");
            }
        }

        private ComboBoxItem _selectedDuration;
        public ComboBoxItem SelectedDuration
        {
            get { return _selectedDuration; }
            set
            {
                _selectedDuration = value;
                _selectedDurationIndex = DurationItems.IndexOf(_selectedDuration);
                OnPropertyChanged("SelectedDuration");
            }
        }

        public Visibility _tcoilUpdate = Visibility.Hidden;

        public Visibility TcoilUpdate
        {
            get { return _tcoilUpdate; }
            set
            {
                _tcoilUpdate = value;
                OnPropertyChanged("TcoilUpdate");
            }
        }

        public Visibility _audioShueUpdate = Visibility.Hidden;

        public Visibility AudioShueUpdate
        {
            get { return _audioShueUpdate; }
            set
            {
                _audioShueUpdate = value;
                OnPropertyChanged("AudioShueUpdate");
            }
        }

        public Visibility _toggleLEDButton = Visibility.Hidden;

        public Visibility ToggleLEDButton
        {
            get { return _toggleLEDButton; }
            set
            {
                _toggleLEDButton = value;
                OnPropertyChanged("ToggleLEDButton");
            }
        }

        private int _selectedDurationIndex;
        public int SelectedDurationIndex
        {
            get { return _selectedDurationIndex; }
            set
            {
                _selectedDurationIndex = value;
                OnPropertyChanged("SelectedDurationIndex");
            }
        }

        //TODO: find the way to run this async!
        //option: button on the right side of receiverListBox
        private ComboBoxItem  _selectedReceiverItem;
        public ComboBoxItem SelectedReceiverItem
        {
            get { return _selectedReceiverItem; }
            set
            {
                _selectedReceiverItem = value;
                _selectedReceiverItemIndex = DurationItems.IndexOf(_selectedReceiverItem);
                OnPropertyChanged("SelectedReceiverItem");
                //SetARDVoltageAsync().RunSynchronously();
                //Task<string> task = RaspberryPiInstance.SetARDVoltage((ContentControl)_selectedReceiverItem.Content);
                //task.Result;
                //Request request = new Request("SetARDVoltage", _selectedReceiverItem.Content);
                //sendRequest(request);
                //kann nicht bevor der Initialisierung des Receiver aufgerufen werden!!!
                //dtVM.getResult(request);
            }
        }

        private int _selectedReceiverItemIndex;
        public int SelectedReceiverItemIndex
        {
            get { return _selectedReceiverItemIndex; }
            set
            {
                _selectedReceiverItemIndex = value;
                OnPropertyChanged("SelectedReceiverItemIndex");
            }
        }

        private bool tCoilDetected;
        public bool TCoilDetected
        {
            get { return tCoilDetected; }
            set
            {
                tCoilDetected = value;
                OnPropertyChanged("TCoilDetected");
            }
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

        public Visibility isPiConnectedStatus = Visibility.Hidden;
        public Visibility IsPiConnectedStatus
        {
            get { return isPiConnectedStatus; }
            set
            {
                isPiConnectedStatus = value;
                OnPropertyChanged("IsPiConnectedStatus");
            }
        }

        private Visibility isPiDisconnected = Visibility.Visible;
        public Visibility IsPiDisconnected
        {
            get { return isPiDisconnected; }
            set
            {
                isPiDisconnected = value;
                OnPropertyChanged("IsPiDisconnected");
            }
        }

        public bool IsRockerSwitchUp { get; set; }
        public bool IsRockerSwitchDown { get; set; }
        public bool IsPushButtonUp { get; set; }
        #endregion

        #region Commands
        public ICommand ItemSelected { get; private set; }
        #endregion

        #region Requests
        public Request PressPushButton
        {
            get { return new Request("PressPushButton", getDuration()); }
        }

        public Request DetectTCol
        {
            get { return new Request("EnableTeleCoil", 1); }
        }

        public Request UndetectTCol
        {
            get { return new Request("EnableTeleCoil", 0); }
        }

        public Request DetectAudioShoe
        {
            get { return new Request("EnableAudioShoe", 1); }
        }

        public Request UndetectAudioShoe
        {
            get { return new Request("EnableAudioShoe", 0); }
        }

        public Request Endless_VC_Up
        {
            get { return new Request("EndlessVCUp", new int[] { }); }
        }

        public Request Endless_VC_Down
        {
            get { return new Request("EndlessVCDown", new int[] { }); }
        }

        public Request GetAvailableHI
        {
            get { return new Request("GetAvailableHI", 0); }
        }
        #endregion

        #region Methods

        /// <summary>

        /// todo: use the raspberry Pi dictionary or something like that

        /// </summary>

        public static RaspberryPi raspberryPi;
        public RaspberryPi RaspberryPiInstance
        {
            get { return raspberryPi; }
            set
            {
                raspberryPi = value;
                //OnPropertyChanged("RaspberryPiInstance");
            }
        }

        private string _raspiConfigString;
        public string RaspiConfigString {
            get { return _raspiConfigString;  }
            set
            {
                _raspiConfigString = value;
                OnPropertyChanged("RaspiConfigString");
            }
        }

        private double _currentPowerVoltage = 1.0;
        public double CurrentPowerVoltage
        {
            get { return _currentPowerVoltage; }
            set
            {
                _currentPowerVoltage = value;
                OnPropertyChanged("CurrentPowerVoltage");
            }
        }

        private string _endlessVcTicks = "";
        public string EndlessVcTicks
        {
            get { return _endlessVcTicks; }
            set
            {
                _endlessVcTicks = value;
                OnPropertyChanged("EndlessVcTicks");
            }
        } 


        public async void connectIP()
        {
            try

            {

                var pi1 = await RaspberryPi.CreateAsync(new IPEndPoint(IPAddress.Parse(IPAdressConnect), 54321));

                raspberryPi = pi1;
                IsPiConnected = true;
                IsPiConnectedStatus = Visibility.Visible;
                IsPiDisconnected = Visibility.Hidden;

                raspberryPis.Add(IPAdressConnect, pi1);

                RaspberryPiItem raspiItem = new RaspberryPiItem() { Name = IPAdressConnect, Id = 45, Status = "OK", raspi = pi1 };

                BackendList.Add(raspiItem);

                SelectedRaspiItem = raspiItem;

                debugVM.AddDebugInfo("[SUCCESS]", "Connection established");

                String result = await RaspberryPiInstance.GetAvailableHI();

                getAvailableHI(result);

                SynchronizationContext uiContext = SynchronizationContext.Current;
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

        public void getAvailableHI(string result)
        {
            availableHI = helperXML.buildDictionary(result);
            foreach (string family in availableHI.Keys)
            {
                foreach (string model in availableHI[family])
                {
                    ComboBoxItem element = new ComboBoxItem();
                    element.Name = family;
                    element.Content = model;
                    HIListItems.Add(element);
                }
            }
            OnPropertyChanged("HIListItems");
        }

        private void initDurationComboBox()
        {
            DurationItems = new ObservableCollection<ComboBoxItem>();
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = "Short";
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Medium";
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Long";
            DurationItems.Add(item1);
            DurationItems.Add(item2);
            DurationItems.Add(item3);
            SelectedDuration = DurationItems.First();
        }

        private void initReceiverComboBox()
        {
            ReceiverItems = new ObservableCollection<ComboBoxItem>();
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = "Small Right";
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Small Left";
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Medium Right";
            ComboBoxItem item4 = new ComboBoxItem();
            item4.Content = "Medium Left";
            ComboBoxItem item5 = new ComboBoxItem();
            item5.Content = "Power Right";
            ComboBoxItem item6 = new ComboBoxItem();
            item6.Content = "Power Left";
            ComboBoxItem item7 = new ComboBoxItem();
            item7.Content = "High Power Right";
            ComboBoxItem item8 = new ComboBoxItem();
            item8.Content = "High Power Left";
            ComboBoxItem item9 = new ComboBoxItem();
            item9.Content = "Defective";
            ComboBoxItem item10 = new ComboBoxItem();
            item10.Content = "No Receiver";
            ReceiverItems.Add(item1);
            ReceiverItems.Add(item2);
            ReceiverItems.Add(item3);
            ReceiverItems.Add(item4);
            ReceiverItems.Add(item5);
            ReceiverItems.Add(item6);
            ReceiverItems.Add(item7);
            ReceiverItems.Add(item8);
            ReceiverItems.Add(item9);
            ReceiverItems.Add(item10);
            SelectedReceiverItem = ReceiverItems.First();
        }

        public int getDuration()
        {
            if (SelectedDurationIndex < 0)
            {
                return -1;
            }
            var a = SelectedDuration;
            UInt16 duration;
            switch (a.Content)
            {
                case "Short":
                    duration = 50;
                    break;
                case "Medium":
                    duration = 500;
                    break;
                case "Long":
                    duration = 3000;
                    break;
                default:
                    return -1;
            }
            return duration;
        }
        #endregion
    }
}
