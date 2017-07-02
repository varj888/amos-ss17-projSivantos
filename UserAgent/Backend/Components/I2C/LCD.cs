using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.I2c;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the LCD Display.
    /// </summary>
    public class LCD : HWComponent
    {
        //Adress setup information
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

        public byte backLight { get; set; }
        public int scrollSpeed { get; set; }

        private I2cDevice _lcdDisplay;
        /* Stores text that has been most recently written to LCD.
         * This might differ from actual hardware status caused
         * e.g. by an error such as a physical bitshift.
         * -> Use actual hardware read-back in future. */
        public StringBuilder CurrentText { get; private set; } = new StringBuilder();

        public override void initiate()
        {
            try
            {
                Task.Run(() => I2C.connectDeviceAsync(DEVICE_I2C_ADDRESS, true, false)).Wait();
                I2C.connectedDevices.TryGetValue(DEVICE_I2C_ADDRESS, out _lcdDisplay);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Problem with I2C " + e.Message);
                throw e;
            }

            initiateLCD();
        }

        /**
        * Initialization
        **/
        public void initiateLCD(bool turnOnDisplay = true, bool turnOnCursor = false, bool blinkCursor = false, bool cursorDirection = true, bool textShift = false)
        {
            Task.Delay(100).Wait();
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

            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D6c) | (Convert.ToByte(cursorDirection) << D5c) | (Convert.ToByte(textShift) << D4c)));

            this.clrscr();
            _initialized = true;
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
            //Task.Delay(100).Wait(); //In case of problem with displaying wrong characters uncomment this part
        }

        /**
        * Clear display and set cursor at start of the first line
        **/
        public void clrscr()
        {
            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D4c)));
            Task.Delay(5).Wait();
            CurrentText.Clear();
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


        /// <summary>
        /// skip to second line
        /// </summary>
        private void gotoSecondLine()
        {
            write(0xc0, Command_sendMode);
        }

        /// <summary>
        /// prints text in two lines
        /// </summary>
        /// <param name="text">text which shall be displayed</param>
        /// <param name="charsMaxInLine">determines the maximum chars on a line</param>
        public void printInTwoLines(string text, int charsMaxInLine = 16)
        {
            string line1 = "", line2 = "";

            line1 = text.Substring(0, charsMaxInLine);
            line2 = text.Substring(charsMaxInLine);
            prints(line1);
            gotoSecondLine();
            CurrentText.AppendLine();
            prints(line2);
        }


        /// <summary>
        /// prints text in two lines
        /// </summary>
        /// <param name="text">text which shall be displayed</param>
        /// <param name="charsMaxInLine">determines the maximum chars on a line</param>
        public void printInTwoLines(string textLine1, string textLine2)
        {
            prints(textLine1);
            gotoSecondLine();
            prints(textLine2);
        }


        /// <summary>
        /// pints text in two lines
        /// </summary>
        /// <param name="text">text which shall be displayed</param>
        /// <param name="charsMaxInLine">determines the maximum chars on a line</param>
        public void printInSecondLine(string text)
        {
            gotoSecondLine();

            //Task.Run(() => scrollText(text)).Wait(); //if there is a problem
            Task.Run(() => scrollText(text));
        }

        private void scrollText(string text)
        {
            int maxChars = 16;
            int speedInChars = 3;
            clrscr();

            for (int i = 0; i <= text.Length - maxChars; i = i + speedInChars < text.Length ? i + speedInChars : text.Length)
            {
                Task.Delay(200).Wait();
                clrscr();
                for (int j = i; j < maxChars + i && j < text.Length; j++)
                {
                    printc(text.ElementAt(j));
                }
                Task.Delay(200).Wait();
            }
        }
        /// <summary>
        /// Print string to LCD display
        /// </summary>
        /// <param name="s"></param>
        public void writeToLCD(string s)
        {
            clrscr();
            prints(s);
        }

        /// <summary>
        /// Prints a string onto display
        /// </summary>
        /// <param name="text">text which shall be displayed</param>
        public void prints(string text)
        {
            foreach (char c in text)
            {
                this.printc(c);
                CurrentText.Append(c); // append string to variable that you can read later on when delivering status information
            }
        }

        /// <summary>
        /// Prints a character onto display
        /// </summary>
        /// <param name="letter">character which shall be displayed</param>
        private void printc(char letter)
        {
            try
            {
                write(Convert.ToByte(letter), Data_sendMode);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// goto X and Y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void gotoxy(byte x, byte y)
        {
            write(Convert.ToByte(x | _LineAddress[y] | (1 << LCD_WRITE)), Command_sendMode);
        }

        /// <summary>
        /// Reset the LCD (clear it's screen)
        /// </summary>
        public void resetLCD()
        {
            initiateLCD();

        }

        /// <summary>
        /// Save custom symbol to CGRAM
        /// </summary>
        /// <param name="data"></param>
        /// <param name="address"></param>
        public void createSymbol(byte[] data, byte address)
        {
            write(Convert.ToByte(0x40 | (address << 3)), Command_sendMode);

            for (var i = 0; i < data.Length; i++)
            {
                write(data[i], Data_sendMode);
            }
            this.clrscr();
        }

        /// <summary>
        /// Print custom symbol
        /// </summary>
        /// <param name="address"></param>
        public void printSymbol(byte address)
        {
            write(address, Data_sendMode);
        }


    }
}