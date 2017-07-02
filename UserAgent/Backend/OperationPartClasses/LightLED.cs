using System;
using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It lights the LED on if the Request-Parameter is 1 and off if 0.
    /// </summary>
    public partial class Operation
    {
        public string lastStateOnRequest;
        public string currentState;
        private const UInt16 GPIO_PIN_ID = 24;

        /// <summary>
        /// Executes the Command LightLED in dependency of the parsed parameter
        /// </summary>
        /// <param name="parameter">Int to turn on or off the LED (0 or 1)</param>
        /// <returns>The requested parameter.</returns>
        public string LightLED(Int32 requestedParameter)
        {
            const uint ON = 1;
            const uint OFF = 0;

            lastStateOnRequest = GPIOinterface.readPin(GPIO_PIN_ID);

            if (requestedParameter == ON)
            {
                Debug.WriteLine("Received command LightLED On!");
                currentState = switch_LED_ToState(ON);
            }
            else if (requestedParameter == OFF)
            {
                Debug.WriteLine("Received command LightLED Off!");
                currentState = switch_LED_ToState(OFF);
            }

            Debug.WriteLine(string.Format("Current Value of Pin {0} for writing LED is: {1} and was when requested {2} \n",
                GPIO_PIN_ID, currentState, lastStateOnRequest));
            return requestedParameter.ToString();
        }



        /// <summary>
        /// Can be used to change the state of the (hardware) LED to a new state.
        /// Note: this method uses the instance constant "PIN_ID" to change the state
        /// => new call then e.g.LED.switchToState(ON);
        /// </summary>
        /// <param name="targetState">the state wished to change to</param>
        /// <returns>The GpioPinValue of the concerned Gpio-Pin</returns>
        private string switch_LED_ToState(uint targetState)
        {
            if (targetState == 0)
            {
                GPIOinterface.deactivatePin(GPIO_PIN_ID);
            }
            else
            {
                GPIOinterface.activatePin(GPIO_PIN_ID);
            }
            return GPIOinterface.readPin(GPIO_PIN_ID);
        }

    }
}

