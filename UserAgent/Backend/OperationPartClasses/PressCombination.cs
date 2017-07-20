using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It activates a combination of pins.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Method can be used to trigger combinations as wished. For now it activats multiple pins at the same time.
        /// Currently we use 3 buttons: rockerswitch_down, rockerswitch_up and pushbutton.
        /// </summary>
        /// <param name="pb">"PB" to activate Pushbutton otherwise NULL</param>
        /// <param name="rsu">"RSU" to activate RockerSwitchUP otherwise NULL</param>
        /// <param name="rsd">"RSD" to activate RockerSwitchDown otherwise NULL</param>
        /// <param name="durationCategorie">expected string defined in <see cref="DurationConfig"/> </param>
        /// <returns>provided duration</returns>
        public string PressCombination(string pb, string rsu, string rsd, string durationCategorie)
        {
            int duration = DurationConfig.getDuration(durationCategorie);
            Debug.WriteLine("\n Execute {0} with Parameters PB={1}, RSD={2}, RSU={3} and duration {4}({5}) \n", this.GetType().Name, pb, rsd, rsu, durationCategorie, duration);

            if (pb == null && rsd == null && rsu == null && durationCategorie == null)
            {
                throw new Exception("Invalid parameterlist received");
            }

            if (rsd == "RSD" & rsu == "RSU")
            {
                throw new Exception("Tester tried to press both rockerswitches in combination");
            }

            if (pb == "PB")
            {
                GPIOinterface.activatePin(GpioMap.pushButton_Pin);
            }

            if (rsu == "RSU" & rsd == null)
            {
                GPIOinterface.activatePin(GpioMap.rockerSwitchUpPin);
            }

            if (rsd == "RSD" & rsu == null)
            {
                GPIOinterface.activatePin(GpioMap.rockerSwitchDownPin);
            }

            Task.Delay(duration).Wait();
            GPIOinterface.deactivatePin(GpioMap.pushButton_Pin);
            GPIOinterface.deactivatePin(GpioMap.rockerSwitchDownPin);
            GPIOinterface.deactivatePin(GpioMap.rockerSwitchUpPin);

            return duration.ToString();
        }
    }
}
