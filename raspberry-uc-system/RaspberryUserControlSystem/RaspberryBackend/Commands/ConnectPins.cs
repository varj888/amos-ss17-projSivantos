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
        /// Connect pin x to y, provided by an integer Array from Frontend
        /// </summary>
        /// <param name="parameter">pins as int array</param>
        public override void executeAsync(Object parameter)
        {
            int[] pins = (int[])parameter;
            Int32 xPin = pins[0];
            Int32 yPin = pins[1];

            if (pins.Length != 2)
            {
                throw new ArgumentException("Please provide exactly 2 Pins which shall be connected");
            }

            RaspberryPi.connectPins(xPin, yPin);
        }
    }
}
