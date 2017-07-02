using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to write on a spefic gpio pin of the Operation.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Execute the Command WritePin
        /// </summary>
        /// <param name="parameters">Represents the GpioPin:Uint16 which shall be written on</param>
        /// <returns>The current status of the requested pin.</returns>
        public string WritePin(UInt16 id)
        {
            GPIOinterface.activatePin(id);
            string retValue = GPIOinterface.readPin(id);
            return retValue;
        }
    }
}
