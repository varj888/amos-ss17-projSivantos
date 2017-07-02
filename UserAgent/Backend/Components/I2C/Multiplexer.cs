using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Devices.I2c;

namespace RaspberryBackend
{
    /// <summary>
    /// represents the physical multiplexer (currently using ADG2128)
    /// and implements the core functionality of the device
    /// ADG2128 is an analog cross point switch with an array size of 8 x 12
    /// </summary>
    public class Multiplexer : HWComponent
    {
        /// <summary>
        /// total ammount of available switches (8 x 12 --> 96)
        /// </summary>
        private readonly int switches = 96;

        // use these constants for controlling how the I2C bus is setup
        private const byte MULTIPLEXER_I2C_ADDRESS = 0x70;
        private I2cDevice multiplexer;
        private byte _DB15 = 0x80;
        private GpioPin _reset;

        //private Dictionary<int, Tuple<int, string>> current_multiplexer_state = new Dictionary<int, Tuple<int, string>>();
        public Dictionary<int, Tuple<int, string>> current_multiplexer_state { get; private set; } = new Dictionary<int, Tuple<int, string>>();

        public override void initiate()
        {
            try
            {
                Task.Run(() => I2C.connectDeviceAsync(MULTIPLEXER_I2C_ADDRESS, true, false)).Wait();
                I2C.connectedDevices.TryGetValue(MULTIPLEXER_I2C_ADDRESS, out multiplexer);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Problem with I2C : " + e.Message);
            }

            _initialized = true;
        }



        /// <summary>
        /// Gets the Y pin currently connected to the X Pin
        /// </summary>
        /// <param name="xi">specified X Pin on the multiplexer</param>
        /// <returns>
        /// Returns the Y pin currenctly connected to the X Pin
        /// Returns -1 if key is not found
        /// </returns>
        public int get_Y_conntected_to_X(int xi)
        {
            if (current_multiplexer_state.ContainsKey(xi))
            {
                return current_multiplexer_state[xi].Item1;
            }

            return -1;
        }

        /// <summary>
        /// Gets the Value pin currently connected to the X Pin
        /// </summary>
        /// <param name="xi">specified X Pin on the multiplexer</param>
        /// <returns>
        /// Returns the Value currenctly connected to the X Pin, e.g.: "RockerSW"
        /// Returns an empty string if key is not found
        /// </returns>
        public string get_Value_conntected_to_X(int xi)
        {
            if (current_multiplexer_state.ContainsKey(xi))
            {
                return current_multiplexer_state[xi].Item2;
            }

            return "";
        }

        /// <summary>
        /// Sets the reset Pin of the Multiplexer and powers it on.
        /// </summary>
        /// <param name="reset">gpioPin ID which will be used to reset the Multiplexer</param>
        public void setResetPin(GpioPin reset)
        {
            _reset = reset;
            this.powerON();
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
            //resetting the multiplexer
            if (isInitialized())
            {
                this._reset.Write(GpioPinValue.Low);
                Task.Delay(100).Wait();
                this._reset.Write(GpioPinValue.High);
            }
            // resets the pin dictionary
            current_multiplexer_state.Clear();
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
        /// which effectively sets the MSB to 1 to close switches. For x Pins above 5 it is
        /// necessary to add 2 to x1 due to reserved codewords. Compare documentation of ADG2108
        /// or ADG2128.
        /// </summary>
        /// <param name="xi"></param>
        /// <param name="yi"></param>
        public void connectPins(int xi, int yi)
        {
            if (xi > 9 | yi > 7) return;
            if (xi > 5) xi = xi + 2;
            this.write(new byte[] { (byte)(_DB15 | (byte)(xi << 3) | (byte)(yi)), (byte)1 });
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
            if (xi > 5) xi = xi + 2;
            this.write(new byte[] { (byte)((byte)(xi << 3) | (byte)(yi)) });
        }
    }
}
