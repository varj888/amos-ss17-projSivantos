using System.Diagnostics;

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
        public void EnableAudioShoe(int placeholderVariable)
        {
            this.setAudioShoe();
            Debug.Write("Set audio-shoe");
        }
    }
}
