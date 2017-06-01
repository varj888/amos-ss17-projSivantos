using System;

namespace RaspberryBackend
{

    /// <summary>
    /// This class represents a Command. It it can be used to reset a spefic gpio pin of the RaspberryPi. 
    /// </summary>
    class ResetPin : Command
    {

        public ResetPin(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// executes the Command ResetPin
        /// </summary>
        /// <param name="parameter">represents the GpioPin which shall be reset</param>
        public override void executeAsync(Object parameter)
        {
            UInt16 id = (UInt16)parameter;
            RaspberryPi.deactivatePin(id);
        }
    }
}
