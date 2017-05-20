using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to write on a spefic gpio pin of the RaspberryPi. 
    /// </summary>
    class WritePin : Command
    {

        public WritePin(GPIOinterface gpioInterface) : base(gpioInterface)
        {
            RequestController.Instance.addRequestetCommand("WritePin", this);
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
                _gpioInterface.setToOutput(id);
                _gpioInterface.writePin(id, 1);
            }
            else
            {
                return;
            }

        }

    }
}
