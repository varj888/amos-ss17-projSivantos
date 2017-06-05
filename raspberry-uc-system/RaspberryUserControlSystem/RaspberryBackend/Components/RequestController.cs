using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace RaspberryBackend
{  /// <summary>
   /// Controlls received Requests from the Frontend by e.g. saving all Request, executing or may reset them later.
   /// </summary>
    public class RequestController
    {
        private static readonly RequestController _instance = new RequestController();
        public RaspberryPi raspberryPi { get; set; }

        public static RequestController Instance
        {
            get
            {
                return _instance;
            }
        }

        private RequestController() { }

        /// <summary>
        /// handles received Requests from the Frontend by deciding what to do in dependency of the request. This method does everything automated!.
        /// Note: At this point, only execution commands are content of the requests.
        /// </summary>
        /// <param name="request">the request information of the Frontend application</param>
        /// <returns>
        ///  Returns a Result which is instanciated with exceptionMessage == null, if the command could be executed without exception
        ///  Returns a Result with exceptionMessage != null, if the command could not be executed without exception
        /// </returns>
        public Result handleRequest(Request request)
        {
            Command command = null;

            try
            {
                //look if the command was already requested once, if not, create it. 
                if (!Command.Instances.TryGetValue(request.command, out command))
                {
                    //look if the command was already requested once, if not, create it.
                    if (!Command.Instances.TryGetValue(request.command, out command))
                    {
                        Debug.WriteLine("\n" + "Looking up requested Command in Assembly.....");
                        command = createCommand(request);
                        Debug.Write(string.Format("Found the following Command in Request: '{0}' and instantiated it \n", command != null ? command.GetType().FullName : "none"));
                    }
                    else
                    {
                        Debug.WriteLine("Requested command is already instantiated and the instance will be taken from the Dictonary" + "\n");
                    }

                    //then, if gpioInterface is ready, execute command
                    if (raspberryPi.isInitialized())
                    {
                        command.executeAsync(request.parameter);
                    }
                    else
                    {
                        throw new Exception("raspberryPi must be initialized.");
                    }
                }
                catch (ArgumentNullException e)
                {
                    throw new ArgumentNullException("Requested command was not found: " + request.command);
                }
                else
                {
                    Debug.WriteLine("Requested command is already instantiated and the instance will be taken from the Dictonary" + "\n");
                }
            }
            catch (Exception e)
            {
                return new Result("Command could not be created");
            }

            try
            {
                command.executeAsync(request.parameter);
            }
            catch(Exception e)
            {
                return new Result(e.Message);
            }

            return new Result(null);      
                
        }

        /// <summary>
        ///  Creates dynamically an instance of the requested Command type and returns it
        ///  (it does not matter wich command are requested as long as they are existing)
        /// </summary>
        /// <param name="gpioInterface"> interaction point to the Raspberry Pi's GpioPins</param>
        /// <param name="request">requested information of the Frontend application</param>
        /// <returns> The requested Command Type</returns>
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

            return (Command)Activator.CreateInstance(commandType, raspberryPi);
        }
    }
}
