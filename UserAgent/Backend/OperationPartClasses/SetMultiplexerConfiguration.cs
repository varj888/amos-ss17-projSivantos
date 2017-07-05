using System.Collections.Generic;
using System.Diagnostics;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It is a partial Operation class
    /// </summary>
    public partial class Operation
    {

        /// <summary>
        /// Sets the multiplexer configuation to a default HI from the XML Config file.
        /// The HI is:
        /// Family: "Pure", Model: "312 702 S (DN)"
        /// </summary>
        public MultiplexerConfig setMultiplexerConfiguration()
        {
            return setMultiplexerConfiguration("Pure", "312 702 S (DN)");
        }

        /// <summary>
        /// Sets the multiplexer configuation to a specific HI
        /// </summary>
        /// <param name="family">HiFamily name of the HI, e.g.: "Pure"</param>
        /// <param name="model_name">HiModel name of the HI: e.g: "312 702 S (DN)"</param>
        public MultiplexerConfig setMultiplexerConfiguration(string family, string model_name)
        {
            Debug.WriteLine(this.GetType().Name + "::: Setting Multiplexer Config:");

            MultiplexerConfig muxConfig = new MultiplexerConfig(family, model_name);
            Dictionary<int, int> xToYMapping = muxConfig.getX_to_Y_Mapping();


            if (!RasPi.isTestMode())
            {
                foreach (var keyValuePair in xToYMapping)
                {
                    Multiplexer.connectPins(keyValuePair.Key, keyValuePair.Value);
                }
            }
            return muxConfig;
        }
    }
}
