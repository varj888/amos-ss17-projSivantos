using System;
using System.Collections.Generic;

namespace RaspberryBackend
{
    /// <summary>
    /// This is the single stored information point on the Receiver Configuration. For now it is only used by <seealso cref="Operation.SetARDVoltage"/>.
    /// </summary>
    public static class ReceiverConfig
    {

        public static Tuple<string, double> SmallRight = new Tuple<string, double>("Small Right", 0.787);
        public static Tuple<string, double> SmallLeft = new Tuple<string, double>("Small Left", 1.540);

        public static Tuple<string, double> MediumRight = new Tuple<string, double>("Medium Right", 2.490);
        public static Tuple<string, double> MediumLeft = new Tuple<string, double>("Medium Left", 3.480);
        public static Tuple<string, double> PowerRight = new Tuple<string, double>("Power Right", 4.870);
        public static Tuple<string, double> PowerLeft = new Tuple<string, double>("Power Left", 6.490);

        public static Tuple<string, double> HighPowerRight = new Tuple<string, double>("High Power Right", 8.660);
        public static Tuple<string, double> HighPowerLeft = new Tuple<string, double>("High Power Left", 11.000);

        public static Tuple<string, double> Defective = new Tuple<string, double>("Defective", 133.700);
        public static Tuple<string, double> NoReceiver = new Tuple<string, double>("NoReceiver", 200.0);


        /// <summary>
        /// Dictionary to represent possible receivers to detect with their respective resistance. Refer to
        /// https://drive.google.com/drive/folders/0BzaNmZTttJK4N1dmUWt0VFRzU3c for more information.
        /// </summary>
        public static Dictionary<string, double> DeviceResistanceMap { get; } = new Dictionary<string, double>
        {
            {SmallRight.Item1, SmallRight.Item2},
            { SmallLeft.Item1, SmallLeft.Item2 },
            {MediumRight.Item1,MediumRight.Item2 },
            {MediumLeft.Item1, MediumLeft.Item2 },
            { PowerRight.Item1, PowerRight.Item2},
            {PowerLeft.Item1, PowerLeft.Item2 },
            { HighPowerRight.Item1, HighPowerRight.Item2},
            {HighPowerLeft.Item1, HighPowerLeft.Item2 },
            { Defective.Item1, Defective.Item2},
            {NoReceiver.Item1, NoReceiver.Item2 },
        };
    }
}
