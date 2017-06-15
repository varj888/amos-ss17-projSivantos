using System;
using System.Diagnostics;
using Windows.Devices.Gpio;

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
        public void ReadPin(UInt16 id)
        {
            string currentState = readPin(id);
            Debug.WriteLine(string.Format("Pin {0} has currently the state: {1}", id, currentState));
        }
    }
}
