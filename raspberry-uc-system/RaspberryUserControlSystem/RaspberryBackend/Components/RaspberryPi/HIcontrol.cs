using System.Collections.Generic;
using System.Diagnostics;

namespace RaspberryBackend
{
    public partial class RaspberryPi
    {
        /// <summary>
        /// Dictionary to represent possible receivers to detect with their respective resistance. Refer to
        /// https://drive.google.com/drive/folders/0BzaNmZTttJK4N1dmUWt0VFRzU3c for more information.
        /// </summary>
        private Dictionary<string, double> deviceResistanceMap = new Dictionary<string, double>
        {
            {"Small right", 0.787},
            {"Small left", 1.540},
            {"Medium right", 2.490},
            {"Medium Left", 3.480},
            {"Power right", 4.870},
            {"Power Left", 6.490},
            {"High Power right", 8.660},
            {"High Power left", 11.000},
            {"Defective", 133.700},
            {"No receiver", 200.0},
        };

        /// <summary>
        /// Sets the DACVoltage output in channel 1 to a desired voltage
        /// </summary>
        /// <param name="voltage"></param>
        public void turnHI_on(double voltage)
        {
            ADConverter.setDACVoltage1(voltage);
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

        /// <summary>
        /// This method sets the voltage for output 1 on the ADCDAC Pi Zero. The formula for setting the voltage was provided
        /// by our partners: Vx = [ Vbat / ( 1000 + Rx ) ] * Rx whereas Rx is being provided from a set list of receivers in
        /// combination with its respective resistance. The list must be contained in this class and filled accordingly, to ensure
        /// the frontend/ API user will not send invalid values possibly resulting in too high current.
        /// </summary>
        /// <param name="device">The device provided as a string used to look up its respective voltage</param>
        public void setARDVoltage(string device)
        {
            if (!this.deviceResistanceMap.ContainsKey(device))
            {
                Debug.Write("Invalid device provided!");
                return;
            }
            double resistance = deviceResistanceMap[device];
            ADConverter.setDACVoltage2((ADConverter.getDACVoltage() / (1000.00 + resistance)) * resistance);
        }
    }
}
