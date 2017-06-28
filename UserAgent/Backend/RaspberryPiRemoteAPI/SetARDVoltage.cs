using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to deactivate a tele-coil.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Executes the Command SetARDVoltage. For this the raspberry-class method setARDVoltage(device) is called.
        /// </summary>
        /// <param name="device">The identifier to choose a device.</param>
        /// <returns>The provided device.</returns>
        public string SetARDVoltage(string device)
        {
            this.setARDVoltage(device);
            this.updateLCD();
            return device;
        }
    }
}
