using System.Collections.Generic;

namespace RaspberryBackend
{
    /// <summary>
    /// Represents the configuration of RasperyyPi components to the multiplexer inputs Y1-Y7
    /// </summary>
    public class YPinConfig : MultiplexerConfig
    {
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
        public static readonly string ARD = "ARD";
        public static readonly string DET_TELE = "DET_TELE";
        public static readonly string DET_AUDIO = "DET_AUDIO";

        public static readonly int Y0 = 0;
        public static readonly int Y1 = 1;
        public static readonly int Y2 = 2;
        public static readonly int Y3 = 3;
        public static readonly int Y4 = 4;
        public static readonly int Y5 = 5;
        public static readonly int Y6 = 6;
        public static readonly int Y7 = 7;


        /// <summary>
        /// Builds a dictionary containing the configuration of RasperyyPi components to the multiplexer inputs Y1-Y7
        /// </summary>
        public YPinConfig()
        {
            gpio_To_YPin_Map = new Dictionary<string, int>();

            gpio_To_YPin_Map.Add(GROUND, Y0);
            gpio_To_YPin_Map.Add(REC_DET, Y1);
            gpio_To_YPin_Map.Add(LED, Y2);
            gpio_To_YPin_Map.Add(PUSHBUTTON_STRING, Y3);
            gpio_To_YPin_Map.Add(ROCKERSWITCH_STRING, Y4);
            gpio_To_YPin_Map.Add(ARD, Y1);
            gpio_To_YPin_Map.Add(DET_TELE, Y6);
            gpio_To_YPin_Map.Add(DET_AUDIO, Y7);
            //TODO: Map Y5-Y7 as soon as we know what the strings mean...

        }
    }
}
