using System.Collections.Generic;
using System.Linq;

namespace RaspberryBackend
{
    /// <summary>
    /// Represents the multiplexer configuration for a specific HI 
    /// </summary>
    class MultiplexerConfig
    {
        private string _family;
        private string _model_name;
        private Dictionary<int, string> _x_pin_to_value_map;

        /// <summary>
        /// Builds a dictionary containing the multiplexer configuration for a specific HI
        /// </summary>
        /// <param name="family">family name of the HI, e.g.: "Pure"</param>
        /// <param name="model_name">model name of the HI: e.g: "312 702 S (DN)"</param>
        /// <param name="value_list">A List containing all possible HI function, e.g.: ["RockerSW","Ground","PB",...]</param>
        public MultiplexerConfig(string family, string model_name, List<string> value_list)
        {
            _family = family;
            _model_name = model_name;
            _x_pin_to_value_map = new Dictionary<int, string>();

            for(int i = 0; i < value_list.Count; ++i)
            {
                X_Pin_To_Value_Map.Add(i, value_list[i]);
            }
        }
        /// <summary>
        /// Getter field for the Multiplexer configuration dictionary
        /// </summary>
        /// <returns>
        /// Returns a Dictionary<int,string> containing multiplexer configurations for the specific HI, e.g.: 1 = "RockerSW", 2 = "Ground", 3 = "PB", ...
        /// </returns>
        public Dictionary<int, string> X_Pin_To_Value_Map { get => _x_pin_to_value_map; }

        /// <summary>
        /// Getter field for HI functionalities and their pins
        /// </summary>
        /// <returns>
        /// Returns all pins that a specific function of the HI is mapped to, or empty when a specific feature
        /// is unavailable for a certain model e.g.: "Ground" = 3, 4, 9 ; "RockerSW" = 8, "PB" = - , ...
        /// </returns>
        public int[] ValueToPins(string val)
        {
            return _x_pin_to_value_map.Where(pair => pair.Value.Equals(val)).Select(pair => pair.Key).ToArray<int>();
        }
    }
}
