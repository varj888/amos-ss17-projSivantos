using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFiles.TransferObjects;
using System.Threading;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it sends date to a LCD through I2C. 
    /// </summary>
    class SendToLCD : Command
    {
        private const int charsMaxInLine = 16;
        private CancellationTokenSource _cancelSendToLCD;
        private int _scrollSpeed = 1;
        private const byte Command_sendMode = 0;
        private const byte Data_sendMode = 1;

        public SendToLCD(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        ///  executes the Command SendToLCD in dependency of the parsed parameter 
        /// </summary>
        /// <param name="parameter">either a text:string which is to be printed on lcd 
        /// or a #command:string e.g #cancel to clear the display 
        /// and terminate all tasks related to a previous call
        /// </param>
        public override void executeAsync(object parameter)
        {
            string text = (string)parameter;
            if(text == "#reset")
            {
                System.Diagnostics.Debug.WriteLine("Resetting display");
                RaspberryPi.resetLCD();
                return;
            }
            if (text.Length <= charsMaxInLine)
            {
                RaspberryPi.writeToLCD(text);
            } else if( text.Length <= 2*charsMaxInLine)
            {
                RaspberryPi.writeToLCDTwoLines(text);
            } else
            {
                System.Diagnostics.Debug.WriteLine("Text too long to print on LCD");
            }
        }
    }
}
