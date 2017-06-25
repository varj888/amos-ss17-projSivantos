using CommonFiles.TransferObjects;
using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It is a partial RaspberryPi class
    /// </summary>
    public partial class RaspberryPi
    {
        /// <summary>
        /// Execute the command SetHI
        /// </summary>
        /// <param name="param">List of family and model in this order.</param>
        /// <returns>The model when successfully updated config.</returns>
        public string SetHI(string family, string model)
        {
            Multiplexer.setMultiplexerConfiguration(family, model);
            this.updateLCD();
            return model;
        }
    }
}
