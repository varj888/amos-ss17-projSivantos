using CommonFiles.TransferObjects;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        public string ConnectPins(int x, int y)
        {
            connectPins(x, y);
            return x + "a" + y;
        }
    }
}
