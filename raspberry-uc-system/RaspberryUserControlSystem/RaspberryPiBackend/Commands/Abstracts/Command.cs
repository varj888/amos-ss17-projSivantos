namespace RaspberryPiBackend
{
    /// <summary>
    /// This class is a abstract for basic commands to use actors or sensors on the HI (EvalBoard)
    /// </summary>
    public abstract class Command : ICommand
    {
        public GPIOinterface _gpioInterface;
        public Command(GPIOinterface gpioInterface)
        {
            _gpioInterface = gpioInterface;
        }

        public abstract void execute(object parameter);
        public abstract void undo();
    }
}
