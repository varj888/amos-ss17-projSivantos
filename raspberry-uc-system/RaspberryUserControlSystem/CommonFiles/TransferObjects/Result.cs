using System;
using System.Runtime.Serialization;

namespace CommonFiles.TransferObjects
{
    /// <summary>
    /// Objects of this type will be received by the Testmachines as Responses to Requests
    /// exception is null if the request was executed sucessful
    /// it will be initialised with an exception if the request was not executed sucessful
    /// </summary>
    [DataContract]
    public class Result
    {
        public Result(Exception e)
        {
            this.exception = e;
        }

        [DataMember]
        public Exception exception;
    }
}