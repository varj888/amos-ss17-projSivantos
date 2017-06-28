using CommonFiles.TransferObjects;
using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to read a spefic gpio pin of the RaspberryPi.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Executes the Command ReadPin
        /// </summary>
        /// <param name="parameter">represents the GpioPin to read from</param>
        /// <returns>The current state of the requested pin represented as string.</returns>
        public string ReadPin(UInt16 id)
        {
            string currentState = readPin(id);
            return currentState.ToString();
        }
    }
}
