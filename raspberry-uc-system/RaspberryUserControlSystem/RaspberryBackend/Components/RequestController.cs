using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace RaspberryBackend
{  /// <summary>
   /// Controlls received Requests from the Frontend by e.g. saving all Request, executing or may reset them later. 
   /// </summary>
    class RequestController
    {
        private static Dictionary<String, Command> requestedCommands = new Dictionary<String, Command>();
        private static readonly RequestController _instance = new RequestController();
        private GPIOinterface gpioInterface;

        public static RequestController Instance
        {
            get
            {
                return _instance;
            }
        }

        public GPIOinterface GpioInterface {
            set => gpioInterface = value;
        }

        private RequestController(){}

        /// <summary>
        /// handles received Requests from the Frontend by deciding what to do in dependency of the request
        /// Note: At this point, only execution commands are content of the requests.
        /// </summary>
        /// <param name="request">the request information of the Frontend application</param>
        public Command handleRequest(Request request)
        {

            Command command = null;

            if (request != null)
            {

                try
                {

                    //look if the command was already requested once, if not, create it. 
                    if (!requestedCommands.TryGetValue(request.command, out command))
                    {
                        Debug.WriteLine("\n" + "Looking up requested Command in Assembly.....");
                        command = createCommand(request);
                        Debug.Write(string.Format("Found the following Command in Request: '{0}' and instantiated it \n", command != null ? command.GetType().FullName : "none"));
                    }
                    //then, if gpioInterface is ready, execute command
                    if (gpioInterface.Initialized)
                    {
                        command.execute(request.parameter);
                    }
                    else
                    {
                        throw new Exception("gpioInterface must be initialized.");
                    }
                   
                }
                catch (ArgumentNullException e)
                {
                    throw new ArgumentNullException("The requested command was not found: " + request.command);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                
            }

            return command;
        }

        /// <summary>
        ///  Creates dynamically an instance of the requested Command type and returns it
        ///  (it does not matter wich command are requested as long as they are existing) 
        /// </summary>
        /// <param name="gpioInterface"> interaction point to the Raspberry Pi's GpioPins</param>
        /// <param name="request">requested information of the Frontend application</param>
        /// <returns></returns>
        private Command createCommand(Request request)
        {
            string command = "RaspberryBackend." + request.command;

            //typeof(ICommand).GetTypeInfo().Assembly:
            //-gets the current running Assembly where ICommand (and all other programm classes) can be found. 
            //-- typeof(type): gets the Type of ICommand => Type ICommand; now access to different methods e.g. (type)ICommand.*
            //-- GetTypeInfo(): gets Metainformation of the type e.g. Assembly information of ICommand
            //-- assembly utilize the Assembly information and returns the referenced assembly
            Assembly executingAssembly = typeof(ICommand).GetTypeInfo().Assembly;
            Type commandType = executingAssembly.GetType(command);


            return (Command) Activator.CreateInstance(commandType, gpioInterface);
        }

        /// <summary>
        /// can be used to save requested commands by adding them to a Dictonary datatype. 
        /// </summary>
        /// <param name="commandName">the name of the requested command</param>
        /// <param name="command">the Command object of the requested command</param>
        public void addRequestedCommand(String commandName, Command command)
        {
            requestedCommands.Add(commandName, command);
        }
    }
}
