using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    public partial class Operation
    {
        private readonly byte LED_CHANNEL = 2;
        private readonly ushort AVERAGE_TIMES = 4;
        private readonly double LED_OFF_THRESHOLD = 0.1;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="x">X-Side pin (output)</param>
        /// <param name="y">Y-Side pin (input)</param>
        /// <returns></returns>
        public bool CheckLEDStatus(int i)
        {
            //wait for 1 sec to be sure the voltage on the board has changed
            Task.Delay(1000);

            double averageVoltage = ADConverter.updateCurrentADCVoltage2Average(AVERAGE_TIMES);
            Debug.WriteLine("Average Voltage on In2 is: " + averageVoltage);

            if (averageVoltage < LED_OFF_THRESHOLD)
            {
                return false;
            }

            return true;
        }
    }
}