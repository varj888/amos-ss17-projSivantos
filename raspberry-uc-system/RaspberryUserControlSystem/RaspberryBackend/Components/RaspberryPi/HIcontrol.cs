namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// usage example:
        /// turns ADConverter on with
        /// 3.3V to the channel 2 with DAC voltage 1.5
        /// </summary>
        public void turnHI_on(double voltage)
        {
            ADCDAC.setDACVoltage(voltage);
        }


        /// <summary>
        /// Set the potentiometer to a value from 0000 0000 - 0111 1111
        /// </summary>
        /// <param name="data"></param>
        public void setAnalogVolume(byte[] data)
        {
            Potentiometer.write(data);
        }

        /// <summary>
        /// Connect pins x to y on the multiplexer. Right now this is the same as _multiplexer.connectPins except no checks
        /// are performed on the input parameters. Eventually we can check for success right here.
        /// </summary>
        /// <param name="xPin"></param>
        /// <param name="yPin">/param>
        public void connectPins(int xPin, int yPin)
        {
            Multiplexer.connectPins(xPin, yPin);
        }
    }

}
