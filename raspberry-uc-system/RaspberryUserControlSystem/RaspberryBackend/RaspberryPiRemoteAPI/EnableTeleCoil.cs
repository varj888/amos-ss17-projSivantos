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
        public void EnableTeleCoil(int value)
        {
            if (value == 1)
            {
                this.setTeleCoil();

            }
            else if (value == 0)
            {
                this.unsetTeleCoil();
            }
            Debug.Write("Set telecoil");
        }
    }
}
