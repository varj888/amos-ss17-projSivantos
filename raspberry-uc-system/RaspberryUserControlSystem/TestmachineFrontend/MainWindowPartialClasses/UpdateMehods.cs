using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        public Dictionary<string, MethodInfo> commandMap = new Dictionary<string, MethodInfo>
        {
            {"EnableTeleCoil", typeof(MainWindow).GetMethod("updateUIElement")},
            {"EnableAudioShoe", typeof(MainWindow).GetMethod("updateAudioShoeUI")},
            {"TurnHIOn", typeof(MainWindow).GetMethod("updatePowerUI")},
            {"NotImplemented", typeof(MainWindow).GetMethod("updateSoundUI")},
            {"PressRockerSwitch", typeof(MainWindow).GetMethod("updateRockerSwitchUI")},
            {"PressPushButton", typeof(MainWindow).GetMethod("updatePushButtonUI")},
        };

        public void updateTeleCoilUI(int value)
        {
            Debug.WriteLine("updateTeleCoilUI");
        }

        public void updateAudioShoeUI(int value)
        {
            Debug.WriteLine("updateAudioShoeUI");
        }

        public void updatePowerUI(int value)
        {
            Debug.WriteLine("updatePowerUI");
        }

        public void updateSoundUI(int value)
        {
            Debug.WriteLine("updateSoundUI");
        }

        public void updatePushButtonUI(int value)
        {
            Debug.WriteLine("updatePushButtonUI");
        }

        public void updateRockerSwitchUI(int value)
        {
            Debug.WriteLine("updateRockerSwitch");
        }
    }
}
