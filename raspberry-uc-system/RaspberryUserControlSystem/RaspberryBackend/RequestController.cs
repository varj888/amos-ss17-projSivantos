using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{  /// <summary>
   /// Controlls received Requests from the Frontend by e.g. saving all Request, executing or reset them. 
   /// </summary>
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

        /// <summary>
        /// handles received Requests from the Frontend by executing them. 
        /// </summary>
        public void handleRequest(Request request)
        {
            Debug.WriteLine(request);
            if (request != null)
            {

                try
                {
                    Command command = getANDinstanciateCommand(gpioInterface, request);

                    Debug.Write("Found the following Command in Request: " + command != null ? command.GetType().FullName : "none");

                    command.execute(request.parameter);
                }
                catch (ArgumentNullException an)
                {
                    Debug.WriteLine("The requestet command was not found:" + an.Message);
                }
                catch (Exception e)
                {
                    Debug.Write("Something went wrong :( :" + e.Message);

                }

            }
        }

        /// <summary>
        /// Creates dynamically an instance of the requested Command type and returns it
        /// </summary>
        private Command getANDinstanciateCommand(GPIOinterface gpioInterface, Request request)
        {
            string command = "RaspberryBackend." + request.command;

            Assembly executingAssembly = typeof(LightLED).GetTypeInfo().Assembly;
            Type commandType = executingAssembly.GetType(command);

            return (Command)Activator.CreateInstance(commandType, gpioInterface);
        }
    }
}
