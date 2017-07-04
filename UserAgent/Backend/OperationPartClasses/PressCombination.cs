using System;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It activates a combination of pins.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Activate multiple pins at the same time. Currently we use 3 buttons: rockerswitch_down, rockerswitch_up and pushbutton.
        /// </summary>
        /// <param name="parameter">Int 1 for press, duration any int: [rockerswitch_0, rockerswitch_1, pushbutton, duration]</param>
        /// <returns>The provided duration.</returns>
        public string PressCombination(int[] param)
        {
            int duration = param[param.Length - 1];
            if (param.Length != 4)
            {
                throw new Exception("Invalid parameterlist received");
            }

            if (param[1] == 1 & param[0] == 1)
            {
                throw new Exception("Tester tried to press both rockerswitches in combination");
            }

            if (param[2] == 1)
            {
                GPIOinterface.activatePin(GpioMap.pushButton_Pin);
            }
            if (param[1] == 1 & param[0] == 0)
            {
                GPIOinterface.activatePin(GpioMap.rockerSwitchUpPin);
            }
            if (param[0] == 1 & param[1] == 0)
            {
                GPIOinterface.activatePin(GpioMap.rockerSwitchDownPin);
            }
            Task.Delay(duration).Wait();
            GPIOinterface.deactivatePin(GpioMap.pushButton_Pin);
            GPIOinterface.deactivatePin(GpioMap.rockerSwitchDownPin);
            GPIOinterface.deactivatePin(GpioMap.rockerSwitchUpPin);

            return duration.ToString();
        }
    }
}
