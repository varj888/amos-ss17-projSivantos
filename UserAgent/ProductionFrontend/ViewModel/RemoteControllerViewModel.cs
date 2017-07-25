using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using TestmachineFrontend1;
using TestMachineFrontend1.Helpers;
using TestMachineFrontend1.Model;
using static RaspberryBackend.ReceiverConfig;

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

        private async Task<string> getRaspiModel()
        {
            return await SelectedRaspiItem.raspi.GetRaspiModel();
        }

        private async Task<string> getRaspiFamily()
        {
            return await SelectedRaspiItem.raspi.GetRaspiFamily();
        }

        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        private string family;
        public string Family
        {
            get { return family; }
            set
            {
                family = value;
                OnPropertyChanged("Family");
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

        public Visibility _AudioShoeUpdate = Visibility.Hidden;

        public Visibility AudioShoeUpdate
        {
            get { return _AudioShoeUpdate; }
            set
            {
                _AudioShoeUpdate = value;
                OnPropertyChanged("AudioShoeUpdate");
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
        private ComboBoxItem _selectedReceiverItem;
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

        //public Dictionary<String, RaspberryPi> RaspberryPis
        //{
        //    get { return raspberryPis; }
        //    set
        //    {
        //        if (value != this.raspberryPis)
        //        {
        //            this.raspberryPis = value;
        //            OnPropertyChanged("RaspberryPis");
        //        }
        //    }
        //}

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

        public Visibility _RPIListVisible = Visibility.Visible;
        public Visibility RPIListVisible
        {
            get { return _RPIListVisible; }
            set
            {
                _RPIListVisible = value;
                OnPropertyChanged("RPIListVisible");
            }
        }
        private Visibility _MinimalViewVis = Visibility.Visible;
        public Visibility MinimalViewVis
        {
            get { return _MinimalViewVis; }
            set
            {
                _MinimalViewVis = value;
                OnPropertyChanged("MinimalViewVis");
            }
        }
        private GridLength _MinimalViewGrids = new GridLength(1, GridUnitType.Auto);
        public GridLength MinimalViewGrids
        {
            get { return _MinimalViewGrids; }
            set
            {
                _MinimalViewGrids = value;
                OnPropertyChanged("MinimalViewGrids");
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

        private Visibility _DebugVisible = Visibility.Collapsed;
        public Visibility DebugVisible
        {
            get { return _DebugVisible; }
            set
            {
                _DebugVisible = value;
                OnPropertyChanged("DebugVisible");
            }
        }

        public bool IsRockerSwitchUp { get; set; }
        public bool IsRockerSwitchDown { get; set; }
        public bool IsPushButtonUp { get; set; }
        #endregion

        #region Commands
        public ICommand ItemSelected { get; private set; }
        #endregion

        #region Methods

        private string _raspiConfigString;
        public string RaspiConfigString
        {
            get { return _raspiConfigString; }
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
        public void addRaspberryPi()
        {
            addRaspberryPi(IPAdressConnect, "?");
        }

        public void addRaspberryPi(string Address, string status)
        {
            //IsPiConnected = false;
            IPAddress address;
            try
            {
                address = IPAddress.Parse(Address);
            }
            catch (FormatException fx)
            {
                debugVM.AddDebugInfo("[ERROR]", "Invalid IP Address Format: " + fx.Message);
                return;
            }
            IPEndPoint endpoint = new IPEndPoint(address, 54321);
            foreach(var entry in BackendList)
            {
                if (entry.endpoint.Equals(endpoint))
                {
                    debugVM.AddDebugInfo("addRaspberryPi", "Already in the List");
                    return;
                }
            }

            RaspberryPi raspi = new RaspberryPi();
            SynchronizationContext uiContext = SynchronizationContext.Current;
            raspi.ConnectionClosed += (object sender, Exception e) => OnConnectionClosed(sender, e, uiContext);
            RaspberryPiItem raspiItem = new RaspberryPiItem() { endpoint = endpoint, Status = status, raspi = raspi, Connected = false };
            BackendList.Add(raspiItem);
            SelectedRaspiItem = raspiItem;
            CollectionViewSource.GetDefaultView(BackendList).Refresh();
        }

        private void OnConnectionClosed(object sender, Exception e, SynchronizationContext context)
        {
            context.Send((object state) =>
            {
                List<RaspberryPiItem> disconnectedList = BackendList.Where(item => item.raspi == (RaspberryPi)sender).ToList();
                RaspberryPiItem piItem = disconnectedList.FirstOrDefault();
                piItem.Status = "available";
                piItem.Connected = false;
                CollectionViewSource.GetDefaultView(BackendList).Refresh();
                debugVM.AddDebugInfo(piItem.endpoint.ToString(), "ConnectionClosed: " + e.Message);
            }, null);
        }

        public async Task connect()
        {
            try
            {
                await SelectedRaspiItem.raspi.ConnectAsync(SelectedRaspiItem.endpoint);
            }
            catch(Exception e)
            {
                debugVM.AddDebugInfo("[ERROR]", "Couldn't establish connection: " + e.Message);
                return;
            }
            debugVM.AddDebugInfo("[SUCCESS]", "Connection established");

            SelectedRaspiItem.Status = "Connected with this UI";
            SelectedRaspiItem.Connected = true;
            IsPiConnectedStatus = Visibility.Visible;
            IsPiDisconnected = Visibility.Hidden;
            CollectionViewSource.GetDefaultView(BackendList).Refresh();
            //raspberryPis.Add(IPAdressConnect, SelectedRaspiItem.raspi);
            //IsPiConnected = true;

            try
            {
                String result = await SelectedRaspiItem.raspi.GetAvailableHI();
                getAvailableHI(result);
            }catch(Exception e)
            {
                debugVM.AddDebugInfo("[ERROR]", "Error getting available HI: " + e.Message);
            }

            try
            {
                Model = await getRaspiModel();
            }catch(Exception e)
            {
                debugVM.AddDebugInfo("[ERROR]", "Error getting Raspi Model: " + e.Message);
            }

            try
            {
                Family = await getRaspiFamily();
            }catch(Exception e)
            {
                debugVM.AddDebugInfo("[ERROR]", "Error getting Raspi Famili: " + e.Message);
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
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = SmallRight.Item1;
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = SmallLeft.Item1;
            ComboBoxItem item4 = new ComboBoxItem();
            item4.Content = MediumRight.Item1;
            ComboBoxItem item5 = new ComboBoxItem();
            item5.Content = MediumLeft.Item1;
            ComboBoxItem item6 = new ComboBoxItem();
            item6.Content = PowerRight.Item1;
            ComboBoxItem item7 = new ComboBoxItem();
            item7.Content = PowerLeft.Item1;
            ComboBoxItem item8 = new ComboBoxItem();
            item8.Content = HighPowerRight.Item1;
            ComboBoxItem item9 = new ComboBoxItem();
            item9.Content = HighPowerLeft.Item1;
            ComboBoxItem item10 = new ComboBoxItem();
            item10.Content = Defective.Item1;
            ComboBoxItem item11 = new ComboBoxItem();
            item11.Content = NoReceiver.Item1;
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

        #endregion
    }
}
