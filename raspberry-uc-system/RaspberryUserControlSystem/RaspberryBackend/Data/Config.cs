using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    ///
    /// </summary>
    class Config
    {
        private Dictionary<int, string> _pin_value_map;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value_list"></param>
        public Config(List<string> value_list)
        {
            _pin_value_map = new Dictionary<int, string>();

            int count = 1;
            foreach(string value in value_list)
            {
                Pin_value_map.Add(count, value);
                count++;
            }

        }
        /// <summary>
        ///
        /// </summary>
        public Dictionary<int, string> Pin_value_map { get => _pin_value_map; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public string getValue(int pin)
        {
            return Pin_value_map[pin];
        }
    }
}
