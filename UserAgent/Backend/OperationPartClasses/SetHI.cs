namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It is a partial Operation class
    /// </summary>
    public partial class Operation
    {
        /// <summary>
        /// Execute the command SetHI and updates the LCD <see cref="updateLCD"/>
        /// </summary>
        /// <param name="param">List of HiFamily and HiModel in this order.</param>
        /// <returns>The HiModel when successfully updated config.</returns>
        public string SetHI(string family, string model)
        {
            setMultiplexerConfiguration(family, model);
            this.updateLCD();
            return model;
        }
    }
}
