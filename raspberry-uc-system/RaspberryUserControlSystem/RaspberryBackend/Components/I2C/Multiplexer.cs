using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Gpio;
using Windows.Devices.I2c;

namespace RaspberryBackend
{
    /// <summary>
    /// represents the physical multiplexer (currently using ADG2128)
    /// and implements the core functionality of the device
    /// ADG2128 is an analog cross point switch with an array size of 8 x 12
    /// </summary>
    public class Multiplexer
    {
        /// <summary>
        /// total ammount of available switches (8 x 12 --> 96)
        /// </summary>
        private readonly int switches = 96;

        // use these constants for controlling how the I2C bus is setup
        private const string I2C_CONTROLLER_NAME = "I2C1";
        private const byte MULTIPLEXER_I2C_ADDRESS = 0x70;
        private I2cDevice multiplexer;
        private Boolean _initialized = false;
        private byte _DB15 = 0x80;
        private GpioPin _reset;

        //private static I2cInstance;

        /// <summary>
        /// creates and initializes the Multiplexer
        /// </summary>
        /// <param name="reset">gpioPin ID which will be used to reset the Multiplexer</param>
        public Multiplexer(GpioPin reset)
        {
            _reset = reset;
            Task.Run(() => this.startI2C()).Wait();
            _initialized = true;
        }

        private async void startI2C()
        {
            try
            {
                var i2cSettings = new I2cConnectionSettings(MULTIPLEXER_I2C_ADDRESS);
                i2cSettings.BusSpeed = I2cBusSpeed.FastMode;
                string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
                var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);
                this.multiplexer = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, i2cSettings);
                this.powerON();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }
        }

        /// <summary>
        /// returns whether the Multiplexer is initialized or not
        /// </summary>
        /// <returns>true:= initialize and false:= not initialized</returns>
        public Boolean isInitialized()
        {
            return _initialized;
        }

        /// <summary>
        /// simultaneously updates all switches
        /// using the LDSW command
        /// </summary>
        public void updateAllSwitches()
        {

        }
        /// <summary>
        /// resets (switch off) all of the
        /// switch channels
        /// </summary>
        public void resetAll()
        {
            this._reset.Write(GpioPinValue.Low);
            Task.Delay(100).Wait();
            this._reset.Write(GpioPinValue.High);
        }
        /// <summary>
        /// switch on all the channells (switches)
        /// </summary>
        public void powerON()
        {
            this._reset.SetDriveMode(GpioPinDriveMode.Output);
            this._reset.Write(GpioPinValue.High);
        }

        /// <summary>
        /// Write to Mux
        /// </summary>
        /// <param name="dataBuffer"></param>
        private void write(byte[] dataBuffer)
        {
            Debug.WriteLine("dataBuffer.Length : " + dataBuffer.Length);
            foreach (var item in dataBuffer)
            {
                Debug.WriteLine("write(databuffer) : " + item.ToString());
            }

            multiplexer.Write(dataBuffer);
        }

        /// <summary>
        /// Connect pins xi to yi. Check for valid pins before (8x10 mux), then OR with _DB15
        /// which effectively sets the MSB to 1 to close switches
        /// </summary>
        /// <param name="xi"></param>
        /// <param name="yi"></param>
        public void connectPins(int xi, int yi)
        {
            if (xi > 9 | yi > 7) return;
            this.write(new Byte[] { (byte)(_DB15 | (byte)(xi << 3) | (byte)(yi)), (byte)1 });
        }

        /// <summary>
        /// Disconnect pins. We don't need to set _DB15 to 0, as leftshifting an int < 15
        /// will effectifely set the MSB to 0, thus opening the switches in the mux
        /// </summary>
        /// <param name="xi"></param>
        /// <param name="yi"></param>
        public void disconnectPins(int xi, int yi)
        {
            if (xi > 9 | yi > 7) return;
            this.write(new Byte[] { (byte)((byte)(xi << 3) | (byte)(yi)) });
        }
    }
}
