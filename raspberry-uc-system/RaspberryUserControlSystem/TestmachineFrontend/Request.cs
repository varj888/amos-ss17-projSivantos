using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestmachineFrontend
{
    /// <summary>
    /// Unit of transfer by the RequestConnClient Class
    /// it is only as a container for the two variables methodName and parameter
    /// note: this class uses the default contract namespace
    /// </summary>
    [DataContract]
    public class Request
    {
        public Request(string methodName, Object parameter)
        {
            this.methodName = methodName;
            this.parameter = parameter;
        }

        [DataMember]
        public string methodName;

        [DataMember]
        public Object parameter;
    }

}
