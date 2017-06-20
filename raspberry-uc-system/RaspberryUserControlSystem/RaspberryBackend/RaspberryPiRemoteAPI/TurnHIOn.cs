using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It can be used turn on the HI on a desired voltage level.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Execute turnHI_on() on the RaspberryPi Object
        /// </summary>
        /// <param name="parameter">Represents the ADCVoltage to be set, will be clipped to min 0 and
        /// max 2.074 volts</param>
        /// <returns>The provided target voltage.</returns>
        public double TurnHIOn(double voltage)
        {
            turnHI_on(voltage);
            return voltage;
        }
    }
}
