using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {


        /// <summary>
        /// execute the Command SetAnalogVolume
        /// </summary>
        /// <param name="parameters">represents the desired level of analog volume</param>
        public Result SetAnalogVolume(byte requestedVolumeLevel)
        {
            //0x03F Middle
            byte[] dataBufferON = new byte[] { 127 }; //oder 0x07F
            byte[] dataBufferOFF = new byte[] { 0 }; //oder 0x000
            byte[] dataBufferVariable = new byte[] { 0 };

            if (requestedVolumeLevel < 0 || requestedVolumeLevel > 127)
            {
                return new Result(true, this.GetType().Name, "Volume can only operate in the intervall [0:127]");
            }

            dataBufferVariable[0] = requestedVolumeLevel;
            setAnalogVolume(dataBufferVariable);

            return new Result(true, this.GetType().Name, requestedVolumeLevel.ToString());
        }
    }
}
