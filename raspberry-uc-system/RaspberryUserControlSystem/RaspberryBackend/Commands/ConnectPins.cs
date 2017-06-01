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
        /// Connect pin x to y
        /// </summary>
        public override void executeAsync(Object pinarray)
        {
            RaspberryPi.connectPins(5, 5);
        }
    }
}
