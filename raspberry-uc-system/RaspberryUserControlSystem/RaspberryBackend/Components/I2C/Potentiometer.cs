using System;
using System.Threading.Tasks;
using Windows.Devices.I2c;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the MCP4018 Chip which is a potentiometer to regulate the Voltage.
    /// </summary>
    public class Potentiometer
    {
        // use these constants for controlling how the I2C bus is setup
        private const byte POTENTIOMETER_I2C_ADDRESS = 0x2F;
        private I2cDevice potentiometer;
        private Boolean _initialized = false;

        /// <summary>
        /// creates and initializes the Potentiometer
        /// </summary>
        public Potentiometer()
        {
            try
            {
                Task.Run(() => I2C.connectDeviceAsync(POTENTIOMETER_I2C_ADDRESS, true, false)).Wait();
                I2C.connectedDevices.TryGetValue(POTENTIOMETER_I2C_ADDRESS, out potentiometer);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Problem with I2C " + e.Message);
                throw e;
            }

            _initialized = true;
        }

        /// <summary>
        /// returns whether the Potentiometer is initialized or not
        /// </summary>
        /// <returns>true:= initialize and false:= not initialized</returns>
        public Boolean isInitialized()
        {
            return _initialized;
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