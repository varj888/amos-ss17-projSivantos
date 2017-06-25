using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It activates a combination of pins.
    /// </summary>
    public partial class RaspberryPi
    {
        /* Dictionaries are not Serializable in C# by default -> To adapt for serialization to an XML file a temporary class is used instead.*/
        private static int statusMessageCount = 0;

        /// <summary>
        /// Executes the Command StatusInformation
        /// </summary>
        /// <param name="parameter">represents the GpioPin to read from</param>
        /// <returns>The current state of the Raspberry Pi: Status of user controls, available features, hardware information.
        public XElement getStatus()
        {

            XElement xml = new XElement("RaspberryPi",
                new XAttribute("Timestamp", DateTime.Now.ToString("hh:mm:ss")),
                new XAttribute("Message", 1),
                new XElement("IPAddress", this.GetIpAddressAsync()),
                new XElement("Initialized", this._initialized),
                new XElement("TestMode", this.testMode)
            );
            /* TODO if possible: read & return currently displayed text.
             * Depending whether or not LCD is connected, status information differs. */
            xml.Add(new XElement("LCD", 
                new XElement("Initialized", this.LCD.isInitialized()), 
                new XElement("Text", this.LCD.isInitialized() ? this.LCD.CurrentText.ToString() : "")));
            xml.Add(new XElement("Potentiometer",
                new XElement("Initialized", this.Potentiometer.isInitialized()),
                new XElement("Wiper", this.Potentiometer.WiperState)));
            xml.Add(new XElement("LED",
                new XElement("Initialized", "-"), // possible to update this later on to better reflect whether or not an LED is even attached
                new XElement("Status", this.readPin(24)))); // gpio interface read pin led pin 24
            xml.Add(new XElement("Multiplexer",
                new XElement("Initialized", this.Multiplexer.isInitialized()), // possible to update this later on to better reflect whether or not an LED is even attached
                new XElement("Family", this.Multiplexer.family),
                new XElement("ModelName", this.Multiplexer.model_name)
                ));
            /* Read config from XML and based on that return supported controls of current HI*/
            xml.Add(new XElement("HI"),
            // TODO Add PIN Information, possibly parse in MultiplexerConfigParser first
                new XElement("RockerSW", "PIN 0"),
                new XElement("Ground", "PIN 1, PIN 2, PIN 3"),
                new XElement("AMR", ""));
            statusMessageCount++; // count send results for no particular reason other than ensuring order of status updates
            return xml;
        }
    }
}
