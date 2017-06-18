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
        /// executes the Command ReadPin
        /// </summary>
        /// <param name="parameter">UInt16 Duration</param>
        public Result PressPushButton(int duration)
        {
            activatePin(pushButton_Pin);
            Task.Delay(duration).Wait();
            deactivatePin(pushButton_Pin);

            return new Result(true, this.GetType().Name, duration.ToString());
        }
    }
}
