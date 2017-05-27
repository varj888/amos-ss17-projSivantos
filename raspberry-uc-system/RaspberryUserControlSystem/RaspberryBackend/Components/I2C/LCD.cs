using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using System.Threading;

namespace RaspberryBackend
{
    public class LCD

    {
        //Adress setup information
        public const string I2C_CONTROLLER_NAME = "I2C1"; //use for RPI2
        public const byte DEVICE_I2C_ADDRESS = 0x27; // 7-bit I2C address of the port expander


        private const byte LCD_WRITE = 0x07;

        //Setup information for lcd initialization (visit lcd documentation for further information)
        public byte En = 0x02;
        public byte Rw = 0x01;
        public byte Rs = 0x00;
        public byte D4 = 0x04;
        public byte D5 = 0x05;
        public byte D6 = 0x06;
        public byte D7 = 0x07;
        public byte Bl = 0x03;

        private byte[] _LineAddress = new byte[] { 0x00, 0x40 };

        public byte backLight { get; set; } = 0x01;
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
            pulseEnable(Convert.ToByte((1 << D5) | (1 << D4)));
            Task.Delay(5).Wait();
            pulseEnable(Convert.ToByte((1 << D5) | (1 << D4)));
            Task.Delay(5).Wait();
            pulseEnable(Convert.ToByte((1 << D5) | (1 << D4)));

            /*  Init 4-bit mode */
            pulseEnable(Convert.ToByte((1 << D5)));

            /* Init 4-bit mode + 2 line */
            pulseEnable(Convert.ToByte((1 << D5)));
            pulseEnable(Convert.ToByte((1 << D7)));

            /* Turn on display, cursor */
            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D7) | (Convert.ToByte(turnOnDisplay) << D6) | (Convert.ToByte(turnOnCursor) << D5) | (Convert.ToByte(blinkCursor) << D4)));

            this.clrscr();

            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D6) | (Convert.ToByte(cursorDirection) << D5) | (Convert.ToByte(textShift) << D4)));
        }

        /**
       * Create falling edge of "enable" pin to write data/inctruction to display
       */
        private void pulseEnable(byte data)
        {
            // Enable bit HIGH
            this._lcdDisplay.Write(new byte[] { Convert.ToByte(data | (1 << En) | (backLight << Bl)) });
            // Enable bit LOW
            this._lcdDisplay.Write(new byte[] { Convert.ToByte(data | (this.backLight << Bl)) });
            //Task.Delay(2).Wait(); //In case of problem with displaying wrong characters uncomment this part
        }

        /**
        * Clear display and set cursor at start of the first line
        **/
        public void clrscr()
        {
            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D4)));
            Task.Delay(5).Wait();
        }

        /**
       * Send pure data to display
       **/
        public void write(byte data, byte Rs)
        {
            pulseEnable(Convert.ToByte((data & 0xf0) | (Rs << this.Rs)));
            pulseEnable(Convert.ToByte((data & 0x0f) << 4 | (Rs << this.Rs)));
            //Task.Delay(5).Wait(); //In case of problem with displaying wrong characters uncomment this part
        }

        //========================================================================================================
        //======================== The Following methods are not used by this class ==============================
        //========================================================================================================

        /**
      * Send data to display
      **/
        public void sendData(byte data)
        {
            this.write(data, 1);
        }


        /**
        * Send command to display
        **/
        public void sendCommand(byte data)
        {
            this.write(data, 0);
        }

        /**
        * Print single character onto display
        **/
        public void printc(char letter)
        {
            try
            {
                this.write(Convert.ToByte(letter), 1);
            }
            catch (Exception e)
            {

            }
        }


        /**
        * Save custom symbol to CGRAM
        **/
        public void createSymbol(byte[] data, byte address)
        {
            this.sendCommand(Convert.ToByte(0x40 | (address << 3)));
            for (var i = 0; i < data.Length; i++)
            {
                this.sendData(data[i]);
            }
            this.clrscr();
        }


        /**
        * Print custom symbol
        **/
        public void printSymbol(byte address)
        {
            this.sendData(address);
        }

        public void printInTwoLines(string text, int charsMaxInLine)
        {
            string line1 = "", line2 = "";

            line1 = text.Substring(0, charsMaxInLine);
            line2 = text.Substring(charsMaxInLine);

            prints(line1);
            gotoSecondLine();
            prints(line2);
        }

        /**
    * skip to second line
    **/
        public void gotoSecondLine()
        {
            this.sendCommand(0xc0);
        }


        /**
        * goto X and Y 
        **/
        public void gotoxy(byte x, byte y)
        {
            this.sendCommand(Convert.ToByte(x | _LineAddress[y] | (1 << LCD_WRITE)));
        }

        public byte getBackLightStatus()
        {
            return backLight;
        }


        /**
        * Can print string onto display
        **/
        public void prints(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                this.printc(text[i]);
            }
        }


        private void scrollText(string text, int countChars)
        {
            int maxChars = 16;

            clrscr();

            for (int i = 0; i <= text.Length - maxChars; i = i + countChars < text.Length ? i + countChars : text.Length)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    return;
                }

                Task.Delay(200).Wait();
                clrscr();
                for (int j = i; j < maxChars + i && j < text.Length; j++)
                {
                    printc(text.ElementAt(j));
                }
                Task.Delay(200).Wait();
            }

        }

        internal void sendTextToLcd(String text)
        {

            const int charsMaxInLine = 16;

            clrscr();

            if (text.Length <= charsMaxInLine)
            {
                prints(text);
            }

            else if (text.Length > charsMaxInLine && text.Length <= 2 * charsMaxInLine)
            {
                Task.Factory.StartNew(() => printInTwoLines(text, charsMaxInLine));

            }
            else
            {
                try
                {
                    Task scrollTextTask = Task.Factory.StartNew(() => scrollText(text, scrollSpeed), cancelToken.Token);
                }
                catch (OperationCanceledException)
                {

                }

            }
        }
    }

}