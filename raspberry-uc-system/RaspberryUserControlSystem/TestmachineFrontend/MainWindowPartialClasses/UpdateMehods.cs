using CommonFiles.TransferObjects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        public Dictionary<string, MethodInfo> commandMap = new Dictionary<string, MethodInfo>
        {
            {"EnableTeleCoil", typeof(MainWindow).GetMethod("updateTeleCoilUI")},
            {"EnableAudioShoe", typeof(MainWindow).GetMethod("updateAudioShoeUI")},
            {"TurnHIOn", typeof(MainWindow).GetMethod("updatePowerUI")},
            {"NotImplemented", typeof(MainWindow).GetMethod("updateSoundUI")},
            {"PressRockerSwitch", typeof(MainWindow).GetMethod("updateRockerSwitchUI")},
            {"PressPushButton", typeof(MainWindow).GetMethod("updatePushButtonUI")},
        };

        public void updateTeleCoilUI(Result result)
        {
            string value = (string)result.value;

            if (value != null)
            {
                if (value.Equals("High"))
                {
                    TCoil_Eclipse.Fill = new SolidColorBrush(Colors.Green);
                }
                else if (value.Equals("Low"))
                {
                    TCoil_Eclipse.Fill = new SolidColorBrush(Colors.Red);
                }
            }
            addMessage("Update", "ToggleTeleCoil completed");
        }

        public void updateAudioShoeUI(Result result)
        {
            string value = (string)result.value;

            if (value != null)
            {
                if (value.Equals("High"))
                {
                    AudioShoe_Eclipse.Fill = new SolidColorBrush(Colors.Green);
                }
                else if (value.Equals("Low"))
                {
                    AudioShoe_Eclipse.Fill = new SolidColorBrush(Colors.Red);

                }
            }
            addMessage("Update", "ToggleAudioShoe completed");
        }

        public void updatePowerUI(Result result)
        {
            double value = (double)result.value;
            // 1.5V is the maximum
            double frac = value / 1.5;
            Color color = Colors.Green;
            color.ScA = (int)System.Math.Floor(frac * 255.0);
            IO_Eclipse.Fill = new SolidColorBrush(color);
            addMessage("Update", "TurnHIOn completed");
        }

        public void updateSoundUI(Result result)
        {
            Debug.WriteLine("updateSoundUI");
        }

        public void updatePushButtonUI(Result result)
        {
            addMessage("Update", "PressPushButton GUI notification not yet implemented!");
        }

        public void updateRockerSwitchUI(Result result)
        {
            addMessage("Update", "PressRockerSwitch GUI notification not yet implemented!");
        }
    }
}
