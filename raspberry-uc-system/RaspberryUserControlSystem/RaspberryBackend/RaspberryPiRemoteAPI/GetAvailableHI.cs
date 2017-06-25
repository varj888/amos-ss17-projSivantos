using CommonFiles.TransferObjects;
using System.Collections.Generic;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// Executes the command GetAvailableHI. This retrieves a list of available HI contained
        /// in the multiplexer-config. Currently this information is retrieved from MultiplexerConfigParser.
        /// </summary>
        /// <param name="y">Dummy</param>
        /// <returns>Dictionary of Lists of strings mapped to strings.</returns>
        public Dictionary<string, List<string>> GetAvailableHI(int y)
        {
            return MultiplexerConfigParser.getAvailableHI();
        }
    }
}
