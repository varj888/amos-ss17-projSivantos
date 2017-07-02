using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend.Config
{
    static class SymbolConfig
    {
        public static byte batterySymbolAddress = 0x1;
        public static byte initSymbolAddress = 0x2;

        public static byte[] batterySymbol =
            {
                0b00001110,
                0b00011111,
                0b00011111,
                0b00011111,
                0b00011111,
                0b00011111,
                0b00011111,
                0b00011111,
            };

        public static byte[] isInitSymbol =
        {
                0b00000000,
                0b00000001,
                0b00000001,
                0b00000010,
                0b00000010,
                0b00010100,
                0b00001100,
                0b00000100,
        };

        public static byte[] notInitSymbol =
        {
                0b00010001,
                0b00010001,
                0b00001010,
                0b00001010,
                0b00000100,
                0b00000100,
                0b00001010,
                0b00010001,
        };

    }
}
