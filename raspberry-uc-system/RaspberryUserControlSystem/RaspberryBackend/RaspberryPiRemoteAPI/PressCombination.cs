using CommonFiles.TransferObjects;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It activates a combination of pins.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Activate multiple pins at the same time. Currently we use 3 buttons: rockerswitch_down, rockerswitch_up and pushbutton.
        /// </summary>
        /// <param name="parameter">Int 1 for press, duration any int: [rockerswitch_0, rockerswitch_1, pushbutton, duration]</param>
        public Result PressCombination(int[] param)
        {
            int duration = param[param.Length - 1];
            if (param.Length != 4)
            {
                return new Result("Invalid parameterlist received"); ;
            }

            if (param[1] == 1 & param[0] == 1)
            {
                return new Result("Tester tried to press both rockerswitches in combination");
            }

            if (param[2] == 1)
            {
                activatePin(pushButton_Pin);
            }
            if (param[1] == 1 & param[0] == 0)
            {
                activatePin(rockerSwitch_Pin_1);
            }
            if (param[0] == 1 & param[1] == 0)
            {
                activatePin(rockerSwitch_Pin_0);
            }
            Task.Delay(duration).Wait();
            deactivatePin(pushButton_Pin);
            deactivatePin(rockerSwitch_Pin_0);
            deactivatePin(rockerSwitch_Pin_1);

            return new Result(true, this.GetType().Name, duration.ToString());
        }
    }
}
