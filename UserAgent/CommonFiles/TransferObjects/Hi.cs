using System.Runtime.Serialization;

namespace RaspberryBackend
{
    [DataContract]
    public class Hi
    {
        [DataMember]
        public string CurrentReceiver { get; set; } = "None";

        /// <summary>
        /// Current Hi Family which the Multiplexer is configured on
        /// </summary>
        [DataMember]
        public string Family { get; set; }

        /// <summary>
        /// Current Hi Model which the Multiplexer is configured on
        /// </summary>
        [DataMember]
        public string Model { get; set; }


        public override string ToString()
        {
            return "Family : " + Family + " \n Model: " + Model + " \n CurrentReceiver:" + CurrentReceiver + " \n";
        }
    }
}
