using System;
using CommonFiles.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

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
        private Multiplexer _multiplexer;
        private ADCDAC _daConverter;

        private int maxCharLCD = 16;
        private Boolean _initialized = false;

        public static RaspberryPi Instance
        {
            get { return _instance; }
        }

        public object GpioInterface { get; internal set; }
        public object LcdDisplay { get; internal set; }
        public object Potentiometer { get; internal set; }

        private RaspberryPi() { }

        /// <summary>
        /// Default initialization of the Raspberry Pi.
        /// Note: It initializes only one time once the gpioInterface is initialized.
        /// </summary>
        public void initialize()
        {
            _gpioInterface = new GPIOinterface();
            _gpioInterface.initPins();

            _lcdDisplay = new LCD();
            _lcdDisplay.initiateLCD();

            _potentiometer = new Potentiometer();

            _daConverter = new ADCDAC();
            _daConverter.init();

            _multiplexer = new Multiplexer(_gpioInterface.getPin(18));

            _initialized = true;

            _lcdDisplay.prints(Others.GetIpAddress());
        }

        /// <summary>
        /// usage example:
        /// turns ADConverter on with
        /// 3.3V to the channel 2 with DAC voltage 1.5
        /// </summary>
        public void turnHI_on(double voltage)
        {
            _daConverter.setDACVoltage(voltage);
        }

        /// <summary>
        /// Resets the single instance of the Raspberry PI representation. For now it is used for Testing.
        /// </summary>
        public void reset()
        {
            _gpioInterface = null;
            _lcdDisplay = null;
            _potentiometer = null;
            _daConverter = null;
            _multiplexer = null;
            _initialized = false;
        }

        /// <summary>
        /// Set the potentiometer to a value from 0000 0000 - 0111 1111
        /// </summary>
        /// <param name="data"></param>
        public void setHIPower(byte[] data)
        {
            _potentiometer.write(data);
        }

        /// <summary>
        /// Print string to LCD display
        /// </summary>
        /// <param name="s"></param>
        public void writeToLCD(string s)
        {
            _lcdDisplay.clrscr();
            _lcdDisplay.prints(s);
        }

        /// <summary>
        /// Print two lines to LCD
        /// </summary>
        /// <param name="s"></param>
        public void writeToLCDTwoLines(string s)
        {
            _lcdDisplay.printInTwoLines(s, maxCharLCD);
        }

        /// <summary>
        /// Reset the LCD (clear it's screen)
        /// </summary>
        public void resetLCD()
        {
            _lcdDisplay.clrscr();
        }

        /// <summary>
        /// Set state for background in LCD. Will want to switch to toggle
        /// </summary>
        /// <param name="targetState"></param>
        public void setLCDBackgroundState(byte targetState)
        {
            _lcdDisplay.backLight = targetState;
            _lcdDisplay.write(targetState, 1);
        }

        /// <summary>
        /// Set GPIO pin to 1
        /// </summary>
        /// <param name="id"></param>
        public void activatePin(UInt16 id)
        {
            _gpioInterface.setToOutput(id);
            _gpioInterface.writePin(id, 1);
        }

        /// <summary>
        /// Reset GPIO pin by settting to 0
        /// </summary>
        /// <param name="id"></param>
        public void deactivatePin(UInt16 id)
        {
            _gpioInterface.setToOutput(id);
            _gpioInterface.writePin(id, 0);
        }

        /// <summary>
        /// Read pin from GPIOInterface
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string readPin(UInt16 id)
        {
            return _gpioInterface.readPin(id);
        }

        /// <summary>
        /// Return whether raspberrypi and it's hardware components are initialized
        /// </summary>
        /// <returns></returns>
        public Boolean isInitialized()
        {
            return _initialized & _gpioInterface.isInitialized() & _lcdDisplay.isInitialized() & _potentiometer.isInitialized();
        }

        /// <summary>
        /// Connect pins x to y on the multiplexer. Right now this is the same as _multiplexer.connectPins except no checks
        /// are performed on the input parameters. Eventually we can check for success right here.
        /// </summary>
        /// <param name="xi"></param>
        /// <param name="yi">/param>
        public void connectPins(int xi, int yi)
        {
            _multiplexer.connectPins(xi, yi);
        }
    }
}
