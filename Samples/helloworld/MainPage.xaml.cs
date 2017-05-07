using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks.Dataflow;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LcdSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Setup address
        private const string I2C_CONTROLLER_NAME = "I2C1"; //use for RPI2
        private const byte DEVICE_I2C_ADDRESS = 0x27; // 7-bit I2C address of the port expander

        DisplayI2C lcd;

        //Setup pins
        private const byte EN = 0x02;
        private const byte RW = 0x01;
        private const byte RS = 0x00;
        private const byte D4 = 0x04;
        private const byte D5 = 0x05;
        private const byte D6 = 0x06;
        private const byte D7 = 0x07;
        private const byte BL = 0x03;

        //LED Button Setup
        private const int LED_PIN = 6;
        private const int BUTTON_PIN = 5;
        private const int TEMP_PIN = 21;

        GpioPin ledPin;
        GpioPin buttonPin;
        GpioPin tempPin;

        GpioPinValue ledPinValue = GpioPinValue.High;
        GpioController gpioController;


        public MainPage()
        {
            gpioController = GpioController.GetDefault();
            this.InitializeComponent();
            this.Start();   
        }

        private void Start()
        {
            // Here is I2C bus and Display itself initialized.
            //
            //  I2C bus is initialized by library constructor. There is also defined PCF8574 pins 
            //  Default `DEVICE_I2C_ADDRESS` is `0x27` (you can change it by A0-2 pins on PCF8574 - for more info please read datasheet)
            //  `I2C_CONTROLLER_NAME` for Raspberry Pi 2 is `"I2C1"`
            //  For Arduino it should be `"I2C5"`, but I did't test it.
            //  Other arguments should be: RS = 0, RW = 1, EN = 2, D4 = 4, D5 = 5, D6 = 6, D7 = 7, BL = 3
            //  But it depends on your PCF8574.
            lcd = new DisplayI2C(DEVICE_I2C_ADDRESS, I2C_CONTROLLER_NAME, RS, RW, EN, D4, D5, D6, D7, BL);
            lcd.init();
            // Here is created new symbol
            // Take a look at data - it's smile emoticon
            // 0x00 => 00000
            // 0x00 => 00000
            // 0x0A => 01010
            // 0x00 => 00000
            // 0x11 => 10001
            // 0x0E => 01110
            // 0x00 => 00000
            // 0x00 => 00000 

            String ip = GetIpAddressAsync();

            // data of symbol by lines                          //address of symbol
            //lcd.createSymbol(new byte[] { 0x00, 0x00, 0x0A, 0x00, 0x11, 0x0E, 0x00, 0x00 }, 0x00);

            // Here is printed string
            lcd.prints(ip);

            InitLedButtonSetup();
            SetupTempMeassurement();
            StartTestMeassurment();
        }

        private void StartTestMeassurment()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            var pinValue = tempPin.Read();
            var task = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.TempTextBox.Text = pinValue.ToString();
            });
        }

        private void SetupTempMeassurement()
        {
            tempPin = gpioController.OpenPin(TEMP_PIN);
            tempPin.SetDriveMode(GpioPinDriveMode.Input);
            tempPin.ValueChanged += TempPin_ValueChanged;

        }

        private void TempPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            
        }

        private void InitLedButtonSetup()
        {
            buttonPin= gpioController.OpenPin(BUTTON_PIN);
            ledPin = gpioController.OpenPin(LED_PIN);
            ledPin.Write(GpioPinValue.High);
            ledPin.SetDriveMode(GpioPinDriveMode.Output);

            if (buttonPin.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
            {
                buttonPin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            }

            buttonPin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            buttonPin.ValueChanged += ButtonPin_ValueChanged; 
        }

        private void ButtonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs events)
        {
            lcd.gotoSecondLine();
            var text = events.Edge.ToString();
            var length = text.Length;

            UpdateGui(text);

            if (length < 16) text += new String(' ', 16 - length);
            lcd.prints(text);
            if(events.Edge == GpioPinEdge.FallingEdge)
            {
                ToggleLed();
            }
        }

        private void ToggleLed()
        {
            ledPinValue = (ledPinValue == GpioPinValue.Low) ? GpioPinValue.High : GpioPinValue.Low;
            ledPin.Write(ledPinValue);
        }

        private void UpdateGui(string text)
        {
            var task = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.HelloMessage.Text = text;
            });
        }

        private string GetIpAddressAsync()
        {
            var ipAsString = "Not Found";
            var hosts = Windows.Networking.Connectivity.NetworkInformation.GetHostNames().ToList();
            var hostNames = new List<string>();
            //NetworkInterfaceType
            foreach (var h in hosts)
            {
                hostNames.Add(h.DisplayName);
                if (h.Type == Windows.Networking.HostNameType.Ipv4)
                {
                    var networkAdapter = h.IPInformation.NetworkAdapter;
                    if (networkAdapter.IanaInterfaceType == (uint)NetworkInterfaceType.Ethernet || networkAdapter.IanaInterfaceType == (uint)NetworkInterfaceType.Wireless80211)
                    {
                        IPAddress ip;
                        if (!IPAddress.TryParse(h.DisplayName, out ip)) continue;
                        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) return ip.ToString();
                    }
                 
                } 
            }

            
            return ipAsString;

        }

        private void ClickMe_Click(object sender, RoutedEventArgs e)
        {
            ToggleLed();
        }
    }
}
