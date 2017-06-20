using CommonFiles.TransferObjects;
using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to write on a spefic gpio pin of the RaspberryPi.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Execute the Command WritePin
        /// </summary>
        /// <param name="parameters">Represents the GpioPin:Uint16 which shall be written on</param>
        /// <returns>The current status of the requested pin.</returns>
        public string WritePin(UInt16 id)
        {
            activatePin(id);
            string retValue = readPin(id);
            return retValue;
        }
    }
}
