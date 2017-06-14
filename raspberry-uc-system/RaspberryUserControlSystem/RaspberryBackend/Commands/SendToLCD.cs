using System;
using System.Threading;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it sends date to a LCD through I2C.
    /// </summary>
    class SendToLCD : Command
    {
        private const int charsMaxInLine = 16;
        public SendToLCD(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        ///  executes the Command SendToLCD in dependency of the parsed parameter
        /// </summary>
        /// <param name="parameter">either a text:string which is to be printed on lcd
        /// or a #command:string e.g #reset to clear the display
        /// and terminate all tasks related to a previous call
        /// </param>
        public override void executeAsync(object[] parameters)
        {
            object parameter = parameters[0];
            string text = (string)parameter;
            if (text == "#reset")
            {
                System.Diagnostics.Debug.WriteLine("Resetting display");
                RaspberryPi.resetLCD();
                return;
            }
            if (text.Length <= charsMaxInLine)
            {
                RaspberryPi.writeToLCD(text);
            }
            else if (text.Length <= 2 * charsMaxInLine)
            {
                RaspberryPi.writeToLCDTwoLines(text);
            }
            else
            {
                throw new ArgumentException("Text too long to print on LCD");
            }
        }
    }
}
