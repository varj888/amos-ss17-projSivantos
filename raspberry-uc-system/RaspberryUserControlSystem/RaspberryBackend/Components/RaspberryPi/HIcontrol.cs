namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// Sets the DACVoltage output in channel 1 to a desired voltage
        /// </summary>
        /// <param name="voltage"></param>
        public void turnHI_on(double voltage)
        {
            ADConverter.setDACVoltage(voltage);
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

        /// <summary>
        /// Send signal that an audio-shoe is connected by pulling up the respective pin as defined by documentation. Compare the
        /// circuit diagram for more details.
        /// </summary>
        public void setAudioShoe()
        {
            this.activatePin(this.audioShoe_Pin);
        }

        /// <summary>
        /// Send signal that an audio-shoe was disconnected by pulling down the respective pin as defined by documentation. Compare the
        /// circuit diagram for more details.
        /// </summary>
        public void unsetAudioShoe()
        {
            this.deactivatePin(this.audioShoe_Pin);
        }

        /// <summary>
        /// Send signal that a tele-coil is nearby by pulling up the respective pin as defined by documentation. Compare the
        /// circuit diagram for more details.
        /// </summary>
        public void setTeleCoil()
        {
            this.activatePin(this.teleCoil_Pin);
        }

        /// <summary>
        /// Send signal that a tele-coil was removed by pulling down the respective pin as defined by documentation. Compare the
        /// circuit diagram for more details.
        /// </summary>
        public void unsetTeleCoil()
        {
            this.deactivatePin(this.teleCoil_Pin);
        }

        /// <summary>
        /// Return current status of audio-shoe. Note that readPin returns the last written value, which does not necessarily
        /// represent the real status of a pin.
        /// </summary>
        /// <returns></returns>
        public string getAudioShoeStatus()
        {
            return this.readPin(this.audioShoe_Pin);
        }

        /// <summary>
        /// Return current status of tele-coil. Note that readPin returns the last written value, which does not neccessarily
        /// represent the real status of a ping.
        /// </summary>
        /// <returns></returns>
        public string getTeleCoilStatus()
        {
            return this.readPin(this.audioShoe_Pin);
        }
    }
}
