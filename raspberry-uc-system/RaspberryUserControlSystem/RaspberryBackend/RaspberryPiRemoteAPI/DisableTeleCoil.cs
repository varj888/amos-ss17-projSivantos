using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to deactivate a tele-coil.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// executes the Command DisableTeleCoil.
        /// </summary>
        public void DisableTeleCoil(int placeholderVariable)
        {
            this.unsetTeleCoil();
            Debug.Write("Unset telecoil");
        }
    }
}
