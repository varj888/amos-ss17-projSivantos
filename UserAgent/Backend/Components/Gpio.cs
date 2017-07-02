using System;
using System.Collections.Generic;
using Windows.Devices.Gpio;
using Windows.Foundation;

namespace RaspberryBackend
{
    //<summary>
    //Interface to access GPIO pin_ids on a raspberry pi
    //</summary>
    public class GPIOinterface : HWComponent
    {
        private Dictionary<string, UInt16> pin_ids = new Dictionary<string, UInt16>();
        private Dictionary<UInt16, GpioPin> pins = new Dictionary<UInt16, GpioPin>();
        private const GpioPinValue PIN_HIGH = GpioPinValue.High;
        private const GpioPinValue PIN_LOW = GpioPinValue.Low;
        private GpioController gpio = GpioController.GetDefault();


        public GPIOinterface()
        {
            pin_ids["PIN_04"] = 4;   //PullUp
            pin_ids["PIN_05"] = 5;   //PullUp
            pin_ids["PIN_06"] = 6;   //PullUp
            pin_ids["PIN_12"] = 12;  //PullDown
            pin_ids["PIN_13"] = 13;  //PullDown
            pin_ids["PIN_16"] = 16;  //PullDown SPI1 CS0
            pin_ids["PIN_17"] = 17;  //PullDown
            pin_ids["PIN_18"] = 18;  //PullDown Reserved for NRESET on Mux
            pin_ids["PIN_19"] = 19;  //PullDown SPI1 MISO
            pin_ids["PIN_20"] = 20;  //PullDown SPI1 MOSI
            pin_ids["PIN_21"] = 21;  //PullDown SPI1 SCLK
            pin_ids["PIN_22"] = 22;  //PullDown
            pin_ids["PIN_23"] = 23;  //PullDown
            pin_ids["PIN_24"] = 24;  //PullDown
            pin_ids["PIN_26"] = 26;  //PullDown
            pin_ids["PIN_27"] = 27;  //PullDown
        }

        public override void initiate()
        {
            if (_initialized) return;

            foreach (var pin in pin_ids.Keys)
            {
                pins[pin_ids[pin]] = gpio.OpenPin(pin_ids[pin]);
            }

            _initialized = true;
        }

        //<summary>
        //Get a list of available pin ids
        //</summary>
        public Dictionary<string, UInt16> getPinIds()
        {
            return this.pin_ids;
        }

        //<summary>
        //Get a pin by ID
        //</summary>
        public GpioPin getPin(UInt16 id)
        {
            if (!pins.ContainsKey(id)) throw new ArgumentException("The requested pin is not available");

            return pins[id];
        }

        //<summary>
        //Register an eventhandler for a pin
        //</summary>
        public void registerEventHandler(UInt16 id, TypedEventHandler<GpioPin, GpioPinValueChangedEventArgs> f)
        {
            getPin(id).ValueChanged += f;
        }

        //<summary>
        //Set inputmode according to whether pin is supposed to be PullUp/ Down
        //</summary>
        public void setToInput(UInt16 id)
        {
            if (id < 9)
            {
                getPin(id).SetDriveMode(GpioPinDriveMode.InputPullUp);
            }
            else
            {
                getPin(id).SetDriveMode(GpioPinDriveMode.InputPullDown);
            }
        }

        //<summary>
        //Set a pin to output
        //</summary>
        public void setToOutput(UInt16 id)
        {

            getPin(id).SetDriveMode(GpioPinDriveMode.Output);
        }

        //<summary>
        //Write to a pin 0 for low-value, 1 for high-value
        //</summary>
        public void writePin(UInt16 id, uint v)
        {
            getPin(id).Write((v == 0) ? PIN_LOW : PIN_HIGH);
        }

        //<summary>
        //Read from a pin (will read last input if pin is configured as input
        //</summary>
        public string readPin(UInt16 id)
        {
            return getPin(id).Read().ToString();
        }

        /// <summary>
        /// Set GPIO pin to 1
        /// </summary>
        /// <param name="id"></param>
        public void activatePin(UInt16 id)
        {
            setToOutput(id);
            writePin(id, 1);
        }

        /// <summary>
        /// Reset GPIO pin by settting to 0
        /// </summary>
        /// <param name="id"></param>
        public void deactivatePin(UInt16 id)
        {
            setToOutput(id);
            writePin(id, 0);
        }
    }
}
