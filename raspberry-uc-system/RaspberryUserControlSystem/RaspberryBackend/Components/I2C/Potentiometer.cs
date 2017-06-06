using System;
using System.Threading.Tasks;
using Windows.Devices.I2c;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the MCP4018 Chip which is a potentiometer to regulate the Voltage.
    /// </summary>
    public class Potentiometer : HWComponent
    {
        // use these constants for controlling how the I2C bus is setup
        private const byte POTENTIOMETER_I2C_ADDRESS = 0x2F;
        private I2cDevice potentiometer;

        public override void initiate()
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
        /// is used to send data to the MCP4018
        /// </summary>
        /// <param name="dataBuffer">contains a single byte 0...127 which represents the wiper state of the potentiometer. 127:= Max Voltage, 0:= Min Voltage</param>
        public void write(byte[] dataBuffer)
        {
            potentiometer.Write(dataBuffer);
        }
    }
}