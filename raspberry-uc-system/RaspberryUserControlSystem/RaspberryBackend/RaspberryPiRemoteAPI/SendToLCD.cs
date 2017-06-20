using CommonFiles.TransferObjects;
using System;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it sends date to a LCD through I2C.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Executes the Command SendToLCD in dependency of the parsed parameter
        /// </summary>
        /// <param name="parameter">Either a text:string which is to be printed on lcd
        /// or a #command:string e.g #reset to clear the display
        /// and terminate all tasks related to a previous call
        /// </param>
        /// <returns>The provided text or a status information.</returns>
        public string SendToLCD(string text)
        {
            const int charsMaxInLine = 16;

            if (text == "#reset")
            {
                resetLCD();
                return "Reset display";
            }
            if (text.Length <= charsMaxInLine)
            {
                writeToLCD(text);
            }
            else if (text.Length <= 2 * charsMaxInLine)
            {
                writeToLCDTwoLines(text);
            }
            else
            {
                throw new Exception("Text too long to print on LCD");
            }

            return text;
        }
    }
}
