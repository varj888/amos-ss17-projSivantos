using System;

namespace RaspberryBackend
{
    class ResetPin : Command
    {

        public ResetPin(GPIOinterface gpioInterface) : base(gpioInterface)
        {
            RequestController.Instance.addRequestetCommand("ResetPin", this);
        }


        public override void execute(Object parameter)
        {
            UInt16 id = 0;
            if (parameter.GetType() == typeof(UInt16))
            {
                id = (UInt16)parameter;
                _gpioInterface.setToOutput(id);
                _gpioInterface.writePin(id, 0);
            }
            else
            {
                return;
            }

        }

    }
}
