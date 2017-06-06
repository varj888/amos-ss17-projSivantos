using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It connects two requested Pins (xPin,yPin) on the Multiplexer.
    /// </summary>
    class ConnectPins : Command
    {
        public ConnectPins(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// Connect pin x to y, provided by pinString
        /// </summary>
        /// <param name="pinString">Input pins in format: "xPin"+"%"+"yPin"</param>
        public override void executeAsync(Object pinString)
        {
            string requestInput = ((string)pinString);
            string[] pins = requestInput.Split('%');
            Int32 xPin = Int32.Parse(pins[0]);
            Int32 yPin = Int32.Parse(pins[1]);

            if (pins.Length != 2)
            {
                throw new ArgumentException("Please provide exactly 2 Pins which shall be connected");
            }

            RaspberryPi.connectPins(xPin, yPin);
        }
    }
}
