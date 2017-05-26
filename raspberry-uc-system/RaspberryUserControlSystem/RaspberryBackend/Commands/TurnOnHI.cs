using System;

namespace RaspberryBackend
{
    class TurnOnHI : Command
    {

        public TurnOnHI(RaspberryPi raspberryPi) : base(raspberryPi)
        {
            RequestController.Instance.addRequestedCommand("TurnOnHI", this);
        }

        /// <summary>
        /// execute the Command WritePin
        /// </summary>
        /// <param name="parameter">represents the GpioPin which shall be written on</param>
        public override void execute(Object parameter)
        {
            string requestedParameter = parameter.ToString();

            if (requestedParameter.Equals("1"))
            {
                //RaspberryPi.Potentiometer._potentiometer.Read();
                RaspberryPi.Potentiometer.turnOn();
            }
            else if (requestedParameter.Equals("0"))
            {
                RaspberryPi.Potentiometer.turnOff();
            }

        }

    }
}
