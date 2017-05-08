using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
