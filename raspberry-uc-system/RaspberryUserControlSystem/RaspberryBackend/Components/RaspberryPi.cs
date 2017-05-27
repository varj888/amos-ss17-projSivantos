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
        private GPIOinterface _gpioInterface;
        private LCD _lcdDisplay;
        private Potentiometer _potentiometer;


        public static RaspberryPi Instance
        {
            get { return _instance; }
        }

        public GPIOinterface GpioInterface
        {
            get { return _gpioInterface; }

        }

        public LCD LcdDisplay
        {
            get { return _lcdDisplay; }

        }

        public Potentiometer Potentiometer
        {
            get { return _potentiometer; }

        }

        private RaspberryPi() { }


        public void initialize()
        {
            if (_gpioInterface != null)
            {
                _gpioInterface = new GPIOinterface();
                _lcdDisplay = new LCD();
                _potentiometer = new Potentiometer();
            }

        }

        public void initialize(GPIOinterface gPIOinterface, LCD lCD, Potentiometer potentiometer)
        {
            if (_gpioInterface != null)
            {
                _gpioInterface = new GPIOinterface();
                _lcdDisplay = new LCD();
                _potentiometer = new Potentiometer();
            }

        }




    }
}
