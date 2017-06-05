using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABElectronics_Win10IOT_Libraries;
using System.Diagnostics;

namespace RaspberryBackend.Components.SPI
{
    /// <summary>
    /// Software representation of the A/D Converter
    /// (MCP3202)
    /// </summary>
    public class ADConverter
    {
        private ADCDACPi adcdac;

        private byte[] channel = new byte[] { 1, 2 };

        private readonly double MIN_REFVOLTAGE = 0.0;
        private readonly double MAX_REFVOLTAGE = 7.0;

        private readonly double MIN_CHVOLTAGE = 0.0;
        private readonly double MAX_CHVOLTAGE = 2.047;
        public ADConverter()
        {
            adcdac = new ADCDACPi();
        }
        /// <summary>
        /// connect to device and set
        /// reference and channel voltages to the channel
        /// </summary>
        /// <param name="channel">can be 1 or 2</param>
        /// <param name="referenceVoltage">value between 0.0 and 7.0</param>
        /// <param name="channelVoltage">can be between 0 and 2.047 volts</param>
        public void initiate(byte channel, double referenceVoltage, double channelVoltage)
        {
            try
            {
                adcdac.Connect();
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
            while (adcdac.IsConnected)
            {
                adcdac.SetADCrefVoltage(referenceVoltage);
                double value = adcdac.ReadADCVoltage(channel);
                Debug.WriteLine("ADCVoltage: " + value + "V");
            }
        }
    }
}
