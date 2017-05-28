using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the RaspberryPi. It contains all component representations which are phyisical connected to the Rpi. 
    /// </summary>
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

        /// <summary>
        /// Default initialization of the Raspberry Pi. 
        /// Note: It initializes only one time once the gpioInterface is initialized. 
        /// </summary>
        public void initialize()
        {
            if (_gpioInterface == null)
            {
                _gpioInterface = new GPIOinterface();
                _lcdDisplay = new LCD();
                _potentiometer = new Potentiometer();
            }

        }
        /// <summary>
        /// Custome initialization of the Raspberry Pi. e.g for Test purposes
        /// </summary>
        /// <param name="gPIOinterface"> Instance of the Gpio Interface</param>
        /// <param name="lCD"> I2C Lcd Display instance</param>
        /// <param name="potentiometer">instance of the MCP4018 chip</param>
        public void initialize(GPIOinterface gPIOinterface, LCD lCD, Potentiometer potentiometer)
        {
            if (_gpioInterface == null)
            {
                _gpioInterface = gPIOinterface;
                _lcdDisplay = lCD;
                _potentiometer = potentiometer;
            }

        }

        /// <summary>
        /// Resets the single instance of the Raspberry PI representation. For now it is used for Testing. 
        /// </summary>
        public void reset()
        {
            _gpioInterface = null;
            _lcdDisplay = null;
            _potentiometer = null;
        }



    }
}
