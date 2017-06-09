using System;
using System.Collections.Generic;
using System.Reflection;

namespace RaspberryBackend
{
    /// <summary>
    /// Software representation of the RaspberryPi. It contains all component representations which are phyisical connected to the Rpi.
    /// To Add new Hadware Components add the corrosponding instance field and create/initialize it in initialize()
    /// </summary>
    public partial class RaspberryPi
    {
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
        public readonly GPIOinterface GPIOinterface;
        public readonly LCD LCD;
        public readonly Potentiometer Potentiometer;
        public readonly Multiplexer Multiplexer;
        public readonly ADConverter ADConverter;

        /// <summary>
        /// Default initialization of the Raspberry Pi. It initialize the preconfigured Hardware of the Raspberry Pi. To add aditional hardware, just put it as a new parameter.
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

        /// <summary>
        /// Customized initialization of the Raspberry Pi. It initialize the desired Hardware configuration of the Raspberry Pi.
        /// </summary>
        /// <param name="hwComponents">
        /// Hardware Componens which shall be connected to the Raspi.
        /// Enter as many components as desired devided with ','. e.g. (HWComponent one, HWComponent two)
        /// </param>
        public void initialize(params HWComponent[] hwComponents)
        {
            foreach (HWComponent hwComponent in hwComponents)
            {
                System.Diagnostics.Debug.WriteLine("Add new Hardware to Pi: " + hwComponent.GetType().Name);
                addToRasPi(hwComponent);

                initializeClassInstanceField(hwComponent);
            }

            initializeHWComponents();

            _initialized = true;
        }

        /// <summary>
        /// Return whether raspberrypi and it's hardware components are initialized
        /// </summary>
        /// <returns></returns>
        public Boolean isInitialized()
        {
            return _initialized & hwComponentsInitialized();
        }

        /// <summary>
        /// Deletes thecurrent Hardware Configuration of the Raspberry Pi. For now it is used for Testing.
        /// </summary>
        public void reset()
        {
            _hwComponents = new Dictionary<String, HWComponent>();
            _initialized = false;
        }

        private void addToRasPi(HWComponent hwComponent)
        {
            _hwComponents.Add(hwComponent.GetType().Name, hwComponent);
            _hwComponents.ToString();
        }

        private bool hwComponentsInitialized()
        {
            foreach (var hwComponent in _hwComponents.Values)
            {
                if (!hwComponent.isInitialized())
                {
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

                    System.Diagnostics.Debug.WriteLine(hwcomponent.GetType().Name + " initiated.");
                }

                if (hwComponentsInitialized())
                {
                    displayIPAdressOnLCD();
                    Multiplexer.setResetPin(GPIOinterface.getPin(18));
                }
            }
        }

        //initiates a declared instance field in the Raspberry Pi Class
        private void initializeClassInstanceField(HWComponent hwComponent)
        {
            String instanceFieldName = hwComponent.GetType().Name;
            Type rasPiClassType = this.GetType();
            FieldInfo classInstanceField = rasPiClassType.GetField(instanceFieldName);

            if (classInstanceField != null)
            {
                var fieldValue = Convert.ChangeType(hwComponent, hwComponent.GetType());
                classInstanceField.SetValue(this, fieldValue);
            }
        }
    }
}
