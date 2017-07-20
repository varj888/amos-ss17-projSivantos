namespace RaspberryBackend
{
    static class SymbolConfig
    {
        public static byte batterySymbolAddress = 0x1;
        public static byte initSymbolAddress = 0x2;
        public static byte busySymbolAddress = 0x3;
        public static byte volumeSymbolAddress = 0x4;



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

        public static byte[] volumeSymbol =
        {
            0b00000001,
            0b00000001,
            0b00000011,
            0b00000111,
            0b00000111,
            0b00001111,
            0b00011111,
            0b00111111,
        };

        public static byte[] busySymbol =
        {
            0b00011111,
            0b00001010,
            0b00001010,
            0b00000100,
            0b00000100,
            0b00001010,
            0b00001010,
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

        public static byte[] getBatterySymbol()
        {
            double batstatus = RaspberryPi.Instance.Control.ADConverter.CurrentDACVoltage1 / RaspberryPi.Instance.Control.ADConverter.getMaxVoltage();
            byte[] data = (byte[])SymbolConfig.batterySymbol.Clone();

            for (int i = 1; i <= 6; i++)
            {
                int counter = 6;
                double frac = (double)i / 6.0;
                if (batstatus < frac)
                {
                    data[counter - i] = 0b00010001;
                }
                counter--;
            }

            return data;
        }

        public static byte[] getInitSymbol(bool isInit)
        {
            return (isInit) ? SymbolConfig.isInitSymbol : SymbolConfig.notInitSymbol;
        }

        public static byte[] getBusySymbol()
        {
            return busySymbol;
        }

        public static byte[] getVolumeSymbol()
        {
            double vol = RaspberryPi.Instance.Control.Potentiometer.WiperState / 127.00;

            byte[] data = (byte[])SymbolConfig.volumeSymbol.Clone();

            if (vol > 0.8)
            {
                data = new byte[]
                {
                    0b00000001,
                    0b00000001,
                    0b00000011,
                    0b00000111,
                    0b00000111,
                    0b00001111,
                    0b00011111,
                    0b00111111,
                };
            }
            else if (vol > 0.5 && vol < 0.8)
            {
                data = new byte[]
                {
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000100,
                    0b00000100,
                    0b00001100,
                    0b00011100,
                    0b00111111,
                };
            }
            else if (vol < 0.6 && vol > 0.3)
            {
                data = new byte[]
                {
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00001000,
                    0b00011000,
                    0b00111111,
                };
            }
            else if (vol > 0 && vol < 0.4)
            {
                data = new byte[]
                {
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00010000,
                    0b00111111,
                };
            }
            else if (vol < 0.3)
            {
                data = new byte[]
                {
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00000000,
                    0b00111111,
                };
            }

            return data;
        }
    }
}
