using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace RaspberryBackend
{
    /// <summary>
    /// This static class parses the given XML Multiplexer configuration file
    /// and stores all possible multiplexer configurations in a dictionary
    /// </summary>
    static class HiXmlParser
    {
        private static Dictionary<string, Dictionary<List<string>, XPinConfig>> hi_dictionary = new Dictionary<string, Dictionary<List<string>, XPinConfig>>();

        private const string _CONFIG_PATH = "Config/xml/PinOutInfo.xml";

        private static XDocument config;

        static HiXmlParser()
        {
            config = XDocument.Load(_CONFIG_PATH);
            buildDictionary();
        }

        private static void buildDictionary()
        {
            IEnumerable<XNode> familyNodes = config.Element("PinOutInfo").Nodes();

            foreach (XElement familyElement in familyNodes)
            {

                string family_name = familyElement.Attribute("name").Value;

                Dictionary<List<string>, XPinConfig> tmp = new Dictionary<List<string>, XPinConfig>();

                IEnumerable<XNode> modelNodes = familyElement.Nodes();
                foreach (XElement modelElement in modelNodes)
                {
                    string model_names_string = modelElement.Attribute("name").Value;

                    List<string> model_names_list = new List<string>();

                    Char delimiter = ',';
                    String[] model_names = model_names_string.Split(delimiter);

                    model_names_list.AddRange(model_names);

                    List<string> pin_value_list = new List<string>();

                    IEnumerable<XNode> pinNodes = modelElement.Nodes();
                    foreach (XElement pinElement in pinNodes)
                    {
                        string pin_value = pinElement.Attribute("value").Value;
                        pin_value_list.Add(pin_value);
                    }

                    XPinConfig pin_config = new XPinConfig(family_name, model_names_string, pin_value_list);

                    tmp.Add(model_names_list, pin_config);
                }
                hi_dictionary.Add(family_name, tmp);
            }
        }

        /// <summary>
        /// Get the multiplexer configuation of a specific HI
        /// </summary>
        /// <param name="family">family name of the HI, e.g.: "Pure"</param>
        /// <param name="model_name">model name of the HI: e.g: "312 702 S (DN)"</param>
        /// <returns>
        /// Returns a HiType Object
        /// Returns null if the specified HI is not found
        /// </returns>
        public static XPinConfig getMultiplexerConfig(string family, string model_name)
        {
            if (hi_dictionary.ContainsKey(family))
            {
                Dictionary<List<string>, XPinConfig> family_dic = hi_dictionary[family];

                XPinConfig multiplex_config = null;
                bool model_found = false;

                foreach (List<string> model_names in family_dic.Keys)
                {
                    if (model_names.Contains(model_name))
                    {
                        multiplex_config = family_dic[model_names];
                        model_found = true;
                        break;
                    }
                }

                if (model_found)
                {
                    return multiplex_config;
                }
            }
            return null;
        }

        /// <summary>
        /// Creates a human readable string of the XML configuration file.
        /// </summary>
        /// <returns>
        /// Returns a string containing multiplexer configurations for all possible HIs
        /// </returns>
        public static string getXMLConfigAsString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string familyName in hi_dictionary.Keys)
            {
                sb.Append("Family " + familyName + ":");
                sb.Append("\n");

                Dictionary<List<string>, XPinConfig> model_dic = hi_dictionary[familyName];

                foreach (List<string> model_names in model_dic.Keys)
                {
                    sb.Append("\tModels: ");
                    sb.Append("\n");
                    foreach (string model_name in model_names)
                    {
                        sb.Append("\t\t[" + model_name + "]");
                        sb.Append("\n");
                    }

                    sb.Append("\n");

                    XPinConfig config_obj = model_dic[model_names];
                    Dictionary<int, string> config_map = config_obj.X_Pin_To_Value_Map;

                    sb.Append("\tConfig: ");
                    sb.Append("\n");
                    foreach (int key in config_map.Keys)
                    {
                        string value = config_map[key];

                        sb.Append("\t\t PinID: " + key + ", Value: " + value);
                        sb.Append("\n");
                    }
                }
                sb.Append("------------------------------------------------------------");
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public static string getAvailableHI()
        {
            return config.ToString(); ;
        }
    }
}
