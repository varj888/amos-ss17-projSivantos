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
        /// executes the Command ReadPin
        /// </summary>
        /// <param name="parameter">represents the GpioPin to read from</param>
        public Result ReadPin(UInt16 id)
        {
            string currentState = readPin(id);
            return new Result(true, this.GetType().Name, currentState.ToString());
        }
    }
}
