namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It can be used turn on the HI on a desired voltage level.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Sets the DACVoltage output in channel 1 to a desired voltage
        /// </summary>
        /// <param name="parameter">Represents the ADCVoltage to be set, will be clipped to min 0 and
        /// max 2.074 volts</param>
        /// <returns>The provided target voltage.</returns>
        public double TurnHIOn(double voltage)
        {
            ADConverter.setDACVoltage1(voltage);
            this.updateLCD();
            return voltage;
        }
    }
}
