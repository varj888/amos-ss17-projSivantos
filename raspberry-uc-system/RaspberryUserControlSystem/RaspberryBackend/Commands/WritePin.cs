using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to write on a spefic gpio pin of the RaspberryPi.
    /// </summary>
    class WritePin : Command
    {

        public WritePin(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// execute the Command WritePin
        /// </summary>
        /// <param name="gpioPinID">represents the GpioPin:Uint16 which shall be written on</param>
        public override void executeAsync(Object gpioPinID)
        {
            UInt16 id = (UInt16)gpioPinID;
            RaspberryPi.activatePin(id);
        }
    }
}
