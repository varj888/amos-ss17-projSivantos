using System.Collections.Generic;
using System.Linq;

namespace RaspberryBackend
{
    /// <summary>
    /// Represents the multiplexer configuration for a specific HI
    /// </summary>
    public class XPinConfig : MultiplexerConfig
    {

        /// <summary>
        /// Builds a dictionary containing the multiplexer configuration for a specific HI
        /// </summary>
        /// <param name="family">HiFamily name of the HI, e.g.: "Pure"</param>
        /// <param name="model_name">HiModel name of the HI: e.g: "312 702 S (DN)"</param>
        /// <param name="value_list">A List containing all possible HI function, e.g.: ["RockerSW","Ground","PB",...]</param>
        public XPinConfig(string family, string model_name, List<string> value_list)
        {
            _x_pin_to_value_map = new Dictionary<int, string>();

            for (int i = 0; i < value_list.Count; ++i)
            {
                _x_pin_to_value_map.Add(i, value_list[i]);
            }
        }

        /// <summary>
        /// Getter field for HI functionalities and their pins
        /// </summary>
        /// <returns>
        /// Returns all pins that a specific function of the HI is mapped to, or empty when a specific feature
        /// is unavailable for a certain HiModel e.g.: "Ground" = 3, 4, 9 ; "RockerSW" = 8, "PB" = - , ...
        /// </returns>
        public int[] ValueToPins(string val)
        {
            return _x_pin_to_value_map.Where(pair => pair.Value.Equals(val)).Select(pair => pair.Key).ToArray<int>();
        }
    }
}
