using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to activate an audio-shoe.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Executes the Command EnableAudioShoe. For this the raspberry-class method set-/ unsetAudioShoe is used.
        /// </summary>
        /// <param name="value">Integer: For 1 activate audioshoe, for 0 deactivate it.</param>
        /// <returns>Current status of AudioShoe-Pin.</returns>
        public string EnableAudioShoe(int value)
        {
            if (value == 1)
            {
                this.setAudioShoe();
            }
            else if (value == 0)
            {
                this.unsetAudioShoe();
            }

            return readPin(audioShoe_Pin);
        }
    }
}
