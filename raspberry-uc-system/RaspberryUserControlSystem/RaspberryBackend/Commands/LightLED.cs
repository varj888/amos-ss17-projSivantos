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
        private const UInt16 GPIO_PIN_ID = 24;

        public string lastStateOnRequest;
        public string currentState;


        public LightLED(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        ///  executes the Command LightLED in dependency of the parsed parameter 
        /// </summary>
        /// <param name="parameter">parameter with content ("0" or "1")</param>
        public override void executeAsync(Object parameter)
        {
            lastStateOnRequest = RaspberryPi.readPin(GPIO_PIN_ID);

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

            Debug.WriteLine(string.Format("Current Value of Pin {0} for writing LED is: {1} and was when requested {2} \n",
                GPIO_PIN_ID, currentState, lastStateOnRequest));
        }



        /// <summary>
        /// can be used to change the state of the (hardware) LED to a new state. 
        /// Note: this method uses the instance constant "PIN_ID" to change the state
        /// => new call then e.g.LED.switchToState(ON);
        /// </summary>
        /// <param name="targetState">the state wished to change to</param>
        /// <returns>The GpioPinValue of the concerned Gpio-Pin</returns>
        private string switch_LED_ToState(uint targetState)
        {
            if( targetState == 0 )
            {
                RaspberryPi.deactivatePin(GPIO_PIN_ID);
            } else
            {
                RaspberryPi.activatePin(GPIO_PIN_ID);
            }
            return RaspberryPi.readPin(GPIO_PIN_ID);
        }

    }
}

