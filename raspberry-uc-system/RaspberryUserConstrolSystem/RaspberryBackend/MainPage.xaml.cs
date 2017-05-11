using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace HelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const int LED_PIN_5 = 5;
        const int LED_PIN_6 = 6;
        DispatcherTimer timer;
        GpioPinValue pinValue1;
        GpioPinValue pinValue2;
        GpioPin pin1;
        GpioPin pin2;
        public MainPage()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            InitGpio();
            if(pin1 != null || pin2 != null)
            {
                timer.Start();
            }

            this.InitializeComponent();
            createListenerAsync();
        }

        /// <summary>
        /// creates TCP socket that is listening on port 7777 for requests
        /// requests will be handeled by SocketListener_ConnectionReceived
        /// </summary>
        private async Task createListenerAsync()
        {

            //Create a StreamSocketListener to start listening for TCP connections.
            Windows.Networking.Sockets.StreamSocketListener socketListener = new Windows.Networking.Sockets.StreamSocketListener();

            //Hook up an event handler to call when connections are received.
            socketListener.ConnectionReceived += SocketListener_ConnectionReceived;
            Debug.WriteLine("create Listener");

            try
            {
                //Start listening for incoming TCP connections on the specified port
                await socketListener.BindServiceNameAsync("7777");
                Debug.WriteLine("created Listener");
            }
            catch (Exception e)
            {
                //Handle exception.
            }
        }

        /// <summary>
        /// trys to read a string from the socket and send it back
        /// </summary>
        private async void SocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender,
            Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
        {
            //Read line from the remote client.
            Stream inStream = args.Socket.InputStream.AsStreamForRead();
            StreamReader reader = new StreamReader(inStream);
            string request = await reader.ReadLineAsync();

            Debug.WriteLine("request: " + request);

            //Send the line back to the remote client.
            Stream outStream = args.Socket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(outStream);
            await writer.WriteLineAsync(request);
            await writer.FlushAsync();
        }

        private void Timer_Tick(object sender, object e)
        {
            //System.Diagnostics.Debug.WriteLine(pin1.Read());
            //System.Diagnostics.Debug.WriteLine(pin2.Read());
            //return;
            /*if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                pin.Write(pinValue);
                //LED.Fill = redBrush;
            }
            else
            {
                pinValue = GpioPinValue.High;
                pin.Write(pinValue);
                //LED.Fill = grayBrush;
            }*/
        }

        private void InitGpio()
        {
            var gpio = GpioController.GetDefault();

            if(gpio == null)
            {
                pin1 = null;
                pin2 = null;
                return;
            }
            pin1 = gpio.OpenPin(LED_PIN_5);
            pin2 = gpio.OpenPin(LED_PIN_6);
            //pinValue = GpioPinValue.High;
            //pin.Write(pinValue);
            pin1.SetDriveMode(GpioPinDriveMode.InputPullDown);
            pin2.SetDriveMode(GpioPinDriveMode.InputPullDown);
            pin1.ValueChanged += val_changed;
            pin2.ValueChanged += val_changed;
        }

        private void val_changed(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Pin {0} changed to {1}",sender.PinNumber, sender.Read());
        }
    }
}
