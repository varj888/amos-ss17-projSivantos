using System;
using System.Diagnostics;
using Windows.Devices.Gpio;

namespace RaspberryPiBackend
{
    /// <summary>
    /// This class represents a Command. It lights the LED on if the Request-Parameter is 1 and off if 0. 
    /// </summary>
    class LightLED : Command
    {
        private const uint ON = 1;
        private const uint OFF = 0;
        private const UInt16 PIN_ID = 6;

        public GpioPinValue lastStateOnRequest;
        public GpioPinValue currentState;


        public LightLED(GPIOinterface gpioInterface) : base(gpioInterface)
        {
            RequestController.Instance.addRequestetCommand("LightLED", this);
            lastStateOnRequest = _gpioInterface.readPin(PIN_ID);
        }


        public override void execute(Object parameter)
        {
            string requestedParameter = parameter.ToString();

            if (requestedParameter.Equals("1"))
            {
                Debug.WriteLine("Received command LightLED On!");
                currentState = switch_LED_ToState(PIN_ID, ON);

            }
            else if (requestedParameter.Equals("0"))
            {
                Debug.WriteLine("Received command LightLED Off!");
                currentState = switch_LED_ToState(PIN_ID, OFF);
            }

            Debug.WriteLine(string.Format("Current Value of Pin {0} for writing LED is: {1} and was when requested {2}",
                PIN_ID, currentState, lastStateOnRequest));

        }

        private GpioPinValue switch_LED_ToState(ushort pinID, uint targetState)
        {
            _gpioInterface.setToOutput(pinID);
            _gpioInterface.writePin(pinID, targetState);

            return _gpioInterface.readPin(PIN_ID);
        }

        public override void undo()
        {
            throw new NotImplementedException();
        }
    }
}
