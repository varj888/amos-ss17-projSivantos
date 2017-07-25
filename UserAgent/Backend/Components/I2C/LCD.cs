using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        private int LCD_MAX_LENGTH = 32;

        //Setup information for lcd initialization (visit lcd documentation for further information)
        public const byte EN = 0x02;
        public const byte RW = 0x01;
        public const byte RS = 0x00;
        public const byte D4c = 0x04;
        public const byte D5c = 0x05;
        public const byte D6c = 0x06;
        public const byte D7c = 0x07;
        public const byte BL = 0x03;

        private bool cancelRequest = false;
        private bool shifting = false;
        private CancellationTokenSource _cts;

        private byte[] _LineAddress = new byte[] { 0x00, 0x40 };

        public byte backLight { get; set; }
        public int scrollSpeed { get; set; }

        private I2cDevice _lcdDisplay;
        /* Stores text that has been most recently written to LCD.
         * This might differ from actual hardware status caused
         * e.g. by an error such as a physical bitshift.
         * -> Use actual hardware read-back in future. */
        public StringBuilder CurrentText { get; private set; } = new StringBuilder();
        public string CurrentTextPlainString { get; internal set; }

        /// <summary>
        /// Getter for LCD_MAX_LENGTH
        /// </summary>
        /// <returns>LCD_MAX_LENGTH</returns>
        public int getMaxLength()
        {
            return this.LCD_MAX_LENGTH;
        }

        /// <summary>
        /// Method to wrap up initiation of the LCD display. Connect to the device and get its address.
        /// </summary>
        public override void initiate()
        {
            try
            {
                Task.Run(() => I2C.connectDeviceAsync(DEVICE_I2C_ADDRESS, true, false)).Wait();
                I2C.connectedDevices.TryGetValue(DEVICE_I2C_ADDRESS, out _lcdDisplay);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Problem with I2C " + e.Message);
                throw e;
            }

            initiateLCD();
        }

        /// <summary>
        /// Initiate the LCD display.
        /// </summary>
        /// <param name="turnOnDisplay">Flag to control whether the display shall light up.</param>
        /// <param name="turnOnCursor">Flag to control display of the cursor.</param>
        /// <param name="blinkCursor">Flag to control whether cursor shall be represented by an underscore or a blinking box.</param>
        /// <param name="cursorDirection">Flag to control the cursor-direction.</param>
        /// <param name="textShift">Control whether text with exceeding length shifts the display.</param>
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

        /// <summary>
        /// Create falling edge of "enable" pin to write data/inctruction to display. This method is only used during initialization.
        /// </summary>
        /// <param name="data">Data to write.</param>
        private void pulseEnable(byte data)
        {
            // Enable bit HIGH
            this._lcdDisplay.Write(new byte[] { Convert.ToByte(data | (1 << EN) | (backLight << BL)) });
            // Enable bit LOW
            this._lcdDisplay.Write(new byte[] { Convert.ToByte(data | (this.backLight << BL)) });
            //Task.Delay(100).Wait(); //In case of problem with displaying wrong characters uncomment this part
        }

        /// <summary>
        /// Clear display and set cursor at start of the first line
        /// </summary
        public void clrscr()
        {
            pulseEnable(0);
            pulseEnable(Convert.ToByte((1 << D4c)));
            Task.Delay(5).Wait();
            CurrentText.Clear();
        }

        /// <summary>
        /// Sends information to the LCD either data or commands.
        /// </summary>
        /// <param name="data">Information which is to be sent on LCD.</param>
        /// <param name="Rs">Rs=0 for Command or Rs = 1 for Data.</param>
        public void write(byte data, byte Rs)
        {
            pulseEnable(Convert.ToByte((data & 0xf0) | (Rs << RS)));
            pulseEnable(Convert.ToByte((data & 0x0f) << 4 | (Rs << RS)));
            //Task.Delay(5).Wait(); //In case of problem with displaying wrong characters uncomment this part
        }


        /// <summary>
        /// Skip to second line
        /// </summary>
        public void gotoSecondLine()
        {
            write(0xc0, Command_sendMode);
        }

        /// <summary>
        /// Prints text in two lines
        /// </summary>
        /// <param name="text">Text which shall be displayed.</param>
        /// <param name="charsMaxInLine">Determines the maximum chars on a line.</param>
        public void printInTwoLines(string text, int charsMaxInLine = 16)
        {
            string line1 = "", line2 = "";

            line1 = text.Substring(0, charsMaxInLine);
            line2 = text.Substring(charsMaxInLine);
            prints(line1);
            gotoSecondLine();
            CurrentText.AppendLine();
            prints(line2);
            CurrentTextPlainString = text;
        }

        /// <summary>
        /// Prints text in two lines
        /// </summary>
        /// <param name="text">Text which shall be displayed.</param>
        /// <param name="charsMaxInLine">Determines the maximum chars on a line.</param>
        public void printInTwoLines(string textLine1, string textLine2)
        {
            prints(textLine1);
            gotoSecondLine();
            prints(textLine2);
        }



        /// <summary>
        /// Method wrapper for manual text-scrolling.
        /// </summary>
        /// <param name="text">Text to use for scrolling.</param>
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
        /// Print string to LCD display. Clears display before.
        /// </summary>
        /// <param name="s">String to print.</param>
        public void writeToLCD(string s)
        {
            clrscr();
            prints(s);
        }

        /// <summary>
        /// Prints a string onto display. Uses printc.
        /// </summary>
        /// <param name="text">Text which shall be displayed.</param>
        public void prints(string text)
        {
            foreach (char c in text)
            {
                this.printc(c);
                CurrentText.Append(c); // append string to variable that you can read later on when delivering status information
            }
        }

        /// <summary>
        /// Prints a character onto display.
        /// </summary>
        /// <param name="letter">Character which shall be displayed.</param>
        private void printc(char letter)
        {
            try
            {
                write(Convert.ToByte(letter), Data_sendMode);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Goto X and Y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void gotoxy(byte x, byte y)
        {
            write(Convert.ToByte(x | _LineAddress[y] | (1 << LCD_WRITE)), Command_sendMode);
        }

        /// <summary>
        /// Reset the LCD (clear its screen).
        /// </summary>
        public void resetLCD()
        {
            initiateLCD();
        }

        /// <summary>
        /// Save custom symbol to CGRAM.
        /// </summary>
        /// <param name="data">Data to store.</param>
        /// <param name="address">Address to store at.</param>
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
        /// Print custom symbol.
        /// </summary>
        /// <param name="address">The address where the symbol is located at.</param>
        public void printSymbol(byte address)
        {
            write(address, Data_sendMode);
        }

        /// <summary>
        /// Shift the display to the right.
        /// </summary>
        public void shiftDisplayRight()
        {
            write(Convert.ToByte(0x18), Command_sendMode);
        }

        /// <summary>
        /// Shift the display to the left.
        /// </summary>
        public void shiftDisplayLeft()
        {
            write(Convert.ToByte(0x1C), Command_sendMode);
        }

        /// <summary>
        /// Method wrapper for automated shifting. Stops once a canellation request is perceived.
        /// </summary>
        public void autoShift()
        {
            int counter = 0;
            bool toggle = true;
            while (!this._cts.IsCancellationRequested)
            {
                if (toggle == true)
                {
                    counter++;
                    this.shiftDisplayRight();
                }
                else
                {
                    this.shiftDisplayLeft();
                    counter--;
                }
                if (counter == 9)
                {
                    toggle = false;
                }
                else if (counter == 0)
                {
                    toggle = true;
                }
                Task.Delay(300).Wait();
            }
        }

        /// <summary>
        /// Method to cancel shifting by sending a cancellation request. Resets the cursor.
        /// </summary>
        public void cancelShifting()
        {
            if (this.isShifting())
            {
                this._cts.Cancel();
                Task.Delay(300).Wait();
                this.shifting = false;
                this.resetCursor();
            }
        }

        /// <summary>
        /// Reset the cursor and display-shift by resetting the DDRAM.
        /// </summary>
        public void resetCursor()
        {
            write(0x1, Command_sendMode);
        }

        /// <summary>
        /// Getter for status of shifting.
        /// </summary>
        /// <returns>Flag to signal shifting.</returns>
        public bool isShifting()
        {
            return this.shifting;
        }

        /// <summary>
        /// Wrapper to start a threaded shifting.
        /// </summary>
        public void startShifting()
        {
            cancelShifting();

            if (this.isShifting()) return;
            _cts = new CancellationTokenSource();
            try
            {
                Task scrollTextTask = Task.Factory.StartNew(autoShift);
            }
            catch (OperationCanceledException e)
            {
                Debug.WriteLine(e.Message);
            }
            this.shifting = true;
        }

        /// <summary>
        /// Switches the Backlight of the LCD to the wished target state
        /// </summary>
        /// <param name="targetState">0x01 for ON state and 0x00 for OFF state</param>
        public void switchBacklightTo(byte targetState)
        {
            if (!(targetState == 0x01 || targetState == 0x00)) throw new ArgumentException("Backlight State can only be 0x01 or 0x00");

            backLight = targetState;
            write(targetState, 0);
        }
    }
}