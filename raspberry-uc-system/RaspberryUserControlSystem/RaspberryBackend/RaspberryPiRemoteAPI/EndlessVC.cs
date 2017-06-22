using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to set endless volume-control.
    /// </summary>
    public partial class RaspberryPi
    {
        /// <summary>
        /// Execute the Command EndlessVCUp. The command uses the Ticks_Counter to its max-value, until the max-value is
        /// reached, it activates the respective pushButton for rocker-switch up for 50ms.
        /// </summary>
        /// <returns>The Ticks_counter representing the current state of volume.</returns>
        public int EndlessVCUp(int ticks)
        {
            Debug.WriteLine("EndlessVCUp ::: Pressing RockerSwitch up " + ticks + " times.");
            pressRockerSwitch(rockerSwitch_Pin_1, ticks);
            return ticks;
        }

        /// <summary>
        /// Execute the Command EndlessVCDown. The command uses the Ticks_Counter to its min-value, until the min-value is
        /// reached, it activates the respective pushButton for rocker-switch up for 50ms.
        /// </summary>
        /// <returns>The Ticks_counter representing the current state of volume.</returns>
        public int EndlessVCDown(int ticks)
        {
            Debug.WriteLine("EndlessVCDown ::: Pressing RockerSwitch down " + ticks + " times.");
            pressRockerSwitch(rockerSwitch_Pin_0, ticks);
            return ticks;
        }

        private void pressRockerSwitch(UInt16 pin, int ticks)
        {
            for (int i = 0; i < ticks; ++i)
            {
                activatePin(pin);
                Task.Delay(50).Wait();
                deactivatePin(pin);
            }
        }
    }
}
