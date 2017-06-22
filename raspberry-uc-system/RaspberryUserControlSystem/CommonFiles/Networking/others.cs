using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;

namespace CommonFiles.Networking
{
    public class Others
    {

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
                return new Result(true, callee.GetType().Name, value);
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