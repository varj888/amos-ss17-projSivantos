using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestMachineFrontend1.Helpers;

namespace TestMachineFrontend1.ViewModel
{
    public class MultiplexerViewModel : ObservableObject
    {
        private Dictionary<string, List<string>> availableHI;
        private HelperXML helperXML;
        private DebugViewModel debugVM;
        private DetectTabViewModel detectTabVM;
        public MultiplexerViewModel()
        {
            availableHI = new Dictionary<string, List<string>>();
            HIListItems = new ObservableCollection<ComboBoxItem>();
            helperXML = new HelperXML();
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
            detectTabVM = MainWindowViewModel.CurrentViewModelDetectTab;
        }
        private double x;
        public double ValueX
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("ValueX");
            }
        }
        private double y;
        public double ValueY
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("ValueY");
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

        public void setHI()
        {
            ComboBoxItem ci;
            try
            {
                ci = SelectedHI;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                debugVM.AddDebugInfo("setHI_Click", "No valid model selected.");
                return;
            }

            string model = ci.Content.ToString();
            string family = ci.Name;

            Request request = new Request("SetHI", new Object[] { family, model });

            detectTabVM.sendRequest(request);
            detectTabVM.getResult(request);
        }

        public Request GetAvailableHI
        {
            get { return new Request("GetAvailableHI", 0); }
        }

        public Request ConnectPins
        {
            get { return new Request("ConnectPins", new object[] { (int)ValueX, (int)ValueY }); }
        }

        public Request ResetMux
        {
            get { return new Request("ResetMux", 0); }
        }

        public void getAvailableHI(Result result)
        {
            availableHI = helperXML.buildDictionary((string)result.value);
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
            debugVM.AddDebugInfo(result.obj, "Updated List");
        }

        public Result getResult(Request request)
        {
            Result result = null;
            try
            {
                result = detectTabVM.getClientconnection().receiveObject();
            }
            catch (Exception e)
            {
                debugVM.AddDebugInfo(request.command, "Result could not be received: " + e.Message);
            }
            return result;
        }
    }
}
