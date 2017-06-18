using CommonFiles.TransferObjects;
using System;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It simulates a rockerswitch.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Execute presssing one rockerswitch
        /// </summary>
        /// <param name="parameter">Expects an int-Array containing id = [0|1] and duration</param>
        public string PressRockerSwitch(int[] param)
        {

            if (param.Length != 2)
            {
                throw new Exception("Received invalid paremeterlist");
            }

            int rsw = param[0];
            int duration = param[1];

            if (rsw < 0 | rsw > 1)
            {
               throw new Exception("Invalid Rockerswitch submitted");
            }

            UInt16 pushButton_Pin;

            if (rsw == 0)
            {
                pushButton_Pin = rockerSwitch_Pin_0;
            }
            else
            {
                pushButton_Pin = rockerSwitch_Pin_1;
            }

            activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            deactivatePin(pushButton_Pin);

            return param.ToString();
        }
    }
}
