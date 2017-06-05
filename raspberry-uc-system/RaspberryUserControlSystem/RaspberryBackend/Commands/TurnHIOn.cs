using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABElectronics_Win10IOT_Libraries;

namespace RaspberryBackend.Commands
{
    class TurnHIOn : Command
    {
        /// <param name="channel">can be 1 or 2</param>
        /// <param name="referenceVoltage">value between 0.0 and 7.0</param>
        /// <param name="channelVoltage">can be between 0 and 2.047 volts</param>

       
        public TurnHIOn(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        public override void executeAsync(object parameter)
        {
            //List<double> paramList = (List<double>)parameter;
            //byte channel = Convert.ToByte(paramList.ElementAt(0));
            //double refVoltage = paramList.ElementAt(1);
            //double chVoltage = paramList.ElementAt(2);
            byte channel = 1;
            double refVoltage = (double)parameter;
            double chVoltage = 1.5;
            RaspberryPi.turnHI_on(channel, refVoltage, chVoltage);
        }
    }
}
