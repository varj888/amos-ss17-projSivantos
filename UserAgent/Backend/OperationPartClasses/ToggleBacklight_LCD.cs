using CommonFiles.TransferObjects;

namespace RaspberryBackend
{

    /// <summary>
    /// This class represents a Command. It it can be used to toggle the Backlight of a I2C connected LCD on the Operation.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Toogles the Backlight of the LCD to onn or off.
        /// </summary>
        /// <param name="parameter">1 for Backlight on or 0 for off</param>
        /// <returns>The provided parameter represented as a string.</returns>
        public string ToggleBacklight_LCD(int requestedParameter)
        {
            const byte ON = 0x01;
            const byte OFF = 0x00;

            if (requestedParameter.Equals(1))
            {
                switchToState(ON);
            }
            else if (requestedParameter.Equals(0))
            {
                switchToState(OFF);
            }

            return requestedParameter.ToString();
        }

        private void switchToState(byte targetState)
        {
            setLCDBackgroundState(targetState);
        }
    }
}
