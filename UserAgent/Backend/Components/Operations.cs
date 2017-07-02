using System;
using System.Collections.Generic;
using System.Reflection;

namespace RaspberryBackend
{
    /// <summary>
    /// Super class for each Operation. Will be initialized by RaspberryPi
    /// </summary>
    public partial class Operation
    {
        /// <summary>
        /// For now this is needed because some operation check the Initialization state of the RasPi.
        /// </summary>
        public RaspberryPi RasPi { get; } = RaspberryPi.Instance;

        /// Hardware Components of the RasPi as Instance Fields.
        /// Note: Both Type and declared name need to be identical otherwise automatation of initialization fails.
        public readonly GPIOinterface GPIOinterface;
        public readonly LCD LCD;
        public readonly Potentiometer Potentiometer;
        public readonly Multiplexer Multiplexer;
        public readonly ADConverter ADConverter;


        public Operation() { }

        /// <summary>
        /// Initializes the Operation Class by initializing corresponding Hardware fields which will be used
        /// from an Operation
        /// </summary>
        /// <param name="hwComponents"> Hardware Components which will be used to initialize the fields</param>
        public Operation(Dictionary<string, HWComponent> hwComponents)
        {
            foreach (HWComponent hwComponent in hwComponents.Values)
            {
                initializeClassInstanceField(hwComponent);
            }
        }

        //initiates a declared instance field in the Raspberry Pi Class
        private void initializeClassInstanceField(HWComponent hwComponent)
        {
            string instanceFieldName = hwComponent.GetType().Name;
            Type rasPiClassType = this.GetType();
            FieldInfo classInstanceField = rasPiClassType.GetField(instanceFieldName);

            if (classInstanceField != null && classInstanceField.GetValue(this) == null)
            {
                var fieldValue = Convert.ChangeType(hwComponent, hwComponent.GetType());
                classInstanceField.SetValue(this, fieldValue);
            }
        }
    }
}
