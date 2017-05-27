using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    public class lcdConfig
    {
        //Adress setup information
        public const string I2C_CONTROLLER_NAME = "I2C1"; //use for RPI2
        public const byte DEVICE_I2C_ADDRESS = 0x27; // 7-bit I2C address of the port expander

        //Setup pins
        public const byte EN = 0x02;
        public const byte RW = 0x01;
        public const byte RS = 0x00;
        public const byte D4 = 0x04;
        public const byte D5 = 0x05;
        public const byte D6 = 0x06;
        public const byte D7 = 0x07;
        public const byte BL = 0x03;
    }
}
