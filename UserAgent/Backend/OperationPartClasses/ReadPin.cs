using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to read a spefic gpio pin of the Operation.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Reads pin from GPIOInterface
        /// </summary>
        /// <param name="parameter">represents the GpioPin to read from</param>
        /// <returns>The current state of the requested pin represented as string.</returns>
        public string ReadPin(UInt16 id)
        {
            string currentState = GPIOinterface.readPin(id);
            return currentState.ToString();
        }
    }
}
