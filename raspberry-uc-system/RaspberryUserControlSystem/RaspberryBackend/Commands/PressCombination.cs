using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It activates a combination of pins.
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
        public override void executeAsync(Object[] parameters)
        {
            object parameter = parameters[0];
            int[] param = (int[])parameter;
            int duration = param[param.Length - 1];
            if(param.Length != 4)
            {
                Debug.WriteLine("Invalid parameterlist received");
                return;
            }

            if(param[1] == 1 & param[0] == 1)
            {
                Debug.WriteLine("Tester tried to press both rockerswitches in combination");
                return;
            }

            if (param[2] == 1)
            {
                RaspberryPi.activatePin(pushButton_Pin);
            }
            if (param[1] == 1 & param[0] == 0)
            {
                RaspberryPi.activatePin(rockerSwitch_Pin_1);
            }
            if (param[0] == 1 & param[1] == 0)
            {
                RaspberryPi.activatePin(rockerSwitch_Pin_0);
            }
            Task.Delay(duration).Wait();
            RaspberryPi.deactivatePin(pushButton_Pin);
            RaspberryPi.deactivatePin(rockerSwitch_Pin_0);
            RaspberryPi.deactivatePin(rockerSwitch_Pin_1);
        }
    }
}
