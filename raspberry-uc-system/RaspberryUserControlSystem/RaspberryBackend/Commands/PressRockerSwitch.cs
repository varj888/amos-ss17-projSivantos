using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It simulates a rockerswitch. 
    /// </summary>
    class PressRockerSwitch : Command
    {

        public PressRockerSwitch(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// Execute presssing one rockerswitch
        /// </summary>
        /// <param name="parameter">Expects an int-Array containing id = [0|1] and duration</param>
        public override void executeAsync(Object parameter)
        {
            int[] param = (int[])parameter;

            if (param.Length != 2)
            {
                Debug.WriteLine("Received invalid paremeterlist");
                return;
            }

            int rsw = param[0];
            int duration = param[1];

            if (rsw < 0 | rsw > 1)
            {
                Debug.WriteLine("Invalid Rockerswitch submitted");
                return;
            }

            UInt16 pushButton_Pin;

            if (rsw == 0)
            {
                pushButton_Pin = rockerSwitch_Pin_0;
            } else
            {
                pushButton_Pin = rockerSwitch_Pin_1;
            }

            RaspberryPi.activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            RaspberryPi.deactivatePin(pushButton_Pin);
        }
    }
}
