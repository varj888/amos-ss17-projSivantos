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
        protected GPIOinterface gpioInterface;

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

        /// <summary>
        /// handles received Requests from the Frontend by deciding what to do in dependency of the request
        /// Note: At this point, only execution commands are content of the requests.
        /// </summary>
        /// <param name="request">the request information of the Frontend application</param>
        public void handleRequest(Request request)
        {


            if (request != null)
            {

                try
                {
                    Command command;

                    //look if the command was already requested once, if not, create it. 
                    if (!requestedCommands.TryGetValue(request.command, out command))
                    {
                        Debug.WriteLine("\n" + "Looking up requested Command in Assembly.....");
                        command = getANDinstantiateCommand(gpioInterface, request);
                        Debug.Write(string.Format("Found the following Command in Request: '{0}' and instantiated it \n", command != null ? command.GetType().FullName : "none"));
                    }
                    //then, execute command
                    command.execute(request.parameter);


                }
                catch (ArgumentNullException an)
                {
                    Debug.WriteLine("The requestet command was not found: " + an.Message);
                }
                catch (Exception e)
                {
                    Debug.Write("Something went wrong :( :" + e.Message);
                }

            }
        }

        /// <summary>
        ///  Creates dynamically an instance of the requested Command type and returns it
        ///  (it does not matter wich command are requested as long as they are existing) 
        /// </summary>
        /// <param name="gpioInterface"> interaction point to the Raspberry Pi's GpioPins</param>
        /// <param name="request">requested information of the Frontend application</param>
        /// <returns></returns>
        private Command getANDinstantiateCommand(GPIOinterface gpioInterface, Request request)
        {
            string command = "RaspberryBackend." + request.command;

            //typeof(ICommand).GetTypeInfo().Assembly:
            //-gets the current running Assembly where ICommand (and all other programm classes) can be found. 
            //-- typeof(type): gets the Type of ICommand => Type ICommand; now access to different methods e.g. (type)ICommand.*
            //-- GetTypeInfo(): gets Metainformation of the type e.g. Assembly information of ICommand
            //-- assembly utilize the Assembly information and returns the referenced assembly
            Assembly executingAssembly = typeof(ICommand).GetTypeInfo().Assembly;
            Type commandType = executingAssembly.GetType(command);

            return (Command)Activator.CreateInstance(commandType, gpioInterface);
        }

        /// <summary>
        /// can be used to save requested commands by adding them to a Dictonary datatype. 
        /// </summary>
        /// <param name="commandName">the name of the requested command</param>
        /// <param name="command">the Command object of the requested command</param>
        public void addRequestetCommand(String commandName, Command command)
        {
            requestedCommands.Add(commandName, command);
        }
    }
}
