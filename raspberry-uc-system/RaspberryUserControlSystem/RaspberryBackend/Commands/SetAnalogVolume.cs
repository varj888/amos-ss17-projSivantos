using System;

namespace RaspberryBackend
{
    class SetAnalogVolume : Command
    {
        //0x03F Middle
        private byte[] dataBufferON = new byte[] { 127 }; //oder 0x07F
        private byte[] dataBufferOFF = new byte[] { 0 }; //oder 0x000
        private byte[] dataBufferVariable = new byte[] { 0 };

        public SetAnalogVolume(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// execute the Command TurnOnHI
        /// </summary>
        /// <param name="batteryDrainLevel">represents the capacity of a battery</param>
        public override void executeAsync(Object volumeLevel)
        {
            byte requestedVolumeLevel = (byte)volumeLevel;
            if (requestedVolumeLevel < 0 || requestedVolumeLevel >= 127)
            {
                throw new ArgumentOutOfRangeException("Volume can only operate in the intervall [0:127]");
            }

            dataBufferVariable[0] = requestedVolumeLevel;
            RaspberryPi.setHIPower(dataBufferVariable);
        }
    }
}
