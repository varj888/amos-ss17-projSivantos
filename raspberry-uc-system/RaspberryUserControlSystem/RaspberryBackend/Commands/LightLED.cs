using System;
using System.Diagnostics;
using Windows.Devices.Gpio;

namespace RaspberryBackend
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
                currentState = switch_LED_ToState(ON);

            }
            else if (requestedParameter.Equals("0"))
            {
                Debug.WriteLine("Received command LightLED Off!");
                currentState = switch_LED_ToState(OFF);
            }

            Debug.WriteLine(string.Format("Current Value of Pin {0} for writing LED is: {1} and was when requested {2}",
                PIN_ID, currentState, lastStateOnRequest));

        }

        /**
         * <summary
         *  can be used to change the state of the (hardware) LED to a new state. 
         *  Note that this method uses the instance constant "PIN_ID" to change the state
         *  (Suggestion: modifying for general purpose and implementing it in an LED object  
         *  => new call then e.g. LED.switchToState(ON);
         * </summary>
        **/
        private GpioPinValue switch_LED_ToState(uint targetState)
        {
            _gpioInterface.setToOutput(PIN_ID);
            _gpioInterface.writePin(PIN_ID, targetState);

            return _gpioInterface.readPin(PIN_ID);
        }

        /// <summary>
        /// can be used to reset to the orignal state before it was changed on request 
        /// </summary>
        public override void undo()
        {
            throw new NotImplementedException();
        }
    }
}
