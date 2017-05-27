namespace RaspberryBackend
{
    /// <summary>
    /// This class is a abstract for basic commands to use actors or sensors on the HI (EvalBoard)
    /// </summary>
    public abstract class Command : ICommand
    {
        public RaspberryPi RaspberryPi;

        public Command(RaspberryPi raspberryPi)
        {
            RaspberryPi = raspberryPi;
        }


        /// <summary>
        /// executes the command in dependencie of the parsed parameter
        /// </summary>
        /// <param name="parameter">A Object parameter in order to determine further attributes of the execution</param>
        public abstract void execute(object parameter);
    }
}
