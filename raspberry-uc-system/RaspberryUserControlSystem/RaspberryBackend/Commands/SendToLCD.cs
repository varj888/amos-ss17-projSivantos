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
    class SendToLCD : Command
    {
        private const int charsMaxInLine = 16;
        private CancellationTokenSource _cancelSendToLCD;
        private int _scrollSpeed = 1;
        private const byte Command_sendMode = 0;
        private const byte Data_sendMode = 1;

        private LCD lcd = RaspberryPi.Instance.LcdDisplay;

        public SendToLCD(RaspberryPi raspberryPi) : base(raspberryPi)
        {

            RequestController.Instance.addRequestedCommand(this.GetType().Name, this);
        }

        public override void execute(object asynchData)
        {

            //AsynchData data = (AsynchData)asynchData;
            //_cancelSendToLCD = (CancellationTokenSource)data.cancellationToken;
            //_scrollSpeed = (int)data.attribute;
            //string text = (string)data.parameter;
            Debug.WriteLine((string)asynchData);
            string text = (string)asynchData;



            lcd.clrscr();

            if (text.Length <= charsMaxInLine)
            {
                prints(text);
            }

            else if (text.Length > charsMaxInLine && text.Length <= 2 * charsMaxInLine)
            {
                //Task.Factory.StartNew(() => printInTwoLines(text, charsMaxInLine));

                printInTwoLines(text, charsMaxInLine);
            }
            else
            {
                try
                {
                    //Task scrollTextTask = Task.Factory.StartNew(() => scrollText(text, _scrollSpeed), _cancelSendToLCD.Token);
                    scrollText(text, _scrollSpeed);
                }
                catch (OperationCanceledException)
                {

                }

            }
        }

        private void printInTwoLines(string text, int charsMaxInLine)
        {
            string line1 = "", line2 = "";

            line1 = text.Substring(0, charsMaxInLine);
            line2 = text.Substring(charsMaxInLine);


            prints(line1);
            gotoSecondLine();
            prints(line2);
        }



        private void scrollText(string text, int countChars)
        {
            int maxChars = 16;

            lcd.clrscr();

            for (int i = 0; i <= text.Length - maxChars; i = i + countChars < text.Length ? i + countChars : text.Length)
            {
                if (_cancelSendToLCD.IsCancellationRequested)
                {
                    return;
                }

                Task.Delay(200).Wait();
                lcd.clrscr();
                for (int j = i; j < maxChars + i && j < text.Length; j++)
                {
                    printc(text.ElementAt(j));
                }
                Task.Delay(200).Wait();
            }

        }



        /**
        * skip to second line
        **/
        private void gotoSecondLine()
        {
            lcd.write(0xc0, Command_sendMode);

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

        /**
      * Print single character onto display
      **/
        public void printc(char letter)
        {
            try
            {
                lcd.write(Convert.ToByte(letter), Data_sendMode);
            }
            catch (Exception e)
            {

            }
        }
    }
}
