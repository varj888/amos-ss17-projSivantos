using System;

namespace RaspberryBackend
{
    public partial class Operation
    {

        /// <summary>
        /// Set the potentiometer to a value from 0000 0000 - 0111 1111
        /// </summary>
        /// <param name="parameters">represents the desired level of analog volume</param>
        /// <returns>The requested volume-level represented as string</returns>
        public string SetAnalogVolume(byte requestedVolumeLevel)
        {
            if (requestedVolumeLevel < 0 || requestedVolumeLevel > 127)
            {
                throw new Exception("Volume can only operate in the intervall [0:127]");
            }

            Potentiometer.write(requestedVolumeLevel);

            return requestedVolumeLevel.ToString();
        }
    }
}
