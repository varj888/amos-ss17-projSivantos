namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It can be used turn on the HI on a desired voltage level.
    /// </summary>
    class TurnHIOn : Command
    {
        public TurnHIOn(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        /// parses the parameter as double voltage and executes turnHI_on() on the RaspberryPi Object
        /// </summary>
        /// <param name="parameter">represents the ADCVoltage to be set, will be clipped to min 0 and max 2.074 volts</param>
        public override void executeAsync(object[] parameters)
        {
            object parameter = parameters[0];
            double voltage = (double)parameter;
            RaspberryPi.turnHI_on(voltage);
        }
    }
}
