namespace RaspberryBackend
{

    /// <summary>
    /// This class represents a Command. It it can be used to reset a spefic gpio pin of the Operation.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Executes the Command ResetPin
        /// </summary>
        /// <param name="parameter">represents the GpioPin which shall be reset</param>
        /// <returns>The current state of the deactivated pin represented as string. Should evaluate to "Low".</returns>
        public string ResetMux(int a)
        {
            string family = "TestFamily";
            string model = "TestModel";
            Multiplexer.resetAll();
            setMultiplexerConfiguration(family, model);
            return "";
        }
    }
}
