using System;
using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It lights the LED on if the Request-Parameter is 1 and off if 0. 
    /// </summary>
    class LightLED : Command
    {
        public LightLED(GPIOinterface gpioInterface) : base(gpioInterface)
        {
            RequestController.Instance.addRequestetCommand("LightLED", this);
        }

        //Suggestion: A instance variable which helds the last known state in order to revert it
        //private static Object lastState

        public override void execute(Object parameter)
        {
            string requesParameter = parameter.ToString();

            UInt16 id = 6;


            if (requesParameter.Equals("1"))
            {
                Debug.WriteLine("Received write command!");
                _gpioInterface.setToOutput(id);
                _gpioInterface.writePin(id, 1);
                Debug.WriteLine(_gpioInterface.readPin(id));
            }
            else if (requesParameter.Equals("0"))
            {
                Debug.WriteLine("Received reset command!");
                _gpioInterface.setToOutput(id);
                _gpioInterface.writePin(id, 0);
            }


        }

        public override void undo()
        {
            throw new NotImplementedException();
        }
    }
}


//if (parameter.Equals("1"))
//           {
//               //Execute appropiate method in GPIOinterface like e.g. gpio.led(1)
//               gpio.setToOutput(5);
//               gpio.setToInput(6);
//               gpio.writePin(5, 1);
//               Debug.WriteLine("LED switched On");
//           }

//           else if (parameter.Equals("0"))
//           {
//               //gpio.writePin(6, 0);
//               //Execute appropiate method in GPIOinterface like e.g. gpio.led(0);
//               Debug.WriteLine("LED switched Off");
//           }

//           else
//           {
//               Debug.WriteLine("no valid parameter");
//           }