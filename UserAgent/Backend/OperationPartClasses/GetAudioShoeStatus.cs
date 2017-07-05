namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It is a partial Operation class
    /// </summary>
    public partial class Operation
    {
        /// <summary>
        /// Return current status of audio-shoe. Note that readPin returns the last written value, which does not necessarily
        /// represent the real status of a pin.
        /// </summary>
        /// <returns></returns>
        public string getAudioShoeStatus()
        {
            return GPIOinterface.readPin(GpioMap.audioShoe_Pin);
        }
    }
}
