namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// Print string to LCD display
        /// </summary>
        /// <param name="s"></param>
        public void writeToLCD(string s)
        {
            LCD.clrscr();
            LCD.prints(s);
        }

        /// <summary>
        /// Print two lines to LCD
        /// </summary>
        /// <param name="s"></param>
        public void writeToLCDTwoLines(string s)
        {
            int maxCharLCD = 16;
            LCD.printInTwoLines(s, maxCharLCD);
        }

        /// <summary>
        /// Reset the LCD (clear it's screen)
        /// </summary>
        public void resetLCD()
        {
            LCD.initiate();
        }

        /// <summary>
        /// Set state for background in LCD. Will want to switch to toggle
        /// </summary>
        /// <param name="targetState"></param>
        public void setLCDBackgroundState(byte targetState)
        {
            LCD.backLight = targetState;
            LCD.write(targetState, 1);
        }
    }
}
