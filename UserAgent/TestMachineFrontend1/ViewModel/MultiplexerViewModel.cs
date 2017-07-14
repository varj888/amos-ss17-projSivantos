using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        MainWindowViewModel mwVM = MainWindowViewModel.Instance;
        //private DetectTabViewModel detectTabVM;
        public MultiplexerViewModel()
        {
            availableHI = new Dictionary<string, List<string>>();
            HIListItems = new ObservableCollection<ComboBoxItem>();
            helperXML = new HelperXML();
            debugVM = MainWindowViewModel.CurrentViewModelDebug;
            //detectTabVM = MainWindowViewModel.CurrentViewModelDetectTab;
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

        public async Task ResetMux()
        {
            //String result = await MainWindowViewModel.raspberryPi.ResetMux(0);
            //Debug.WriteLine(result);
        }

        public async Task ConnectPins()
        {
            //String result = await MainWindowViewModel.raspberryPi.ConnectPins((int)ValueX, (int)ValueY);
            //Debug.WriteLine(result);
        }

        public async Task setHI()
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

            //Result result = await MainWindowViewModel.raspberryPi.SetHI(family, model);
            //Debug.WriteLine(result.value);
        }

        //public Request GetAvailableHI
        //{
        //    get { return new Request("GetAvailableHI", 0); }
        //}

        //public void getAvailableHI(Result result)
        //{
        //    availableHI = helperXML.buildDictionary((string)result.value);
        //    foreach (string family in availableHI.Keys)
        //    {
        //        foreach (string model in availableHI[family])
        //        {
        //            ComboBoxItem element = new ComboBoxItem();
        //            element.Name = family;
        //            element.Content = model;
        //            HIListItems.Add(element);
        //        }
        //    }
        //    debugVM.AddDebugInfo(result.obj, "Updated List");
        //}

        //public Result getResult(Request request)
        //{
        //    Result result = null;
        //    try
        //    {
        //        result = Transfer.receiveObject<Result>(/*detectTabVM*/mwVM.getClientconnection().GetStream());
        //    }
        //    catch (Exception e)
        //    {
        //        debugVM.AddDebugInfo(request.command, "Result could not be received: " + e.Message);
        //    }
        //    return result;
        //}
    }
}
