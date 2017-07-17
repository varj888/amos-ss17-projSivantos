using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to deactivate a tele-coil.
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// This method sets the voltage for output 1 on the ADCDAC Pi Zero. The formula for setting the voltage was provided
        /// by our partners: Vx = [ Vbat / ( 14 + Rx ) ] * Rx whereas Rx is being provided from a set list of receivers in
        /// combination with its respective resistance <seealso cref="ReceiverConfig.DeviceResistanceMap"/>. The list must be contained in this class and filled accordingly, to ensure
        /// the frontend/ API user will not send invalid values possibly resulting in too high current.
        /// </summary>
        /// <param name="device">The device provided as a string used to look up its respective voltage</param>
        /// <returns>The provided device.</returns>
        public string SetARDVoltage(string device)
        {
            if (!ReceiverConfig.DeviceResistanceMap.ContainsKey(device))
            {
                Debug.Write("Invalid device provided!");
                return device;
            }

            double resistance = ReceiverConfig.DeviceResistanceMap[device];
            double voltage = (ADConverter.CurrentDACVoltage1 / (14.00 + resistance)) * resistance;
            Debug.WriteLine("Setting ARD for Device " + device + " to " + voltage.ToString());

            if (!RasPi.isTestMode())
            {
                ADConverter.setDACVoltage2(voltage);
            }

            StorageCfgs.Hi.CurrentReceiver = device;
            StorageHandler<Hi>.Save(StorageCfgs.FileName_HiCfg, StorageCfgs.Hi).Wait(10000);

            this.updateLCD();

            return device;
        }
    }
}
