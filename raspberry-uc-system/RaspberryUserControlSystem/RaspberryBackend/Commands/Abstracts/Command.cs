using System;
using System.Collections.Generic;

namespace RaspberryBackend
{
    /// <summary>
    /// This class is a abstract for basic commands to use actors or sensors on the HI (EvalBoard)
    /// </summary>
    public abstract class Command : ICommand
    {
        private static Dictionary<String, Command> instances = new Dictionary<String, Command>();
        public RaspberryPi RaspberryPi;
        protected UInt16 pushButton_Pin = 26;
        protected UInt16 rockerSwitch_Pin_0 = 20;
        protected UInt16 rockerSwitch_Pin_1 = 21;

        public static Dictionary<String, Command> Instances
        {
            get { return instances; }
        }

        public Command(RaspberryPi raspberryPi)
        {
            RaspberryPi = raspberryPi;
            instances.Add(this.GetType().Name, this);
        }


        /// <summary>
        /// executes the command in dependencie of the parsed parameter
        /// </summary>
        /// <param name="parameter">A Object parameter in order to determine further attributes of the execution</param>
        public abstract void executeAsync(object parameter);


    }
}
