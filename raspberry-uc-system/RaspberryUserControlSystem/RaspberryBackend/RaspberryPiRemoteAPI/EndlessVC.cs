using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it can be used to write on a spefic gpio pin of the RaspberryPi.
    /// </summary>
    public partial class RaspberryPi
    {

        private int _ticks_counter = 0;

        //UI Bindings
        public int Ticks_Counter { get => _ticks_counter; set => _ticks_counter = value; }

        /// <summary>
        /// execute the Command WritePin
        /// </summary>
        /// <param name="parameters">represents the GpioPin:Uint16 which shall be written on</param>
        public string EndlessVCUp(int[] param)
        {
            if (Ticks_Counter == int.MaxValue) return Ticks_Counter.ToString();
            ++Ticks_Counter;
            
            UInt16 pushButton_Pin_Up;

            pushButton_Pin_Up = rockerSwitch_Pin_1;

            Debug.WriteLine("Endless VC Up, Tick Counter: " + _ticks_counter);
            activatePin(pushButton_Pin_Up);
            Task.Delay(50).Wait();
            deactivatePin(pushButton_Pin_Up);

            return Ticks_Counter.ToString();
        }

        public string EndlessVCDown(int[] param)
        {
            if (Ticks_Counter == int.MinValue) return Ticks_Counter.ToString();
            --Ticks_Counter;

            UInt16 pushButton_Pin_Down;

            pushButton_Pin_Down = rockerSwitch_Pin_0;

            Debug.WriteLine("Endless VC Down, Tick Counter: " + _ticks_counter);
            activatePin(pushButton_Pin_Down);
            Task.Delay(50).Wait();
            deactivatePin(pushButton_Pin_Down);

            return Ticks_Counter.ToString();
        }
    }
}
