using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    class RequestController
    {
        private static readonly RequestController _instance = new RequestController();
        protected static GPIOinterface gpioInterface;

        private static object syncLock = new object();
        

        private RequestController()
        {
            gpioInterface = new GPIOinterface();
        }

        public static RequestController Instance
        {
            get
            {
                return _instance;
            }
        }


        public void handleRequest(Request r)
        {
            if (r != null)
            {
                UInt16 id = 0;
                if(r.parameter.GetType() == typeof(int))
                {
                    id = (UInt16) r.parameter;
                } else
                {
                    return;
                }

                if( r.command == "read"  )
                {
                    gpioInterface.setToInput(id);
                    gpioInterface.readPin(id);
                } else if(r.command == "write")
                {
                    gpioInterface.setToOutput(id);
                    gpioInterface.writePin(id, 1);
                } else if( r.command == "reset" )
                {
                    gpioInterface.setToOutput(id);
                    gpioInterface.writePin(id, 0);
                }


                //Debug.WriteLine("Got Request, setting pins.");
                //gpioInterface.setToInput(5);
                //gpioInterface.setToInput(6);
                //gpioInterface.writePin(5, 1);
                //gpioInterface.writePin(6, 1);
                
                //string command = "RaspberryBackend." + r.command;


                //try
                //{
                //    // Create dynamically an instance of the requested Command type 
                //    Assembly executingAssembly = typeof(LightLED).GetTypeInfo().Assembly;
                //    Type commandType = executingAssembly.GetType(command);
                //    Command com = (Command)Activator.CreateInstance(commandType);

                //    // execute the requested command
                //    Debug.Write("Found the following type in Request: ");
                //    Debug.WriteLine(com != null ? com.GetType().FullName : "none");
                //    com.execute(r.parameter);
                //}
                //catch (ArgumentNullException an)
                //{
                //    Debug.WriteLine("The requestet command was not found:" + an.Message);
                //}
                //catch (Exception)
                //{
                //    Debug.WriteLine("Something went wrong :( ");
                //}

            }
        }
    }

    public interface ICommand
    {
        void execute(Object parameter);

        void undo();
    }

    public abstract class Command : ICommand
    {
        public GPIOinterface gpio;
        public Command(GPIOinterface gpioInterface)
        {
            gpio = gpioInterface;
        }

        public abstract void execute(object parameter);
        public abstract void undo();
    }


    class LightLED : Command
    {
        public LightLED(GPIOinterface gpioInterface) : base(gpioInterface) {    }

        //Suggestion: A instance variable which helds the last known state in order to revert it
        //private static Object lastState

        public override void execute(Object parameter)
        {
            string par = parameter.ToString();


            if (par.Equals("1"))
            {
                //Execute appropiate method in GPIOinterface like e.g. gpio.led(1)
                gpio.setToOutput(5);
                gpio.setToInput(6);
                gpio.writePin(5, 1);
                Debug.WriteLine("LED switched On");
            }

            else if (par.Equals("0"))
            {
                //gpio.writePin(6, 0);
                //Execute appropiate method in GPIOinterface like e.g. gpio.led(0);
                Debug.WriteLine("LED switched Off");
            }

            else
            {
                Debug.WriteLine("no valid parameter");
            }
        }

        public override void undo()
        {
            throw new NotImplementedException();
        }
    }
}
