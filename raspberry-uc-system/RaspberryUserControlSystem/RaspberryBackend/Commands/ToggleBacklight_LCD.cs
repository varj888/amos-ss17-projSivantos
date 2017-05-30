using System.Diagnostics;

namespace RaspberryBackend
{

    /// <summary>
    /// This class represents a Command. It it can be used to toggle the Backlight of a I2C connected LCD on the RaspberryPi. 
    /// </summary>
    class ToggleBacklight_LCD : Command
    {
        private const byte ON = 0x01;
        private const byte OFF = 0x00;

        public ToggleBacklight_LCD(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        public override void executeAsync(object parameter)
        {
            string requestedParameter = parameter.ToString();

            if (requestedParameter.Equals("1"))
            {
                Debug.WriteLine("Received command ToggleBacklightLCD On!");
                switchToState(ON);

            }
            else if (requestedParameter.Equals("0"))
            {
                Debug.WriteLine("Received command ToggleBacklightLCD Off!");
                switchToState(OFF);
            }


        }

        private void switchToState(byte targetState)
        {
            RaspberryPi.setLCDBackgroundState(targetState);
            Debug.WriteLine("Backlight state changed!");
        }
    }
}
