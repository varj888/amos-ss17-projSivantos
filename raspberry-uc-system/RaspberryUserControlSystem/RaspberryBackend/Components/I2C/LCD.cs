using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using System.Threading;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the LCD Display. 
    /// </summary>
    public class LCD

    {
        //Adress setup information
        public const string I2C_CONTROLLER_NAME = "I2C1"; //use for RPI2
        public const byte DEVICE_I2C_ADDRESS = 0x27; // 7-bit I2C address of the port expander


        private const byte LCD_WRITE = 0x07;
        private const byte Command_sendMode = 0;
        private const byte Data_sendMode = 1;

        //Setup information for lcd initialization (visit lcd documentation for further information)
        public const byte EN = 0x02;
        public const byte RW = 0x01;
        public const byte RS = 0x00;
        public const byte D4c = 0x04;
        public const byte D5c = 0x05;
        public const byte D6c = 0x06;
        public const byte D7c = 0x07;
        public const byte BL = 0x03;

        private byte[] _LineAddress = new byte[] { 0x00, 0x40 };

        public byte backLight { get; set; } = 0x00;
        public int scrollSpeed { get; set; }
        public CancellationTokenSource cancelToken { get; set; }

        private I2cDevice _lcdDisplay;

        public LCD()
        {

            // It's async method, so we have to wait
            Task.Run(() => this.startI2C()).Wait();
        }

        /**
        * Start I2C Communication
        **/
        public async void startI2C()
        {
            try
            {
                var i2cSettings = new I2cConnectionSettings(DEVICE_I2C_ADDRESS);
                i2cSettings.BusSpeed = I2cBusSpeed.FastMode;
                string deviceSelector = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);
                var i2cDeviceControllers = await DeviceInformation.FindAllAsync(deviceSelector);
                this._lcdDisplay = await I2cDevice.FromIdAsync(i2cDeviceControllers[0].Id, i2cSettings);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", e.Message);
                return;
            }

        }


        /**
        * Initialization
        **/
        public void initiateLCD(bool turnOnDisplay = true, bool turnOnCursor = false, bool blinkCursor = false, bool cursorDirection = true, bool textShift = false)
        {
            //Task.Delay(100).Wait();
            //pulseEnable(Convert.ToByte((turnOnBacklight << 3)));

            /* Init sequence */
            Task.Delay(100).Wait();
            pulseEnable(Convert.ToByte((1 << D5c) | (1 << D4c)));
            Task.Delay(5).Wait();
            pulseEnable(Convert.ToByte((1 << D5c) | (1 << D4c)));
            Task.Delay(5).Wait();
            pulseEnable(Convert.ToByte((1 << D5c) | (1 << D4c)));

            /*  Init 4-bit mode */
            pulseEnable(Convert.ToByte((1 << D5c)));

            /* Init 4-bit mode + 2 line */
            pulseEnable(Convert.ToByte((1 << D5c)));
            pulseEnable(Convert.ToByte((1 << D7c)));

            /* Turn on display, cursor */
            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D7c) | (Convert.ToByte(turnOnDisplay) << D6c) | (Convert.ToByte(turnOnCursor) << D5c) | (Convert.ToByte(blinkCursor) << D4c)));

            this.clrscr();

            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D6c) | (Convert.ToByte(cursorDirection) << D5c) | (Convert.ToByte(textShift) << D4c)));
        }

        /**
       * Create falling edge of "enable" pin to write data/inctruction to display
       */
        private void pulseEnable(byte data)
        {
            // Enable bit HIGH
            this._lcdDisplay.Write(new byte[] { Convert.ToByte(data | (1 << EN) | (backLight << BL)) });
            // Enable bit LOW
            this._lcdDisplay.Write(new byte[] { Convert.ToByte(data | (this.backLight << BL)) });
            //Task.Delay(2).Wait(); //In case of problem with displaying wrong characters uncomment this part
        }

        /**
        * Clear display and set cursor at start of the first line
        **/
        public void clrscr()
        {
            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D4c)));
            Task.Delay(5).Wait();
        }


        /// <summary>
        /// sends information to the LCD either data or commands
        /// </summary>
        /// <param name="data">information which is to be sent on LCD</param>
        /// <param name="Rs">Rs=0 for Command or Rs = 1 for Data</param>        
        public void write(byte data, byte Rs)
        {
            pulseEnable(Convert.ToByte((data & 0xf0) | (Rs << RS)));
            pulseEnable(Convert.ToByte((data & 0x0f) << 4 | (Rs << RS)));
            //Task.Delay(5).Wait(); //In case of problem with displaying wrong characters uncomment this part
        }

        //========================================================================================================
        //======================== The Following methods are not used by this class ==============================
        //======================== and shoul be moved to Commands                   ==============================
        //========================================================================================================


        /**
        * Save custom symbol to CGRAM
        **/
        public void createSymbol(byte[] data, byte address)
        {
            write(Convert.ToByte(0x40 | (address << 3)), Command_sendMode);

            for (var i = 0; i < data.Length; i++)
            {
                write(data[i], Data_sendMode);
            }
            this.clrscr();
        }


        /**
        * Print custom symbol
        **/
        public void printSymbol(byte address)
        {
            write(address, Data_sendMode);
        }


        /**
        * goto X and Y 
        **/
        public void gotoxy(byte x, byte y)
        {
            write(Convert.ToByte(x | _LineAddress[y] | (1 << LCD_WRITE)), Command_sendMode);
        }




    }

}