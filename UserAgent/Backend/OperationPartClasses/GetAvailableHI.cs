namespace RaspberryBackend
{
    public partial class Operation
    {
        /// <summary>
        /// Executes the command GetAvailableHI. This retrieves a list of available HI contained
        /// in the multiplexer-config. Currently this information is retrieved from MultiplexerConfigParser.
        /// </summary>
        /// <param name="y">Dummy</param>
        /// <returns>Dictionary of Lists of strings mapped to strings.</returns>
        public string GetAvailableHI(int y)
        {
            return HiXmlParser.getAvailableHI();
        }
    }
}
