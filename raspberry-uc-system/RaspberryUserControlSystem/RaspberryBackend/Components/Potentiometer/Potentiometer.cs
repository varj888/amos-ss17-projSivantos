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

namespace RaspberryBackend
{
    public class Potentiometer
    {
        // use these constants for controlling how the I2C bus is setup
        private const string I2C_CONTROLLER_NAME = "I2C1";
        private const byte POTENTIOMETER_I2C_ADDRESS = 0x2F;

        private I2cDevice potentiometer;
        private byte[] dataBufferON = new byte[] { 0x7F };
        private byte[] dataBufferOFF = new byte[] { 0x00 };


        public I2cDevice _potentiometer { get { return potentiometer; } }

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

        public void turnOn()
        {
            potentiometer.Write(dataBufferON);
        }

        public void turnOff()
        {
            potentiometer.Write(dataBufferOFF);
        }

        public Potentiometer()
        {
            startI2C();
        }


    }
}