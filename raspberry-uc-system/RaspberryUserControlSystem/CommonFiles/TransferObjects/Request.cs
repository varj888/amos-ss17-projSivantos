using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CommonFiles.TransferObjects{
    /// <summary>
    /// Unit of transfer by the RequestConnClient Class
    /// is only as a container for the two variables methodName and parameter
    /// </summary>
    [DataContract]
    public class Request
    {
        public Request(string command, Object parameter)
        {
            this.command = command;
            this.parameter = parameter;
        }

        [DataMember]
        public string command;

        [DataMember]
        public Object parameter;
    }
}