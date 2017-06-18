using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to deactivate an audio-shoe.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// executes the Command EnableAudioShoe.
        /// </summary>
        public void DisableAudioShoe()
        {
            this.unsetAudioShoe();
            Debug.Write("Unset audio-shoe");
        }
    }
}
