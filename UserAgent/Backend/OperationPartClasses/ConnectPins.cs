namespace RaspberryBackend
{
    public partial class Operation
    {
        /// <summary>
        /// Connect pins x to y on the multiplexer. Right now this is the same as _multiplexer.connectPins except no checks
        /// are performed on the input parameters. Eventually we can check for success right here.
        /// </summary>
        /// <param name="x">X-Side pin (output)</param>
        /// <param name="y">Y-Side pin (input)</param>
        /// <returns>String representing which pin to which was connected.</returns>
        public string ConnectPins(int x, int y)
        {
            Multiplexer.connectPins(x, y);
            return x + " to " + y;
        }
    }
}
