using System;
using System.Diagnostics;
using Windows.Devices.Gpio;

namespace RaspberryBackend
{
    class ReadPin : Command
    {
        public GpioPinValue currentState;

        public ReadPin(GPIOinterface gpioInterface) : base(gpioInterface)
        {
            RequestController.Instance.addRequestetCommand("ReadPin", this);
        }


        public override void execute(Object parameter)
        {
            UInt16 id = 0;
            if (parameter.GetType() == typeof(UInt16))
            {
                id = (UInt16)parameter;
                _gpioInterface.setToInput(id);
                currentState = _gpioInterface.readPin(id);
                Debug.Write(string.Format("Pin {0} has currently the state: ", parameter.ToString()));
                Debug.WriteLine(currentState);
            }
            else
            {
                return;
            }

        }

    }
}
