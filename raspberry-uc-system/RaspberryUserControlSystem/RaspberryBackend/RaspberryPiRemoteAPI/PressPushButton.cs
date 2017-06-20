using CommonFiles.TransferObjects;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to read a spefic gpio pin of the RaspberryPi.
    /// </summary>
    public partial class RaspberryPi
    {


        /// <summary>
        /// Executes the Command PressPushButton. For this the respective Pushbutton is activated and deactivated after
        /// a user-provided duration using the raspberry-class methods.
        /// </summary>
        /// <param name="parameter">UInt16 Duration</param>
        /// <returns>The provided duration as string.</returns>
        public string PressPushButton(int duration)
        {
            activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            deactivatePin(pushButton_Pin);

            return duration.ToString();
        }
    }
}
