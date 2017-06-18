﻿using CommonFiles.TransferObjects;
using System;

namespace RaspberryBackend
{

    /// <summary>
    /// This class represents a Command. It it can be used to reset a spefic gpio pin of the RaspberryPi.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// executes the Command ResetPin
        /// </summary>
        /// <param name="parameter">represents the GpioPin which shall be reset</param>
        public Result ResetPin(UInt16 id)
        {
            deactivatePin(id);
            return new Result(true, this.GetType().Name, readPin(id).ToString());
        }
    }
}
