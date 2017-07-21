using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It lights the LED on if the Request-Parameter is 1 and off if 0.
    /// </summary>
    public partial class Operation
    {
        private bool _lastSave = true;

        /// <summary>
        /// This method should always be used to save the Hi config so different save Task do not interfere with each other. It only saves after the las save Task is complete.
        /// </summary>
        /// <returns></returns>
        public async Task saveHiConfig()
        {
            while (!_lastSave) { }
            _lastSave = false;

            await Task.Run(save);
        }

        private async Task save()
        {
            _lastSave = await StorageHandler<Hi>.Save(StorageCfgs.FileName_HiCfg, StorageCfgs.Hi);
        }
    }
}

