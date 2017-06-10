using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to read a spefic gpio pin of the RaspberryPi. 
    /// </summary>
    class PressPushButton : Command
    {

        public PressPushButton(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// executes the Command ReadPin 
        /// </summary>
        /// <param name="parameter">UInt16 Duration</param>
        public override void executeAsync(Object parameter)
        {
            int duration = (int)parameter;
            RaspberryPi.activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            RaspberryPi.deactivatePin(pushButton_Pin);
        }
    }
}
