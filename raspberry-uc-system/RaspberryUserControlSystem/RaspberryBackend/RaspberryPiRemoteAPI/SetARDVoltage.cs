using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to deactivate a tele-coil.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// executes the Command EnableTeleCoil.
        /// </summary>
        public void SetARDVoltage(string device)
        {
            this.setARDVoltage(device);
            Debug.WriteLine("Set ARD voltage");
        }
    }
}
