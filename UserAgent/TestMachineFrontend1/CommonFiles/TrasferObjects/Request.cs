using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CommonFiles.TransferObjects
{
    /// <summary>
    /// Objects of this type will be send from the Testmachine to the Raspberry pi to control it
    /// The parameter variable can be any primitive Type and any Type annotated as KnownType
    /// </summary>
    [DataContract]
    [KnownType(typeof(int[]))]
    [KnownType(typeof(string[]))]
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