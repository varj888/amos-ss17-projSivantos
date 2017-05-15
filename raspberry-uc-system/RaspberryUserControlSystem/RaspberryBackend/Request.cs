using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


    /// <summary>
    /// Unit of transfer by the RequestConnClient Class
    /// is only as a container for the two variables methodName and parameter
    /// note: this class uses the default contract namespace
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
