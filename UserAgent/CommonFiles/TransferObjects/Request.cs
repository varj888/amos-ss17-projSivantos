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
    /// Objects of this type will be send from the testmachine to the raspberry pi to control it.
    /// Parameters have to be known types.
    /// </summary>
    [DataContract]
    [KnownType(typeof(int[]))]
    [KnownType(typeof(string[]))]
    public class Request
    {
        /// <summary>
        /// Constructor for a request.
        /// </summary>
        /// <param name="command">The command this request carries.</param>
        /// <param name="parameter">The respective parameters associated with the command.</param>
        public Request(string command, object parameter)
        {
            this.command = command;
            parameters = new object[] { parameter };
        }

        /// <summary>
        /// Alternative constructor for a request.
        /// </summary>
        /// <param name="command">The command this request carries.</param>
        /// <param name="parameters">A list of parameters associated with this command.</param>
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
        /// Handles a request by invoking the appropriate method of callee.
        /// </summary>
        /// <param name="callee">Object, which method will be called</param>
        /// <param name="request">This parameter is used by handleRequest to determine the called method and their parameters.</param>
        /// <returns>Returns the result of the called Method</returns>
        public static Object handleRequest(Object callee, Request request)
        {
            MethodInfo m;

            // Searching the method
            try
            {
                m = callee.GetType().GetMethod(request.command);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e.Message);
            }

            if (m == null)
            {
                return new ExceptionResult("Command not found");
            }

            // calling the method
            try
            {
                object value = m.Invoke(callee, request.parameters);
                return new SuccessResult(value);
            }
            catch (TargetInvocationException e)
            {
                return new ExceptionResult(e.GetBaseException().Message);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e.Message);
            }
        }
    }
}