using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to activate an audio-shoe.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// executes the Command EnableAudioShoe.
        /// </summary>
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
