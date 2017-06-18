using CommonFiles.TransferObjects;

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
        public Result SetARDVoltage(string device)
        {
            this.setARDVoltage(device);
            return new Result(true, this.GetType().Name, device);
        }
    }
}
