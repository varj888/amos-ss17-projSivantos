using System;

namespace RaspberryBackend
{
    class WritePin : Command
    {

        public WritePin(GPIOinterface gpioInterface) : base(gpioInterface)
        {
            RequestController.Instance.addRequestetCommand("WritePin", this);
        }


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
