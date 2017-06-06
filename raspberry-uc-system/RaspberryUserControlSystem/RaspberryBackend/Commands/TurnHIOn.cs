using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABElectronics_Win10IOT_Libraries;

namespace RaspberryBackend
{
    class TurnHIOn : Command
    {
        public TurnHIOn(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// parses the parameter as double voltage and executes turnHI_on() on the RaspberryPi Object
        /// </summary>
        /// <param name="parameter">represents the ADCVoltage to be set, will be clipped to min 0 and max 2.074 volts</param>
        public override void executeAsync(object parameter)
        {
            double voltage = (double) parameter;
            RaspberryPi.turnHI_on(voltage);
        }
    }
}
