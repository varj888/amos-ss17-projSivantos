using System.Collections.Generic;

namespace RaspberryBackend
{
    /// <summary>
    /// This is the single stored information point on the Receiver Configuration. For now it is only used by <seealso cref="Operation.SetARDVoltage"/>.
    /// </summary>
    public static class ReceiverConfig
    {
        public static string CurrentReceiver { get; set; } = "None";

        /// <summary>
        /// Dictionary to represent possible receivers to detect with their respective resistance. Refer to
        /// https://drive.google.com/drive/folders/0BzaNmZTttJK4N1dmUWt0VFRzU3c for more information.
        /// </summary>
        public static Dictionary<string, double> DeviceResistanceMap { get; } = new Dictionary<string, double>
        {
            {"Small Right", 0.787},
            {"Small Left", 1.540},
            {"Medium Right", 2.490},
            {"Medium Left", 3.480},
            {"Power Right", 4.870},
            {"Power Left", 6.490},
            {"High Power Right", 8.660},
            {"High Power Left", 11.000},
            {"Defective", 133.700},
            {"No Receiver", 200.0},
        };
    }
}
