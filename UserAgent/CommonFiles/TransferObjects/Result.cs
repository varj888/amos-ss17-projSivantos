using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommonFiles.TransferObjects
{
    /// <summary>
    /// Objects of this type will be received by the Testmachines as Responses to sucessfully executed Requests
    /// </summary>
    [DataContract]
    [KnownType(typeof(Dictionary<string, string>))]
    public class SuccessResult
    {
        /// <summary>
        /// Result of a Request
        /// </summary>
        [DataMember]
        public object result;

        public SuccessResult(Object result)
        {
            this.result = result;
        }
    }

    //Todo: Delete this class later. It is just for merge
    public class Result
    {

    }

    [DataContract]
    public class ExceptionResult
    {
        [DataMember]
        public string exceptionMessage;

        public ExceptionResult(string exceptionMessage)
        {
            this.exceptionMessage = exceptionMessage;
        }
    }

    /// <summary>
    /// Exeption, which is thrown as result of a ExceptionResult
    /// </summary>
    public class RequestExecutionException : Exception
    {
        public RequestExecutionException()
        {
        }

        public RequestExecutionException(string message)
            : base(message)
        {
        }

        public RequestExecutionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}