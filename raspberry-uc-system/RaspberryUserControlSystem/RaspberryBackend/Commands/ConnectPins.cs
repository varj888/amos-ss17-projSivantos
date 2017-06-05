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
            string s = ((string)pinarray);
            string[] xy = s.Split('%');
            

            if (xy.Length != 2)
            {
                return;
            }


            RaspberryPi.connectPins(Int32.Parse(xy[0]), Int32.Parse(xy[1]));
        }
    }
}
