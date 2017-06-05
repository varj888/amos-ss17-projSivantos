using System;
using RaspberryBackend.Data;

namespace RaspberryBackend
{
    class ConnectPins : Command
    {
        public ConnectPins(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// Connect pin x to y, taken from pinarray[0] = x and pinarray[1] = y
        /// </summary>
        public override void executeAsync(Object pinarray)
        {
            int[] pins = (int[]) pinarray;
            if (pins.Length != 2)
            {
                return;
            }
            RaspberryPi.connectPins(pins[0], pins[1]);
        }
    }
}
