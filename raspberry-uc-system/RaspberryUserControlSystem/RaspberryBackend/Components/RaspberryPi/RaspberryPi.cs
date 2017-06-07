using System;
using System.Collections.Generic;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the RaspberryPi. It contains all component representations which are phyisical connected to the Rpi.
    /// </summary>
    public partial class RaspberryPi
    {
        private Dictionary<String, HWComponent> _hwComponents;

        private static readonly RaspberryPi _instance = new RaspberryPi();
        private bool _initialized = false;
        private bool defaultMode = false;

        public static RaspberryPi Instance
        {
            get { return _instance; }
        }

        private RaspberryPi() { }

        /// <summary>
        /// Default initialization of the Raspberry Pi.
        /// </summary>
        public void initialize()
        {
            defaultMode = true;
            initialize(
                new GPIOinterface(),
                new LCD(),
                new Potentiometer(),
                new Multiplexer(GPIOinterface.getPin(18)),
                new ADCDAC()
                );
        }

        /// <summary>
        /// Customized initialization of the Raspberry Pi.
        /// </summary>
        /// <param name="hwComponents">
        /// Hardware Componens which shall be connected to the Raspi.
        /// Enter as many components as desired devided with ','. e.g. (HWComponent one, HWComponent two)
        /// </param>
        public void initialize(params HWComponent[] hwComponents)
        {
            _hwComponents = new Dictionary<String, HWComponent>();

            foreach (HWComponent hwComponent in hwComponents)
            {
                addToRasPi(hwComponent);
            }

            initializeHWComponents();

            _initialized = true;
        }

        private void addToRasPi(HWComponent hwComponent)
        {
            _hwComponents.Add(hwComponent.GetType().Name, hwComponent);
            _hwComponents.ToString();
        }

        /// <summary>
        /// Resets the single instance of the Raspberry PI representation. For now it is used for Testing.
        /// </summary>
        public void reset()
        {
            _hwComponents = new Dictionary<String, HWComponent>();
        }

        /// <summary>
        /// Return whether raspberrypi and it's hardware components are initialized
        /// </summary>
        /// <returns></returns>
        public Boolean isInitialized()
        {
            return _initialized & GPIOinterface.isInitialized() & LCD.isInitialized() & Potentiometer.isInitialized();
        }
    }
}
