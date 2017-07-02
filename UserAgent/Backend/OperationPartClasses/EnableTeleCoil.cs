using static RaspberryBackend.GpioMap;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to deactivate a tele-coil.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Send signal that a tele-coil is detected or undetected by pulling up the respective pin as defined by documentation. Compare the
        /// circuit diagram for more details.
        /// </summary>
        /// <param name="value">Integer: For 1 activate the telecoil. For 0 deactivate it.</param>
        /// <returns>The current status of the teleCoil-pin.</returns>
        public string EnableTeleCoil(int value)
        {
            if (value == 1)
            {
                GPIOinterface.activatePin(teleCoil_Pin);


            }
            else if (value == 0)
            {
                GPIOinterface.deactivatePin(teleCoil_Pin);

            }

            return GPIOinterface.readPin(teleCoil_Pin);

        }
    }
}
