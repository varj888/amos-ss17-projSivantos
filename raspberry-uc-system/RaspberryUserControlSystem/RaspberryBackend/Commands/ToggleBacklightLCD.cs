using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Devices.Gpio;

namespace RaspberryBackend.Commands
{
    class ToggleBacklightLCD : Command
    {
        private const byte ON = 0x01;
        private const byte OFF = 0x00;

        public ToggleBacklightLCD(RaspberryPi raspberryPi) : base(raspberryPi)
        {
            RequestController.Instance.addRequestedCommand("ToggleBacklight", this);
        }

        public override void execute(object parameter)
        {
            string requestedParameter = parameter.ToString();

            if (requestedParameter.Equals("1"))
            {
                Debug.WriteLine("Received command LightLED On!");
                switchToState(ON);

            }
            else if (requestedParameter.Equals("0"))
            {
                Debug.WriteLine("Received command LightLED Off!");
                switchToState(OFF);
            }


        }

        private void switchToState(byte targetState)
        {
            RaspberryPi.lcdDisplay._backLight = targetState;
            RaspberryPi.lcdDisplay.sendCommand(targetState);

        }
    }
}
