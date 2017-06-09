using System;
using System.Diagnostics;

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
        /// execute the Command SetAnalogVolume
        /// </summary>
        /// <param name="volumeLevel">represents the desired level of analog volume</param>
        public override void executeAsync(Object volumeLevel)
        {
            byte requestedVolumeLevel = (byte)volumeLevel;
            Debug.WriteLine(this.GetType().Name + "::: Requested Volume level: " + requestedVolumeLevel);
            if (requestedVolumeLevel < 0 || requestedVolumeLevel > 127)
            {
                throw new ArgumentOutOfRangeException("Volume can only operate in the intervall [0:127]");
            }

            dataBufferVariable[0] = requestedVolumeLevel;
            RaspberryPi.setAnalogVolume(dataBufferVariable);
        }
    }
}
