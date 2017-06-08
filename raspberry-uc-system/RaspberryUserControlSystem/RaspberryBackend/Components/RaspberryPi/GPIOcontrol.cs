using System;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// Set GPIO pin to 1
        /// </summary>
        /// <param name="id"></param>
        public void activatePin(UInt16 id)
        {
            GPIOinterface.setToOutput(id);
            GPIOinterface.writePin(id, 1);
        }

        /// <summary>
        /// Reset GPIO pin by settting to 0
        /// </summary>
        /// <param name="id"></param>
        public void deactivatePin(UInt16 id)
        {
            GPIOinterface.setToOutput(id);
            GPIOinterface.writePin(id, 0);
        }

        /// <summary>
        /// Read pin from GPIOInterface
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string readPin(UInt16 id)
        {
            return GPIOinterface.readPin(id);
        }
    }
}
