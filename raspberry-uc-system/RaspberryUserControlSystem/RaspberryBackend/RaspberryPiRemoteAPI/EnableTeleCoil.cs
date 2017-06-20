using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to deactivate a tele-coil.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Executes the Command EnableTeleCoil.
        /// </summary>
        /// <param name="value">Integer: For 1 activate the telecoil. For 0 deactivate it.</param>
        /// <returns>The current status of the teleCoil-pin.</returns>
        public string EnableTeleCoil(int value)
        {
            if (value == 1)
            {
                this.setTeleCoil();

            }
            else if (value == 0)
            {
                this.unsetTeleCoil();
            }

            return readPin(teleCoil_Pin);

        }
    }
}
