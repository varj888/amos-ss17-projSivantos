namespace RaspberryBackend
{
    /// <summary>
    /// This config contains all information on custom Symbols with the purpose to be print out on the LCD <see cref="LCD"/>
    /// </summary>
    static class SymbolConfig
    {
        /// <summary>
        /// Register Adress of the Battery Symbol on LCD. It can be accesed with <seealso cref="LCD.printSymbol"/>
        /// </summary>
        public static byte batterySymbolAddress => 0x1;
        /// <summary>
        /// Register Adress of the Initialized Symbol. It can be accesed with <seealso cref="LCD.printSymbol"/>
        /// </summary>
        public static byte initSymbolAddress => 0x2;
        /// <summary>
        /// Register Adress of the Busy Symbol on LCD. It can be accesed with <seealso cref="LCD.printSymbol"/>
        /// </summary>
        public static byte busySymbolAddress => 0x3;
        /// <summary>
        /// Register Adress of the Volume Symbol on LCD. It can be accesed with <seealso cref="LCD.printSymbol"/>
        /// </summary>
        public static byte volumeSymbolAddress => 0x4;

        /// <summary>
        /// The Battery Symbol which displays the exact current state of the Voltage-level
        /// </summary>
        public static byte[] currentBatterySymbol => getBatterySymbol();

        /// <summary>
        /// The Volume Symbol which displays an categorized state of the Volume-level
        /// </summary>
        public static byte[] currentVolumeSymbol => getVolumeSymbol();

        /// <summary>
        /// The Initialized Symbol which displays the exact state whether the RasPi is initialized or not
        /// </summary>
        public static byte[] currentInitySymbol => getInitSymbol(RaspberryPi.Instance.Control.RasPi.isInitialized());

        /// <summary>
        /// The Busy Symbol which displays the exact state whether the a Testmachine is connected or not
        /// </summary>
        public static byte[] currentBusySymbol => getBusySymbol();

        /// <summary>
        /// Initializes the Symbols so they can be printed by adressing them in the lcd. Symbols won't bi initilized if LCD is not initialized or they were already initialized
        /// </summary>
        internal static void initilizeSymbols()
        {
            initilize(currentBatterySymbol, batterySymbolAddress);
            initilize(currentInitySymbol, initSymbolAddress);
            initilize(currentBusySymbol, busySymbolAddress);
            initilize(currentVolumeSymbol, volumeSymbolAddress);
        }
        private static void initilize(byte[] symbole, byte symboleAdress)
        {
            if (!RaspberryPi.Instance.Control.LCD.isInitialized()) return;

            RaspberryPi.Instance.Control.LCD.createSymbol(symbole, symboleAdress);
        }

        /// <summary>
        /// Battery Symbol for LCD.
        /// </summary>
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

        /// <summary>
        /// Volume Symbol for LCD which currently works only with Analog Volume Control.
        /// </summary>
        public static byte[] volumeSymbol =
        {
            0b00000001,
            0b00000011,
            0b00000111,
            0b00001111,
            0b00011111,
            0b00011111,
            0b00111111,
            0b00111111,
        };

        /// <summary>
        /// Busy Symbol for LCD which means no Testmachine can connect because there another Testmachine is already connected.
        /// </summary>
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

        /// <summary>
        /// Initialized Symbol for LCD which means that the raspberry Pi is ready.
        /// </summary>
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

        /// <summary>
        /// Not Initialized Symbol for LCD which means that the raspberry Pi is not ready.
        /// </summary>
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


        /// <summary>
        /// Gets and updates the Battery Symbol
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets and updates the Initialize Symbol
        /// </summary>
        /// <returns></returns>
        public static byte[] getInitSymbol(bool isInit)
        {
            if (!RaspberryPi.Instance.Control.LCD.isInitialized()) return notInitSymbol;

            return (isInit) ? isInitSymbol : notInitSymbol;
        }

        /// <summary>
        /// Gets and updates the Busy Symbol
        /// </summary>
        /// <returns></returns>
        public static byte[] getBusySymbol()
        {
            byte[] data = (byte[])busySymbol.Clone();

            if (MainPage.clientSocket != null)
            {
                data = new byte[]
                {
                    0b00011111,
                    0b00001110,
                    0b00001110,
                    0b00000100,
                    0b00000100,
                    0b00001110,
                    0b00001110,
                    0b00011111,
                };
            }
            else
            {
                data = new byte[]
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
            }

            return data;
        }

        /// <summary>
        /// Gets and updates the Volume Symbol
        /// </summary>
        /// <returns></returns>
        public static byte[] getVolumeSymbol()
        {
            if (!RaspberryPi.Instance.Control.LCD.isInitialized()) return volumeSymbol;

            double vol = RaspberryPi.Instance.Control.Potentiometer.WiperState / 127.00;

            byte[] data = (byte[])volumeSymbol.Clone();

            if (vol > 0.8)
            {

            }
            else if (vol > 0.5 && vol < 0.8)
            {
                data = new byte[]
                {
                    0b00000001,
                    0b00000010,
                    0b00000100,
                    0b00001100,
                    0b00011100,
                    0b00011100,
                    0b00011100,
                    0b00011111,
                };
            }
            else if (vol < 0.6 && vol > 0.3)
            {
                data = new byte[]
                {
                    0b00000001,
                    0b00000010,
                    0b00000100,
                    0b00001000,
                    0b00011000,
                    0b00011000,
                    0b00011000,
                    0b00011111,
                };
            }
            else if (vol > 0 && vol < 0.4)
            {
                data = new byte[]
                {
                    0b00000001,
                    0b00000010,
                    0b00000100,
                    0b00001000,
                    0b00010000,
                    0b00010000,
                    0b00010000,
                    0b00011111,
                };
            }
            else if (vol < 0.3)
            {
                data = new byte[]
                {
                    0b00000001,
                    0b00000010,
                    0b00000100,
                    0b00001000,
                    0b00010000,
                    0b00000000,
                    0b00000000,
                    0b00011111,
                };
            }

            return data;
        }
    }
}
