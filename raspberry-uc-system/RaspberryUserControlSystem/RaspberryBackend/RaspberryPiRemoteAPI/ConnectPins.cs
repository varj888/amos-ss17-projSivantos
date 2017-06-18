using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        public Result ConnectPins(int x, int y)
        {
            connectPins(x, y);
            return new Result(true, this.GetType().Name, x + "a" + y);
        }
    }
}
