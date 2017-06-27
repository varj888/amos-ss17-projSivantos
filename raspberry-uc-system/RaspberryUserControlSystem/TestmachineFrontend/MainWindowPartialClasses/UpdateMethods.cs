using CommonFiles.TransferObjects;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, List<string>> availableHI;
        /// <summary>
        /// Updates a UI element respectlivly to the result of a sent command from Frontend to Backend.
        /// </summary>
        /// <param name="result">The responded result from Backend. It is either "High" or "Low"</param>
        public void updateGUI_EnableTeleCoil(Result result)
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

        /// <summary>
        /// Updates a UI element respectlivly to the result of a sent command from Frontend to Backend
        /// </summary>
        /// <param name="result">The responded result from Backend. It is either "High" or "Low"</param>
        public void updateGUI_EnableAudioShoe(Result result)
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

        /// <summary>
        /// Updates a UI element respectlivly to the result of a sent command from Frontend to Backend
        /// </summary>
        /// <param name="result">The responded result from Backend. It is double value in the intervall [0:1.5]</param>
        public void updateGUI_TurnHIOn(Result result)
        {
            double value = (double)result.value;
            // 1.5V is the maximum
            double frac = value / 1.5;
            Color color = Colors.Green;
            color.ScA = (int)System.Math.Floor(frac * 255.0);
            IO_Eclipse.Fill = new SolidColorBrush(color);
            addMessage("Update", "TurnHIOn completed");
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="result"></param>
        public void updateGUI_Sound(Result result)
        {
            addMessage("Update", "updateSoundUI GUI notification not yet implemented!");
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="result">The responded result from Backend</param>
        public void updateGUI_PressPushButton(Result result)
        {
            addMessage("Update", "PressPushButton GUI notification not yet implemented!");
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="result">The responded result from Backend</param>
        public void updateGUI_PressRockerSwitch(Result result)
        {
            addMessage("Update", "PressRockerSwitch GUI notification not yet implemented!");
        }

        public void updateGUI_EndlessVCDown(Result result)
        {
            int ticks = (int)result.value;
            this.addMessage("updateGUI_EndlessVCDown", "Pressed released EndlessVCDown" + ticks + " times successfully.");
        }

        public void updateGUI_EndlessVCUp(Result result)
        {
            int ticks = (int)result.value;
            this.addMessage("updateGUI_EndlessVCUp", "Pressed released EndlessVCUp" + ticks + " times successfully.");
        }

        public void updateGUI_SetHI(Result result)
        {
            addMessage(result.obj, "Successfully set HI");
        }

        public void updateGUI_GetAvailableHI(Result result)
        {
            this.availableHI = buildDictionary((string)result.value);
            foreach(string family in availableHI.Keys)
            {
                foreach(string model in availableHI[family])
                {
                    ComboBoxItem element = new ComboBoxItem();
                    element.Name = family;
                    element.Content = model;
                    availableHIList.Items.Add(element);
                }
            }
            addMessage(result.obj, "Updated List");
        }

        public void updateGUI_SetHI(Result result)
        {
            addMessage(result.obj, "Successfully set HI");
        }

        public void updateGUI_GetAvailableHI(Result result)
        {
            this.availableHI = buildDictionary((string)result.value);
            foreach(string family in availableHI.Keys)
            {
                foreach(string model in availableHI[family])
                {
                    ComboBoxItem element = new ComboBoxItem();
                    element.Name = family;
                    element.Content = model;
                    availableHIList.Items.Add(element);
                }
            }
            addMessage(result.obj, "Updated List");
        }
    }
}
