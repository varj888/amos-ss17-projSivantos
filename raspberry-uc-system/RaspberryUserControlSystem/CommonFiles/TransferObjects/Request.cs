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
    }
}