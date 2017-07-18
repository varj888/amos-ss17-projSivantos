﻿using System.Threading.Tasks;
using static RaspberryBackend.GpioMap;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to read a spefic gpio pin of the Operation.
    /// </summary>
    public partial class Operation
    {


        /// <summary>
        /// Executes the Command PressPushButton. For this the respective Pushbutton is activated and deactivated after
        /// a user-provided duration using the raspberry-class methods.
        /// </summary>
        /// <param name="parameter">string DurationCategorie <see cref="DurationConfig"/></param>
        /// <returns>The provided duration as string.</returns>
        public string PressPushButton(string durationCategorie)
        {
            int duration = DurationConfig.getDuration(durationCategorie);

            GPIOinterface.activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            GPIOinterface.deactivatePin(pushButton_Pin);

            return duration.ToString();
        }
    }
}
