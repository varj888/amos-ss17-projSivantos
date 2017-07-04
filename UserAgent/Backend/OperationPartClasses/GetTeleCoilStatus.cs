namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It is a partial Operation class
    /// </summary>
    public partial class Operation
    {
        /// <summary>
        /// Return current status of tele-coil. Note that readPin returns the last written value, which does not neccessarily
        /// represent the real status of a ping.
        /// </summary>
        /// <returns></returns>
        public string getTeleCoilStatus()
        {
            return GPIOinterface.readPin(GpioMap.audioShoe_Pin);
        }
    }
}
