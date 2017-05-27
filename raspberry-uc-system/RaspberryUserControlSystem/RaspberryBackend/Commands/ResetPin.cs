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
            RequestController.Instance.addRequestedCommand("ResetPin", this);
        }

        /// <summary>
        /// executes the Command ResetPin
        /// </summary>
        /// <param name="parameter">represents the GpioPin which shall be reset</param>
        public override void execute(Object parameter)
        {
            UInt16 id = 0;
            if (parameter.GetType() == typeof(UInt16))
            {
                id = (UInt16)parameter;
                RaspberryPi.GpioInterface.setToOutput(id);
                RaspberryPi.GpioInterface.writePin(id, 0);

            }
            else
            {
                return;
            }

        }

    }
}
