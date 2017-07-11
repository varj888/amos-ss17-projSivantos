using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommonFiles.TransferObjects
{
    /// <summary>
    /// Objects of this type will be received by the Testmachines as Responses to Requests
    /// exceptionMessage is null if the request was executed sucessful
    /// it will be initialised with an exception if the request was not executed sucessful
    /// </summary>
    [DataContract]
    [KnownType(typeof(Dictionary<string, string>))]
    public class Result
    {
        private Result(bool success, string obj, Object value, String exceptionMessage)
        {
            this.exceptionMessage = exceptionMessage;
            this.success = success;
            this.obj = obj;
            this.value = value;
        }

        public Result(bool success, string obj, Object value) : this(success, obj, value, null)
        {
        }

        public Result(String exceptionMessage) : this(false, null, null, exceptionMessage)
        {
        }

        [DataMember]
        public bool success;
        [DataMember]
        public string exceptionMessage;
        [DataMember]
        public string obj;
        [DataMember]
        public Object value;
    }
}