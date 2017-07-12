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

        private readonly byte CHANNEL_1 = 0x1;
        private readonly byte CHANNEL_2 = 0x2;
        private readonly double MIN_VOLTAGE = 0.0;
        private readonly double MAX_VOLTAGE = 1.5;
        private readonly double STANDARD_VOLTAGE = 1.0;

        private double currentDACVoltage1 = -1;
        private double currentDACVoltage2 = -1;

        public override void initiate()
        {
            connect();

            Debug.WriteLine(this.GetType().Name + "::: Setting DACVoltage to standard 1.0 volts.");
            setDACVoltage1(STANDARD_VOLTAGE);

            _initialized = true;
        }

        public double getMaxVoltage()
        {
            return this.MAX_VOLTAGE;
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
        public void setDACVoltage(double voltage, byte channel)
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
                Debug.WriteLine(channel + "   " + voltage);
                _adConvert.SetDACVoltage(channel, voltage);
                if (channel == CHANNEL_1)
                {
                    currentDACVoltage1 = voltage;
                }
                else if (channel == CHANNEL_2)
                {
                    currentDACVoltage2 = voltage;
                }
            }
        }

        public double getDACVoltage1()
        {
            return currentDACVoltage1;
        }
        public double getDACVoltage2()
        {
            return currentDACVoltage2;
        }

        internal void setADCRefVoltage(double v)
        {
            _adConvert.SetADCrefVoltage(v);
        }

 

        /// <summary>
        /// Wrapper around setDACVoltage so set channels without knowing their addresss
        /// </summary>
        /// <param name="voltage"></param>
        public void setDACVoltage1(double voltage)
        {
            this.setDACVoltage(voltage, this.CHANNEL_1);
        }

        /// <summary>
        /// Wrapper around setDACVoltage so set channels without knowing their addresss
        /// </summary>
        /// <param name="voltage"></param>
        public void setDACVoltage2(double voltage)
        {
            this.setDACVoltage(voltage, this.CHANNEL_2);
        }

        public void readADCVoltage(int times, byte channel)
        {
            if (channel < 1 || channel > 2) return;

            for (int i = 0; i < times; i++)
            {
                Debug.WriteLine("ADC In" + channel + " Voltage is: {0}", _adConvert.ReadADCVoltage(channel));
                Task.Delay(1000).Wait();
            }
        }
    }
}
