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
            RequestController.Instance.addRequestedCommand("WritePin", this);
        }

        /// <summary>
        /// execute the Command WritePin
        /// </summary>
        /// <param name="parameter">represents the GpioPin which shall be written on</param>
        public override void execute(Object parameter)
        {
            UInt16 id = 0;
            if (parameter.GetType() == typeof(UInt16))
            {
                id = (UInt16)parameter;
                RaspberryPi.GpioInterface.setToOutput(id);
                RaspberryPi.GpioInterface.writePin(id, 1);
            }
            else
            {
                return;
            }

        }

    }
}
