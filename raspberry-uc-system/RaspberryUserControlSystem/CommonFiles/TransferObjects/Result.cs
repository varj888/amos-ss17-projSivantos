using System;
using System.Runtime.Serialization;

namespace CommonFiles.TransferObjects
{
    /// <summary>
    /// Objects of this type will be received by the Testmachines as Responses to Requests
    /// exceptionMessage is null if the request was executed sucessful
    /// it will be initialised with an exception if the request was not executed sucessful
    /// </summary>
    [DataContract]
    public class Result
    {
        private Result(bool success, string obj, Object value, String exceptionMessage)
        {
            this.exceptionMessage = exceptionMessage;
            this.success = success;
            this.obj = obj;
            this.value = value;
        }

        public Result(bool success, string obj, Object value)
        {
            new Result(success, obj, value, null);
        }

        public Result(String exceptionMessage)
        {
            new Result(false, null, null, exceptionMessage);
        }

        [DataMember]
        public bool success;
        public string exceptionMessage;
        public string obj;
        public Object value;
    }
}