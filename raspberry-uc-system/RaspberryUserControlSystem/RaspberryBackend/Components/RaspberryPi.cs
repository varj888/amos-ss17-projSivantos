using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    public class RaspberryPi
    {

        private static readonly RaspberryPi _instance = new RaspberryPi();
        private GPIOinterface _gpioInterface = new GPIOinterface();
        private DisplayI2C _lcdDisplay = new DisplayI2C(new lcdConfig());
        private Potentiometer potentiometer = new Potentiometer();


        public static RaspberryPi Instance
        {
            get { return _instance; }
        }

        public GPIOinterface GpioInterface
        {
            get { return _gpioInterface; }

        }

        public DisplayI2C lcdDisplay
        {
            get { return _lcdDisplay; }

        }

        private RaspberryPi() { }
    }
}
