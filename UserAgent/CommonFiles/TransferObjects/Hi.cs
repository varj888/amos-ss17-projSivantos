using System.Runtime.Serialization;

namespace RaspberryBackend
{
    /// <summary>
    /// A wrapper-class to represent an HI.
    /// </summary>
    [DataContract]
    public class Hi
    {
        [DataMember]
        public string CurrentReceiver { get; set; } = "None";

        /// <summary>
        /// Current HI family the multiplexer is configured to.
        /// </summary>
        [DataMember]
        public string Family { get; set; }

        /// <summary>
        /// Current Hi Model the Multiplexer is configured to.
        /// </summary>
        [DataMember]
        public string Model { get; set; }

        /// <summary>
        /// Method to define the string-representation of an HI.
        /// </summary>
        /// <returns>The string representation of an HI.</returns>
        public override string ToString()
        {
            return "Family : " + Family + " \n Model: " + Model + " \n CurrentReceiver:" + CurrentReceiver + " \n";
        }
    }
}
