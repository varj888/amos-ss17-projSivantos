using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend.Data
{
    class Breadboard
    {

        private string _family_name;
        private string _model_name;

        private Config _pin_config;

        public Breadboard(string family, string model, Config config)
        {
            _family_name = family;
            _model_name = model;
            _pin_config = config;
        }

        public string Family_name { get => _family_name; }
        public string Model_name { get => _model_name; }
        internal Config Pin_config { get => _pin_config;}
    }
}
