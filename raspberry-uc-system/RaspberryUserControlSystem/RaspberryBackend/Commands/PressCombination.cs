using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It connects two requested Pins (xPin,yPin) on the Multiplexer.
    /// </summary>
    class PressCombination : Command
    {

        public PressCombination(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// Activate multiple pins at the same time. Currently we use 3 buttons: rockerswitch_down, rockerswitch_up and pushbutton.
        /// </summary>
        /// <param name="parameter">Int 1 for press, duration any int: [rockerswitch_0, rockerswitch_1, pushbutton, duration]</param>
        public override void executeAsync(Object parameter)
        {
            int[] param = (int[])parameter;
            int duration = param[param.Length - 1];
            if(param.Length != 3)
            {
                Debug.WriteLine("Invalid parameterlist received");
                return;
            }
            if (param[0] == 1)
            {
                RaspberryPi.activatePin(rockerSwitch_Pin_0);
            }
            if (param[1] == 1)
            {
                RaspberryPi.activatePin(rockerSwitch_Pin_1);
            }
            if (param[2] == 1)
            {
                RaspberryPi.activatePin(pushButton_Pin);
            }
            Task.Delay(duration).Wait();
            RaspberryPi.deactivatePin(pushButton_Pin);
            RaspberryPi.deactivatePin(rockerSwitch_Pin_0);
            RaspberryPi.deactivatePin(rockerSwitch_Pin_1);
        }
    }
}
