namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// Hardware Components of the RasPi

        public GPIOinterface GPIOinterface;
       // public GPIOinterface GPIOinterface { get => hwComponentsInitialized() ? (GPIOinterface)_hwComponents[typeof(GPIOinterface).Name] : null; }
        public LCD LCD { get => hwComponentsInitialized() ? (LCD)_hwComponents[typeof(LCD).Name] : null; }
        public Potentiometer Potentiometer { get => hwComponentsInitialized() ? (Potentiometer)_hwComponents[typeof(Potentiometer).Name] : null; }
        public Multiplexer Multiplexer { get => hwComponentsInitialized() ? (Multiplexer)_hwComponents[typeof(Multiplexer).Name] : null; }
        public ADCDAC ADCDAC { get => hwComponentsInitialized() ? (ADCDAC)_hwComponents[typeof(ADCDAC).Name] : null; }

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

                if (hwComponentsInitialized())
                {
                    displayIPAdressOnLCD();
                    Multiplexer.setResetPin(GPIOinterface.getPin(18));
                }
            }
        }
    }
}
