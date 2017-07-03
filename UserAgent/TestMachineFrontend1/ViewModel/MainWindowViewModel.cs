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
using TestmachineFrontend;

namespace TestMachineFrontend1.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Commands
        public static ICommand ConnectIPCommand { get; private set; }
        public static ICommand GetDurationCommand { get; private set; }
        public static ICommand AddDebugInfoCommand { get; private set; }
        public static ICommand SendRequestCommand { get; private set; }
        public static ICommand PressPushButtonCommand { get; private set; }
        public static ICommand EndlessVcCommand { get; private set; }
        public static ICommand PressRockerSwitchCommand { get; private set; }

        #endregion

        #region Properties
        public static List<TabControlModel> TabItems { get; set; }
        private TestCallee testCallee;
        public static TestCallee TestCallee { get; private set; }
        public TestCallee TestCalleeProperty
        {
            get { return testCallee; }
            set
            {
                testCallee = value;
                OnPropertyChanged("TestCalleeProperty");
            }
        }
        #endregion

        #region ViewModels
        public static MainWindowViewModel Instance { get; private set; }
        public static DebugViewModel CurrentViewModelDebug { get; private set; }
        public static DetectTabViewModel CurrentViewModelDetectTab { get; private set; }
        public static LCDControlsViewModel CurrentViewModelLCDControls { get; private set; }
        public static UserControlsViewModel CurrentViewModelUserControls { get; private set; }
        public static MultiplexerViewModel CurrentViewModelMultiplexer { get; private set; }
        public static MainTabViewModel CurrentViewModelMainTab { get; private set; }
        public static DetectViewModel CurrentViewModelDetect { get; private set; }
        #endregion

        static MainWindowViewModel()
        {
            TestCallee = new TestCallee();
            InitAllViewModels();
            InitAllCommands();
            TabItems = GetAllTabItems();
        }

        public static void InitAllViewModels()
        {
            Instance = new MainWindowViewModel();
            CurrentViewModelDebug = new DebugViewModel();
            CurrentViewModelDetectTab = new DetectTabViewModel(CurrentViewModelDebug, TestCallee);
            CurrentViewModelLCDControls = new LCDControlsViewModel();
            CurrentViewModelUserControls = new UserControlsViewModel(CurrentViewModelDetectTab);
            CurrentViewModelMultiplexer = new MultiplexerViewModel();
            CurrentViewModelMainTab = new MainTabViewModel();
            CurrentViewModelDetect = new DetectViewModel();
        }

        public static void InitAllCommands()
        {
            ConnectIPCommand = new ConnectIPCommand(CurrentViewModelDetectTab);
            AddDebugInfoCommand = new AddDebugInfoCommand(CurrentViewModelDebug);
            SendRequestCommand = new SendRequestCommand(CurrentViewModelDetectTab);
            GetDurationCommand = new GetDurationCommand(CurrentViewModelUserControls);
            PressPushButtonCommand = new PressPushButtonCommand(CurrentViewModelUserControls,
                CurrentViewModelDetectTab, CurrentViewModelDebug);
            EndlessVcCommand = new EndlessVcCommand(CurrentViewModelDetectTab, CurrentViewModelDebug);
            PressRockerSwitchCommand = new PressRockerSwitchCommand
                (CurrentViewModelUserControls, CurrentViewModelDetectTab, CurrentViewModelDebug);
        }

        public static List<TabControlModel> GetAllTabItems()
        {
            return new List<TabControlModel>
            {
                new TabControlModel()
                {
                    Header = "Detect",
                    CurrentTabContentViewModel = CurrentViewModelDetectTab
                },
                new TabControlModel()
                {
                    Header = "User Controls",
                    CurrentTabContentViewModel = CurrentViewModelUserControls
                },
                new TabControlModel()
                {
                    Header = "LCD Controls",
                    CurrentTabContentViewModel = CurrentViewModelLCDControls
                },
                new TabControlModel()
                {
                    Header = "Mux55",
                    CurrentTabContentViewModel = CurrentViewModelMultiplexer
                }
            };
        }
    }
}

