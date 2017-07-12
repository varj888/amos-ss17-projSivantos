using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestMachineFrontend1.Commands;
using TestMachineFrontend1.Helpers;

namespace TestMachineFrontend1.ViewModel
{
    public class UserControlsViewModel : ObservableObject
    {
        //private DetectTabViewModel dtVM;
        MainWindowViewModel mwVM = MainWindowViewModel.Instance;
        public UserControlsViewModel()
        {
            //dtVM = MainWindowViewModel.CurrentViewModelDetectTab;
            initDurationComboBox();
            initReceiverComboBox();
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
            item1.Content = "Short";
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Small Right";
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Small Left";
            ComboBoxItem item4 = new ComboBoxItem();
            item4.Content = "Medium Right";
            ComboBoxItem item5 = new ComboBoxItem();
            item5.Content = "Medium Left";
            ComboBoxItem item6 = new ComboBoxItem();
            item6.Content = "Power Right";
            ComboBoxItem item7 = new ComboBoxItem();
            item7.Content = "Power Left";
            ComboBoxItem item8 = new ComboBoxItem();
            item8.Content = "High Power Right";
            ComboBoxItem item9 = new ComboBoxItem();
            item9.Content = "High Power Left";
            ComboBoxItem item10 = new ComboBoxItem();
            item10.Content = "Defective";
            ComboBoxItem item11 = new ComboBoxItem();
            item11.Content = "No Receiver";
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
            ReceiverItems.Add(item11);
            SelectedReceiverItem = ReceiverItems.First();
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

        private ComboBoxItem _selectedReceiverItem;
        public ComboBoxItem SelectedReceiverItem
        {
            get { return _selectedReceiverItem; }
            set
            {
                _selectedReceiverItem = value;
                _selectedReceiverItemIndex = DurationItems.IndexOf(_selectedReceiverItem);
                OnPropertyChanged("SelectedReceiverItem");
                Request request = new Request("SetARDVoltage", _selectedReceiverItem.Content);
                //dtVM.sendRequest(request);
                mwVM.sendRequest(request);
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

        public Request PressPushButton
        {
            get { return new Request("PressPushButton", getDuration()); }
        }

        public Request PressRockerSwitchUp
        {
            get { return new Request("PressRockerSwitch", new int[] { 1, getDuration() }); }
        }

        public Request PressRockerSwitchDown
        {
            get { return new Request("PressRockerSwitch", new int[] { 0, getDuration() }); }
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
    }
}
