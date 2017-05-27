using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the MCP4018 Chip which is a potentiometer to regulate the Voltage. 
    /// </summary>
    public class Potentiometer
    {

        // use these constants for controlling how the I2C bus is setup
        private const string I2C_CONTROLLER_NAME = "I2C1";
        private const byte POTENTIOMETER_I2C_ADDRESS = 0x2F;
        private I2cDevice potentiometer;

        /// <summary>
        /// starts the I2C communication with the LCD Display
        /// </summary>
        public async void startI2C()
        {
            try
            {

                var i2cSettings = new I2cConnectionSettings(POTENTIOMETER_I2C_ADDRESS);
                i2cSettings.BusSpeed = I2cBusSpeed.FastMode;
                string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
                var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);
                this.potentiometer = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, i2cSettings);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }
        }


        public Potentiometer()
        {
            startI2C();
        }


        /// <summary>
        /// is used to send data to the MCP4018
        /// </summary>
        /// <param name="dataBuffer">contains a single byte 0...127 which represents the wiper state of the potentiometer. 127:= Max Voltage, 0:= Min Voltage</param>
        public void write(byte[] dataBuffer)
        {

            potentiometer.Write(dataBuffer);
        }


    }
}