using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    static class GPIOConfig
    {

        public static Dictionary<string, int> _gpio_to_Y_map = new Dictionary<string, int>();
        
        public static readonly UInt16 MULTIPLEXER_RESET_PIN = 18;
        public static readonly UInt16 PUSHBUTTON_PIN = 26;
        public static readonly UInt16 ROCKERSWITCH_PIN_0 = 20;
        public static readonly UInt16 ROCKERSWITCH_PIN_1 = 21;

        public static readonly string ROCKERSWITCH_STRING = "RockerSW";
        public static readonly string PUSHBUTTON_STRING = "PB";
        public static readonly string GROUND = "Ground";
        public static readonly string AMR = "AMR";
        public static readonly string AUDIOINPUT = "AudioInput";
        public static readonly string REC_DET = "REC_DET";
        public static readonly string LED = "LED";
        public static readonly string M = "M";
        public static readonly string STOP_END = "Stop-End";
        public static readonly string ENDLESS_VC = "EndlessVC";

        public static readonly int Y3 = 3;
        public static readonly int Y4 = 4;

        static GPIOConfig()
        {
            _gpio_to_Y_map.Add(GPIOConfig.PUSHBUTTON_STRING, Y3);
            _gpio_to_Y_map.Add(GPIOConfig.ROCKERSWITCH_STRING, Y4);

        }
    }
}
