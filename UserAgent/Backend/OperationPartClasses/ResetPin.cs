using System;

namespace RaspberryBackend
{

    /// <summary>
    /// This class represents a Command. It it can be used to reset a spefic gpio pin of the RaspberryPi.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Executes the Command ResetPin
        /// </summary>
        /// <param name="parameter">represents the GpioPin which shall be reset</param>
        /// <returns>The current state of the deactivated pin represented as string. Should evaluate to "Low".</returns>
        public string ResetPin(UInt16 id)
        {
            GPIOinterface.deactivatePin(id);
            return GPIOinterface.readPin(id).ToString();
        }
    }
}
