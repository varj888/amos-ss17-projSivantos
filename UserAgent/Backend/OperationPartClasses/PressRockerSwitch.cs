using System;
using System.Diagnostics;
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
        /// <param name="rsw">expected "up" ord "down"</param>
        /// <param name="durationCategorie">expected string defined in <see cref="DurationConfig"/></param>
        /// <returns>content of rsw+(duration) </returns>
        public string PressRockerSwitch(string rsw, string durationCategorie)
        {
            if (rsw == null || durationCategorie == null)
            {
                throw new Exception("Received invalid paremeterlist");
            }

            int duration = DurationConfig.getDuration(durationCategorie);
            Debug.WriteLine("\n Execute {0} with Parameter {3} duration {1}({2}) \n", this.GetType().Name, durationCategorie, duration, rsw);

            UInt16 pushButton_Pin;

            if (rsw == "down")
            {
                pushButton_Pin = GpioMap.rockerSwitchDownPin;
            }
            else if (rsw == "up")
            {
                pushButton_Pin = GpioMap.rockerSwitchUpPin;
            }
            else
            {
                throw new Exception("Invalid Rockerswitch submitted");
            }

            GPIOinterface.activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            GPIOinterface.deactivatePin(pushButton_Pin);

            return rsw + "(" + duration + ")";
        }
    }
}
