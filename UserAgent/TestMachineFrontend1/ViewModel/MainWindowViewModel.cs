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
using TestmachineFrontend1;
using CommonFiles.Networking;
using System.Net.Sockets;
using CommonFiles.TransferObjects;
using System.Threading;
using System.Net;

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
        public static ICommand SetHICommand { get; private set; }
        public static ICommand LEDOnCommand { get; private set; }

        #endregion

        #region Properties
        public static List<TabControlModel> TabItems { get; set; }
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
            InitAllViewModels();
            InitAllCommands();
            TabItems = GetAllTabItems();
        }

        public static void InitAllViewModels()
        {
            Instance = new MainWindowViewModel();
            CurrentViewModelDebug = new DebugViewModel();
            CurrentViewModelDetectTab = new DetectTabViewModel();
            CurrentViewModelLCDControls = new LCDControlsViewModel();
            CurrentViewModelUserControls = new UserControlsViewModel();
            CurrentViewModelMultiplexer = new MultiplexerViewModel();
            CurrentViewModelMainTab = new MainTabViewModel();
            CurrentViewModelDetect = new DetectViewModel();
        }

        public static void InitAllCommands()
        {
            ConnectIPCommand = new ConnectIPCommand();
            AddDebugInfoCommand = new AddDebugInfoCommand();
            SendRequestCommand = new SendRequestCommand();
            GetDurationCommand = new GetDurationCommand();
            PressPushButtonCommand = new PressPushButtonCommand();
            EndlessVcCommand = new EndlessVcCommand();
            PressRockerSwitchCommand = new PressRockerSwitchCommand();
            SetHICommand = new SetHICommand();
            LEDOnCommand = new LEDOnCommand();
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

        /// <summary>
        /// todo: use the raspberry Pi dictionary or something like that
        /// </summary>
        public static RaspberryPi raspberryPi;

        public async void connectIP()
        {
            try
            {
                var pi1 = await RaspberryPi.CreateAsync(new IPEndPoint(IPAddress.Parse(CurrentViewModelDetectTab.IPAdressConnect), 54321));
                raspberryPi = pi1;
                CurrentViewModelDetectTab.IsPiConnected = true;
                CurrentViewModelDetectTab.raspberryPis.Add(CurrentViewModelDetectTab.IPAdressConnect, pi1);
                RaspberryPiItem raspiItem = new RaspberryPiItem() { Name = CurrentViewModelDetectTab.IPAdressConnect, Id = 45, Status = "OK", raspi = pi1 };
                CurrentViewModelDetectTab.BackendList.Add(raspiItem);
                CurrentViewModelDetectTab.SelectedRaspiItem = raspiItem;
                CurrentViewModelDebug.AddDebugInfo("[SUCCESS]", "Connection established");
                //sendRequest(GetAvailableHI);
                //Result result = getResult(GetAvailableHI);
                //MainWindowViewModel.CurrentViewModelMultiplexer.getAvailableHI(result);
                SynchronizationContext uiContext = SynchronizationContext.Current;
                await Task.Run(() => ReceiveResultLoop(uiContext));
            }
            catch (FormatException fx)
            {
                CurrentViewModelDebug.AddDebugInfo("[ERROR]", "Invalid IP Address Format: " + fx.Message);

                //TODO check
                CurrentViewModelDetectTab.IsPiConnected = false;
            }
            catch (SocketException sx)
            {
                CurrentViewModelDebug.AddDebugInfo("[ERROR]", "Couldn't establish connection: " + sx.Message);
                //TODO check
                CurrentViewModelDetectTab.IsPiConnected = false;

            }
            catch (Exception any)
            {
                CurrentViewModelDebug.AddDebugInfo("[ERROR]", "Unknown Error. " + any.Message);
                //TODO check
                CurrentViewModelDetectTab.IsPiConnected = false;
            }
        }

        public void invokeRaspberryPi(Request request)
        {
            //if (CurrentViewModelDetectTab.SelectedRaspiItem == null)
            //{
            //    CurrentViewModelDebug.AddDebugInfo("Debug", "No raspi selected");
            //    return;
            //}

            //try
            //{
            //    Transfer.sendObject(getClientconnection().GetStream(), request);
            //}
            //catch (Exception ex)
            //{
            //    CurrentViewModelDebug.AddDebugInfo(request.command, "Request could not be sent: " + ex.Message);
            //    return;
            //}
        }

        private async Task ReceiveResultLoop(SynchronizationContext uiContext)
        {
            while (true)
            {
                //Object result = raspberryPi.getNotification();

                //if (result.exceptionMessage == null)
                //{
                //    uiContext.Send((object state) => CurrentViewModelDebug.AddDebugInfo(result.value.ToString(), "sucess"), null);

                    //if ((result.obj.Equals(CurrentViewModelUserControls.DetectTCol.command))
                    //    && result.value.ToString() == "High")
                    //{
                    //    CurrentViewModelUserControls.TCoilDetected = true;
                    //    CurrentViewModelDebug.AddDebugInfo("Update", "ToggleTeleCoil completed");

                    //}
                    //else if (result.obj.Equals(CurrentViewModelUserControls.UndetectTCol.command)
                    //    && result.value.ToString() == "Low")
                    //{
                    //    CurrentViewModelUserControls.TCoilDetected = false;
                    //    CurrentViewModelDebug.AddDebugInfo("Update", "ToggleTeleCoil completed");
                    //}
                //}
                //else
                //{
                //    uiContext.Send((object state) => CurrentViewModelDebug.AddDebugInfo(result.value.ToString(), result.exceptionMessage), null);
                //}
            }
        }


        //public TcpClient getClientconnection()
        //{
        //    if (CurrentViewModelDetectTab.SelectedRaspiItem == null && CurrentViewModelDetectTab.BackendList.Count > 0)
        //    {
        //        CurrentViewModelDetectTab.SelectedRaspiItem = CurrentViewModelDetectTab.BackendList.ElementAt(0);
        //    }
        //    var c = (RaspberryPiItem)CurrentViewModelDetectTab.SelectedRaspiItem;
        //    return c.raspi.socket;
        //}
    }
}

