using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// Executes the command ConnectPins. For this we call the raspberry-class method connectPins.
        /// </summary>
        /// <param name="x">X-Side pin (output)</param>
        /// <param name="y">Y-Side pin (input)</param>
        /// <returns>String representing which pin to which was connected.</returns>
        public string ConnectPins(int x, int y)
        {
            connectPins(x, y);
            return x + " to " + y;
        }
    }
}
