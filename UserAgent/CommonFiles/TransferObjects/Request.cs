using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace CommonFiles.TransferObjects
{
    /// <summary>
    /// Objects of this type will be send from the Testmachine to the Raspberry pi to control it
    /// parameters have to be known Types
    /// </summary>
    [DataContract]
    [KnownType(typeof(int[]))]
    [KnownType(typeof(string[]))]
    public class Request
    {
        public Request(string command, object parameter)
        {
            this.command = command;
            parameters = new object[] { parameter };
        }

        public Request(string command, Object[] parameters)
        {
            this.command = command;
            this.parameters = parameters;
        }

        [DataMember]
        public string command;

        [DataMember]
        public Object[] parameters;

        /// <summary>
        /// Handles a Request by invoking the appropriate Method of callee
        /// </summary>
        /// <param name="callee">Objekt, which method will be called</param>
        /// <param name="request">This parameter is used by handleRequest to determine the called method and their parameters</param>
        /// <returns>Returns the result of the called Method</returns>
        public static Result handleRequest(Object callee, Request request)
        {
            MethodInfo m;

            // Searching the method
            try
            {
                m = callee.GetType().GetMethod(request.command);
            }
            catch (Exception e)
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
                object value = m.Invoke(callee, request.parameters);
                return new Result(true, request.command, value);
            }
            catch (TargetInvocationException e)
            {
                return new Result(e.GetBaseException().Message);
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }
    }
}