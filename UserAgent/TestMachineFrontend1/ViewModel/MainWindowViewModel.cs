using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.Helpers;
using TestMachineFrontend1.Model;
using TestMachineFrontend1.Commands;

namespace TestMachineFrontend1.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        //public static ICommand LoadViewCommand { get; private set; }
        public ICommand ConnectIPCommand { get; private set; }
        //TODO: check if is better to use getDuration() as a simple method
        public ICommand GetDurationCommand { get; private set; }
        public ICommand PressPushButtonCommand { get; private set; }
        public List<TabControlModel> TabItems { get; set; }
TestCallee testCallee; 

        public MainWindowViewModel()
        {
testCallee = new TestCallee();
            LoadDebugView();
            LoadDetectView();
            LoadMainTabView();
            LoadDetectTabView();
            LoadLCDControlsView();
            LoadMultiplexerView();
            LoadUserControlView();
            //LoadViewCommand = new DelegateCommand(o => this.LoadDetectTabView());
            ConnectIPCommand = new ConnectIPCommand((DetectTabViewModel)CurrentViewModelDetectTab);
            GetDurationCommand = new GetDurationCommand((UserControlsViewModel)CurrentViewModelUserControls);
            PressPushButtonCommand = new PressPushButtonCommand((UserControlsViewModel)CurrentViewModelUserControls,
                (DetectTabViewModel)CurrentViewModelDetectTab, (DebugViewModel)CurrentViewModelDebug);

            this.TabItems = GetAllTabItems();
        }

        //public ObservableCollection<ObservableObject> TabItems { get { return tabItems; } }

        public List<TabControlModel> GetAllTabItems()
        {
            return new List<TabControlModel>
                            {
                                new TabControlModel()
                                    {
                                        Header = "Detect",
                                        CurrentTabContentViewModel = (DetectTabViewModel)CurrentViewModelDetectTab
                                    },
                                new TabControlModel()
                                    {
                                        Header = "User Controls",
                                        CurrentTabContentViewModel = (UserControlsViewModel)CurrentViewModelUserControls
                                    },
                                new TabControlModel()
                                    {
                                        Header = "LCD Controls",
                                        CurrentTabContentViewModel = (LCDControlsViewModel)CurrentViewModelLCDControls
                                    },
                                new TabControlModel()
                                    {
                                        Header = "Mux55",
                                        CurrentTabContentViewModel = (MultiplexerViewModel)CurrentViewModelMultiplexer
                                    }
                            };
        }

        private ObservableObject currentViewModelDebug;

        public ObservableObject CurrentViewModelDebug
        {
            get { return currentViewModelDebug; }
            set
            {
                currentViewModelDebug = value;
                this.OnPropertyChanged("CurrentViewModelDebug");
            }
        }

        private ObservableObject currentViewModelDetectTab;

        public ObservableObject CurrentViewModelDetectTab
        {
            get { return currentViewModelDetectTab; }
            set
            {
                currentViewModelDetectTab = value;
                this.OnPropertyChanged("CurrentViewModelDetectTab");
            }
        }

        private ObservableObject currentViewModelLCDControls;

        public ObservableObject CurrentViewModelLCDControls
        {
            get { return currentViewModelLCDControls; }
            set
            {
                currentViewModelLCDControls = value;
                this.OnPropertyChanged("CurrentViewModelLCDControls");
            }
        }

        private ObservableObject currentViewModelUserControls;

        public ObservableObject CurrentViewModelUserControls
        {
            get { return currentViewModelUserControls; }
            set
            {
                currentViewModelUserControls = value;
                this.OnPropertyChanged("CurrentViewModelUserControls");
            }
        }

        private ObservableObject currentViewModelMultiplexer;

        public ObservableObject CurrentViewModelMultiplexer
        {
            get { return currentViewModelMultiplexer; }
            set
            {
                currentViewModelMultiplexer = value;
                this.OnPropertyChanged("CurrentViewModelMultiplexer");
            }
        }

        private ObservableObject currentViewModelMainTab;
        public ObservableObject CurrentViewModelMainTab
        {
            get { return currentViewModelMainTab; }
            set
            {
                currentViewModelMainTab = value;
                this.OnPropertyChanged("CurrentViewModelMainTab");
            }
        }

        private ObservableObject currentViewModelDetect;

        public ObservableObject CurrentViewModelDetect
        {
            get { return currentViewModelDetect; }
            set
            {
                currentViewModelDetect = value;
                this.OnPropertyChanged("CurrentViewModelDetect");
            }
        }

        private void LoadDebugView()
        {
            CurrentViewModelDebug = new DebugViewModel();
        }

        private void LoadDetectView()
        {
            CurrentViewModelDetect = new DetectViewModel();
        }

        private void LoadMainTabView()
        {
            CurrentViewModelMainTab = new MainTabViewModel();
        }

        private void LoadDetectTabView()
        {
            CurrentViewModelDetectTab = new DetectTabViewModel((DebugViewModel)CurrentViewModelDebug);
        }
        private void LoadLCDControlsView()
        {
            CurrentViewModelLCDControls = new LCDControlsViewModel();
        }

        private void LoadUserControlView()
        {
            CurrentViewModelUserControls = new UserControlsViewModel((DetectTabViewModel)currentViewModelDetectTab);
        }

        private void LoadMultiplexerView()
        {
            CurrentViewModelMultiplexer = new MultiplexerViewModel();
        }
    }
}

