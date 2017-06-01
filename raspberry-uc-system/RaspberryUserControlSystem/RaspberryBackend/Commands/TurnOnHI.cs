using System;

namespace RaspberryBackend
{
    class TurnOnHI : Command
    {
        //0x03F Middle
        private byte[] dataBufferON = new byte[] { 127 }; //oder 0x07F
        private byte[] dataBufferOFF = new byte[] { 0 }; //oder 0x000
        private byte[] dataBufferVariable = new byte[] { 0 };

        public TurnOnHI(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// execute the Command TurnOnHI
        /// </summary>
        /// <param name="batteryDrainLevel">represents the capacity of a battery</param>
        public override void executeAsync(Object batteryDrainLevel)
        {
            string requestedParameter = batteryDrainLevel.ToString();
            if (requestedParameter.Equals("127"))
            {
                //RaspberryPi.Potentiometer._potentiometer.Read();
                RaspberryPi.setHIPower(dataBufferON);
            }
            else if (requestedParameter.Equals("0"))
            {
                RaspberryPi.setHIPower(dataBufferOFF);
            }
            else
            {
                dataBufferVariable[0] = (byte)batteryDrainLevel;
                RaspberryPi.setHIPower(dataBufferVariable);
            }
        }
    }
}
