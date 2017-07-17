using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    public partial class Operation
    {
        /// <summary>
        /// Reads the current configuration of the Raspberry
        /// </summary>
        /// <param name="i">not used</param>
        /// <returns>The current configuration as human readable string</returns>
        public string GetRaspiConfig(int i)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("IPAddress: " + this.GetIpAddressAsync() + "\n");
            sb.Append("Initialized: " + RasPi.isInitialized() + "\n");
            sb.Append("TestMode: " + RasPi.isTestMode() + "\n");
            sb.Append("LCD: \n");
            sb.Append("\t Initialized: " + this.LCD.isInitialized() + "\n");
            sb.Append("\t Text: " + this.LCD.CurrentTextPlainString + "\n");
            sb.Append("Potentiometer: \n");
            sb.Append("\t Initialized: " + this.Potentiometer.isInitialized() + "\n");
            sb.Append("HI: \n");
            sb.Append("\t Family: " + StorageCfgs.Hi.Family + "\n");
            sb.Append("\t Model: " + StorageCfgs.Hi.Model + "\n");
            sb.Append("\t Available Usercontrols: \n");
            XPinConfig mux_config = HiXmlParser.getMultiplexerConfig(StorageCfgs.Hi.Family, StorageCfgs.Hi.Model);

            foreach (var pair in mux_config.X_Pin_To_Value_Map)
            {
                if (!pair.Value.Equals("")) sb.Append("\t\t X-Pin:" + pair.Key + ", UserControl: " + pair.Value + "\n");
            }

            sb.Append("Status of Usercontrols: \n");
            sb.Append("\t Telecoil detected: " + getTeleCoilStatus() + "\n");
            sb.Append("\t Audio Shoe detected: " + getAudioShoeStatus() + "\n");
            sb.Append("\t LED is On: " + CheckLEDStatus(0) + "\n");


            return sb.ToString();
        }
    }
}
