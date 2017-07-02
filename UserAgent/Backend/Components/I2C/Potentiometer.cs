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
        /* Stores value that potentiometer has most recently been set to.
         * This information isn't based on hardware read-backs, but instead based
         * on values that are expected to be true. Thus HW-based errors aren't accounted for.
         * Improving this in further iteration might be necessary. */
        public byte WiperState { get; private set; }

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

        public void write(byte data) => write(new[] { data });

        /// <summary>
        /// Used to send data to the MCP4018 Potentiometer
        /// </summary>
        /// <param name="dataBuffer">Contains a single Byte (0...127) which represents the wiper state of the Potentiometer. 127:= Max Voltage, 0:= Min Voltage</param>
        public void write(byte[] dataBuffer)
        {
            potentiometer.Write(dataBuffer);
            this.WiperState = dataBuffer[0]; // update wiperState to current value
        }
    }
}