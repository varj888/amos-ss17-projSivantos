using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace RaspberryBackend.Data
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


        //private static I2cInstance;

        public Multiplexer()
        {
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
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }
        }

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

        }
        /// <summary>
        /// switch on all the channells (switches)
        /// </summary>
        public void powerON()
        {

        }

        public void write(byte[] dataBuffer)
        {
            multiplexer.Write(dataBuffer);
        }
    }
}
