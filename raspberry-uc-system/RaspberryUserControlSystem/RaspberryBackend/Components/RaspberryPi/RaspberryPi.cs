using CommonFiles.Networking;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the RaspberryPi. It contains all component representations which are phyisical connected to the Rpi.
    /// To Add new Hadware Components, create/initialize it in initialize(). If it is desired, aditionally declare the corrosponding instance field which will be automatically initialised.
    /// For Testing without connected Hardware Components, use the overloaded initialized(params HWComponents[] hwComponents) method to initialize the Raspberry Pi.
    /// </summary>
    public partial class RaspberryPi
    {
        protected UInt16 pushButton_Pin = 26;
        protected UInt16 rockerSwitch_Pin_0 = 20;
        protected UInt16 rockerSwitch_Pin_1 = 21;
        protected UInt16 audioShoe_Pin = 19;
        protected UInt16 teleCoil_Pin = 13;
        protected UInt16 muxerResetPin = 18;
        private ServerSkeleton skeleton = null;

        //Single location for all Hardware Components
        private Dictionary<String, HWComponent> _hwComponents = new Dictionary<String, HWComponent>();

        //Singleton pattern
        private static readonly RaspberryPi _instance = new RaspberryPi();
        private RaspberryPi() { }
        public static RaspberryPi Instance { get => _instance; }

        //flags for robustness and testing
        private bool _initialized = false;
        private bool testMode = true;

        /// Hardware Components of the RasPi as Instance Fields.
        /// Note: Both Type and declared name needs to be identical otherwise automatation of initialization fails.
        public readonly GPIOinterface GPIOinterface;
        public readonly LCD LCD;
        public readonly Potentiometer Potentiometer;
        public readonly Multiplexer Multiplexer;
        public readonly ADConverter ADConverter;

        /// <summary>
        /// Default initialization of the Raspberry Pi. It initialize the preconfigured Hardware of the RasPi.
        /// To add aditional hardware, just insert a new parameter in the initialize(..) call eg. initialize(... , new HWComponent).
        /// To modify the Start-Up Configuration use aditionally <see cref="initiateStartUpConfiguration"/>.
        /// Note: See <seealso cref="initialize(HWComponent[])"/> for detailed insight of the RasPi's initialization process.
        /// </summary>
        public void initialize()
        {
            testMode = false;
            initialize(
                new GPIOinterface(),
                new LCD(),
                new Potentiometer(),
                new Multiplexer(),
                new ADConverter()
                );
        }

        private void initiateStartUpConfiguration()
        {
            Multiplexer.setResetPin(GPIOinterface.getPin(18));
            Multiplexer.setMultiplexerConfiguration("TestFamily", "TestModel");
        }

        /// <summary>
        /// Customized initialization of the Raspberry Pi. It initialize the desired Hardware and Start-Up configuration of the Raspberry Pi.
        /// </summary>
        /// <param name="hwComponents">
        /// Hardware Componens which shall be connected to the Raspi.
        /// Enter as many components as desired devided with ','. e.g. initialize(HWComponent one, HWComponent two);
        /// </param>
        public void initialize(params HWComponent[] hwComponents)
        {
            if (hwComponents != null)
            {
                foreach (HWComponent hwComponent in hwComponents)
                {
                    System.Diagnostics.Debug.WriteLine("Add new Hardware to Pi: " + hwComponent.GetType().Name);
                    addToRasPi(hwComponent);

                    initializeClassInstanceField(hwComponent);
                }

                initializeHWComponents();

                // Since the initialisation of Hardware is indipendent, the start-configuration of the RasPi which relise on them is seperated
                if (hwComponentsInitialized())
                {
                    initiateStartUpConfiguration();
                }
                else if (!testMode)
                {
                    throw new AggregateException("Hardware Components are (partly) not initialised thus the startconfiguration could not be initalised");
                }

                _initialized = true;
            }
            else
            {
                throw new AggregateException("The RasPi, at least, needs to be initialised with the Hardware Component <GPIOinterface>.");
            }
        }


        /// <summary>
        /// Return whether raspberrypi and it's hardware components are initialized
        /// </summary>
        /// <returns>True if each HWComponent and the Raspberry Pi is initialised. False if at least one HWComponent is not initialised</returns>
        public Boolean isInitialized()
        {
            return _initialized & hwComponentsInitialized();
        }

        /// <summary>
        /// Deletes the current Hardware Configuration of the Raspberry Pi. For now it is used for testing. Later it can be used for dynamically auto configurate the Raspberry Pi.
        /// </summary>
        public void reset()
        {
            resetClassInstanceField();
            _hwComponents = new Dictionary<String, HWComponent>();
            _initialized = false;
        }

        private void addToRasPi(HWComponent hwComponent)
        {
            string key = hwComponent.GetType().Name;
            if (!_hwComponents.ContainsKey(key)) _hwComponents.Add(key, hwComponent);
        }

        private bool hwComponentsInitialized()
        {
            foreach (var hwComponent in _hwComponents.Values)
            {
                if (!hwComponent.isInitialized())
                {
                    System.Diagnostics.Debug.WriteLine(hwComponent.GetType().Name + " is not initialised");
                    return false;
                }
            }
            return true;
        }

        //initialization of each Hardware Component
        private void initializeHWComponents()
        {
            if (!testMode)
            {
                foreach (HWComponent hwcomponent in _hwComponents.Values)
                {
                    System.Diagnostics.Debug.WriteLine("Initialize conneted Hardware : " + hwcomponent.GetType().Name);

                    System.Threading.Tasks.Task.Delay(250).Wait();
                    hwcomponent.initiate();

                    System.Diagnostics.Debug.WriteLine(hwcomponent.GetType().Name + " initalized.");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("System starts in Test-Mode. Hardware Components are not going to be connected/initialised.");
            }
        }

        //initiates a declared instance field in the Raspberry Pi Class
        private void initializeClassInstanceField(HWComponent hwComponent)
        {
            String instanceFieldName = hwComponent.GetType().Name;
            Type rasPiClassType = this.GetType();
            FieldInfo classInstanceField = rasPiClassType.GetField(instanceFieldName);

            if (classInstanceField != null && classInstanceField.GetValue(this) == null)
            {
                var fieldValue = Convert.ChangeType(hwComponent, hwComponent.GetType());
                classInstanceField.SetValue(this, fieldValue);
            }
        }

        //resets all Hardware related instance Fields. For a detailed call structure see initializeClassInstanceField(HWComponent)
        private void resetClassInstanceField()
        {
            foreach (var hwComponent in _hwComponents.Values)
            {
                this.GetType().GetField(hwComponent.GetType().Name).SetValue(this, null);
            }
        }

        public void setSkeleton(ServerSkeleton s)
        {
            this.skeleton = s;
        }
    }
}
