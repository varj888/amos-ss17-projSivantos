namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// Hardware Components of the RasPi
        public GPIOinterface GPIOinterface { get => _initialized ? (GPIOinterface)_hwComponents[typeof(GPIOinterface).Name] : null; }
        public LCD LCD { get => _initialized ? (LCD)_hwComponents[typeof(LCD).Name] : null; }
        public Potentiometer Potentiometer { get => _initialized ? (Potentiometer)_hwComponents[typeof(Potentiometer).Name] : null; }
        public Multiplexer Multiplexer { get => _initialized ? (Multiplexer)_hwComponents[typeof(Multiplexer).Name] : null; }
        public ADCDAC ADCDAC { get => _initialized ? (ADCDAC)_hwComponents[typeof(ADCDAC).Name] : null; }

        //initialization of each Hardware Component
        private void initializeHWComponents()
        {
            if (!testMode)
            {
                foreach (HWComponent hwcomponent in _hwComponents.Values)
                {
                    System.Threading.Tasks.Task.Delay(250).Wait();
                    hwcomponent.initiate();
                }

                _initialized = true;

                displayIPAdressOnLCD();
                Multiplexer.setResetPin(GPIOinterface.getPin(18));
            }
        }
    }
}
