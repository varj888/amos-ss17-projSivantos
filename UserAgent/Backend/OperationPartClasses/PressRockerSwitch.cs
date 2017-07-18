using System;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It simulates a rockerswitch.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Execute presssing one rockerswitch
        /// </summary>
        /// <param name="parameter">Expects an string-Array containing id = ["up"|"down"] and Duration-Categorie <see cref="DurationConfig"</param>
        /// <returns>The provided parameterlist represented as string</returns>
        public string PressRockerSwitch(string[] param)
        {

            if (param.Length != 2)
            {
                throw new Exception("Received invalid paremeterlist");
            }

            string rsw = param[0];
            int duration = DurationConfig.getDuration(param[1]);

            if (rsw != "up" | rsw != "down")
            {
                throw new Exception("Invalid Rockerswitch submitted");
            }

            UInt16 pushButton_Pin;

            if (rsw == "down")
            {
                pushButton_Pin = GpioMap.rockerSwitchDownPin;
            }
            else
            {
                pushButton_Pin = GpioMap.rockerSwitchUpPin;
            }

            GPIOinterface.activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            GPIOinterface.deactivatePin(pushButton_Pin);

            return param.ToString();
        }
    }
}
