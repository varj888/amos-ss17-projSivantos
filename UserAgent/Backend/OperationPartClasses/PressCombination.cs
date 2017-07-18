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
        /// <param name="parameter"> string[]={ pushbutton_0, rockerswitch_1, rockerswitch_2, durationCategorie_3 } <see cref="DurationConfig"</param>
        /// <returns>The provided duration.</returns>
        public string PressCombination(string[] param)
        {
            int duration = DurationConfig.getDuration(param[param.Length - 1]);
            if (param.Length != 4)
            {
                throw new Exception("Invalid parameterlist received");
            }

            if (param[1] == "RSD" & param[2] == "RSU")
            {
                throw new Exception("Tester tried to press both rockerswitches in combination");
            }

            if (param[2] == "RSD")
            {
                GPIOinterface.activatePin(GpioMap.rockerSwitchDownPin);
            }
            if (param[1] == "RSU" & param[0] == null)
            {
                GPIOinterface.activatePin(GpioMap.rockerSwitchUpPin);
            }
            if (param[0] == "PB" & param[1] == null)
            {
                GPIOinterface.activatePin(GpioMap.pushButton_Pin);
            }

            Task.Delay(duration).Wait();
            GPIOinterface.deactivatePin(GpioMap.pushButton_Pin);
            GPIOinterface.deactivatePin(GpioMap.rockerSwitchDownPin);
            GPIOinterface.deactivatePin(GpioMap.rockerSwitchUpPin);

            return duration.ToString();
        }
    }
}
