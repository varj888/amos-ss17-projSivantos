using ABElectronics_Win10IOT_Libraries;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the A/D Converter
    /// (MCP3202)
    /// </summary>
    public class ADConverter : HWComponent
    {
        private ADCDACPi _adConvert = new ADCDACPi();

        private readonly byte CHANNEL = 0x1;
        private readonly double MIN_VOLTAGE = 0.0;
        private readonly double MAX_VOLTAGE = 1.5;
        private readonly double STANDARD_VOLTAGE = 1.0;

        private double currentDACVoltage = -1;

        public override void initiate()
        {
            connect();

            Debug.WriteLine(this.GetType().Name + "::: Setting DACVoltage to standard 1.0 volts.");
            setDACVoltage(STANDARD_VOLTAGE);
            _initialized = true;
        }

        /// <summary>
        /// connect to device
        /// </summary>
        private void connect()
        {
            Debug.WriteLine(this.GetType().Name + "::: Connecting to SPI Device...");

            Task.Run(() => _adConvert.Connect()).Wait();
            Task.Delay(5000).Wait();

            Debug.WriteLine(this.GetType().Name + "::: Conntected Status is: " + _adConvert.IsConnected);

            if (_adConvert.IsConnected == false)
            {
                throw new Exception("ADCDAC Connection failure.");
            }
            else
            {
                Debug.WriteLine(this.GetType().Name + "::: DACDAC is ready for setting voltages!");
            }
        }

        public bool isConnected()
        {
            return _adConvert.IsConnected;
        }

        /// <summary>
        /// method to set the DAC voltage on the ADCDAC Channel 1
        /// </summary>
        /// <param name="voltage">can be between 0 and 2.047 volts</param>
        public void setDACVoltage(double voltage)
        {
            if (voltage > MAX_VOLTAGE)
            {
                voltage = MAX_VOLTAGE;
            }
            else if (voltage < MIN_VOLTAGE)
            {
                voltage = MIN_VOLTAGE;
            }

            //happens only if ADCDAC is actually connected
            if (_adConvert.IsConnected)
            {
                _adConvert.SetDACVoltage(CHANNEL, voltage);
            }

            currentDACVoltage = voltage;
        }

        public double getDACVoltage()
        {
            return currentDACVoltage;
        }
    }
}
