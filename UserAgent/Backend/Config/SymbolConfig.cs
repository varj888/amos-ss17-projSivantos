
namespace RaspberryBackend
{
    static class SymbolConfig
    {
        public static bool symbolsInitilized { get; private set; } = false;

        public static byte batterySymbolAddress => 0x1;
        public static byte initSymbolAddress => 0x2;
        public static byte busySymbolAddress => 0x3;
        public static byte volumeSymbolAddress => 0x4;


        public static byte[] currentBatterySymbol => getBatterySymbol();
        public static byte[] currentVolumeSymbol => getVolumeSymbol();
        public static byte[] currentInitySymbol => getInitSymbol(RaspberryPi.Instance.Control.RasPi.isInitialized());
        public static byte[] currentBusySymbol => getBusySymbol();

        /// <summary>
        /// Initializes the Symbols so they can be printed by adressing them in the lcd. Symbols won't bi initilized if LCD is not initialized or they were already initialized
        /// </summary>
        public static void initilizeSymbols()
        {
            if (!RaspberryPi.Instance.Control.LCD.isInitialized()) return;

            RaspberryPi.Instance.Control.LCD.createSymbol(currentBatterySymbol, batterySymbolAddress);
            RaspberryPi.Instance.Control.LCD.createSymbol(currentInitySymbol, initSymbolAddress);
            RaspberryPi.Instance.Control.LCD.createSymbol(currentBusySymbol, busySymbolAddress);
            RaspberryPi.Instance.Control.LCD.createSymbol(currentVolumeSymbol, volumeSymbolAddress);

            symbolsInitilized = true;
        }


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
            if (!RaspberryPi.Instance.Control.LCD.isInitialized()) return batterySymbol;

            double batstatus = RaspberryPi.Instance.Control.ADConverter.CurrentDACVoltage1 / RaspberryPi.Instance.Control.ADConverter.getMaxVoltage();
            byte[] data = (byte[])batterySymbol.Clone();

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
            if (!RaspberryPi.Instance.Control.LCD.isInitialized()) return notInitSymbol;

            return (isInit) ? isInitSymbol : notInitSymbol;
        }

        public static byte[] getBusySymbol()
        {
            return busySymbol;
        }

        public static byte[] getVolumeSymbol()
        {
            if (!RaspberryPi.Instance.Control.LCD.isInitialized()) return volumeSymbol;

            double vol = RaspberryPi.Instance.Control.Potentiometer.WiperState / 127.00;

            byte[] data = (byte[])volumeSymbol.Clone();

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
