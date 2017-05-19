using System.Runtime.Serialization;



    /// <summary>
    /// Unit of transfer by the RequestConnClient Class
    /// it is only as a container for the two variables methodName and parameter
    /// note: this class uses the default contract namespace
    /// </summary>
    [DataContract]
    public class Request
    {
        public Request(string command, object parameter)
        {
            this.command = command;
            this.parameter = parameter;
        }

        [DataMember]
        public string command;

        [DataMember]
        public object parameter;
    }


