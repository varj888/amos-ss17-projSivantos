using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RegistryServer
{

    /// <summary>
    /// Allows to call a Method of a Service by Requests
    /// </summary>
    public class RequestController
    {
        private RegistryService service;

        /// <summary>
        /// Creates a RequestController to a Service
        /// </summary>
        /// <param name="service">Service, which methods will be called</param>
        public RequestController(RegistryService service) {
            this.service = service;
        }

        /// <summary>
        /// handling the Request by searching the method request.command and calling it
        /// the argument of the called Method is request.parameter
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result handleRequest(Request request)
        {
            MethodInfo m;

            // Searching the method
            try
            {
                m = typeof(RegistryService).GetMethod(request.command);        
            }catch(Exception e)
            {
                return new Result(e.Message);
            }

            if (m == null)
            {
                return new Result("Command not found");
            }

            // calling the method
            try
            {
                m.Invoke(service, new Object[] { request.parameter });
            }catch(Exception e)
            {
                return new Result(e.Message);
            }

            return new Result("success");
        }
    }
}
