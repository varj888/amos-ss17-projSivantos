using static RaspberryBackend.GpioMap;
namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to activate an audio-shoe.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Executes the Command EnableAudioShoe. Send signal that an audio-shoe is connected /disconnected
        /// by pulling up /down the respective pin as defined by documentation.
        /// Compare the circuit diagram for more details.
        /// </summary>
        /// <param name="value">Integer: For 1 activate audioshoe, for 0 deactivate it.</param>
        /// <returns>Current status of AudioShoe-Pin.</returns>
        public string EnableAudioShoe(int value)
        {
            if (value == 1)
            {
                GPIOinterface.activatePin(audioShoe_Pin);
            }
            else if (value == 0)
            {
                GPIOinterface.deactivatePin(audioShoe_Pin);

            }

            return GPIOinterface.readPin(audioShoe_Pin);
        }
    }
}
