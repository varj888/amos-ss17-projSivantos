using CommonFiles.TransferObjects;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace TestmachineFrontend
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Updates a UI element respectlivly to the result of a sent command from Frontend to Backend.
        /// </summary>
        /// <param name="result">The responded result from Backend. It is either "High" or "Low"</param>
        //public void updateGUI_EnableTeleCoil(Result result)
        //{
        //    string value = (string)result.value;

        //    if (value != null)
        //    {
        //        if (value.Equals("High"))
        //        {
        //            TCoil_Eclipse.Fill = new SolidColorBrush(Colors.Green);
        //        }
        //        else if (value.Equals("Low"))
        //        {
        //            TCoil_Eclipse.Fill = new SolidColorBrush(Colors.Red);
        //        }
        //    }
        //    addMessage("Update", "ToggleTeleCoil completed");
        //}

        ///// <summary>
        ///// Updates a UI element respectlivly to the result of a sent command from Frontend to Backend
        ///// </summary>
        ///// <param name="result">The responded result from Backend. It is either "High" or "Low"</param>
        //public void updateGUI_EnableAudioShoe(Result result)
        //{
        //    string value = (string)result.value;

        //    if (value != null)
        //    {
        //        if (value.Equals("High"))
        //        {
        //            AudioShoe_Eclipse.Fill = new SolidColorBrush(Colors.Green);
        //        }
        //        else if (value.Equals("Low"))
        //        {
        //            AudioShoe_Eclipse.Fill = new SolidColorBrush(Colors.Red);

        //        }
        //    }
        //    addMessage("Update", "ToggleAudioShoe completed");
        //}

        ///// <summary>
        ///// Updates a UI element respectlivly to the result of a sent command from Frontend to Backend
        ///// </summary>
        ///// <param name="result">The responded result from Backend. It is double value in the intervall [0:1.5]</param>
        //public void updateGUI_TurnHIOn(Result result)
        //{
        //    double value = (double)result.value;
        //    // 1.5V is the maximum
        //    double frac = value / 1.5;
        //    Color color = Colors.Green;
        //    color.ScA = (int)System.Math.Floor(frac * 255.0);
        //    IO_Eclipse.Fill = new SolidColorBrush(color);
        //    addMessage("Update", "TurnHIOn completed");
        //}

        ///// <summary>
        ///// Not yet implemented
        ///// </summary>
        ///// <param name="result"></param>
        //public void updateGUI_Sound(Result result)
        //{
        //    addMessage("Update", "updateSoundUI GUI notification not yet implemented!");
        //}

        ///// <summary>
        ///// Not yet implemented
        ///// </summary>
        ///// <param name="result">The responded result from Backend</param>
        //public void updateGUI_PressPushButton(Result result)
        //{
        //    addMessage("Update", "PressPushButton GUI notification not yet implemented!");
        //}

        ///// <summary>
        ///// Not yet implemented
        ///// </summary>
        ///// <param name="result">The responded result from Backend</param>
        //public void updateGUI_PressRockerSwitch(Result result)
        //{
        //    addMessage("Update", "PressRockerSwitch GUI notification not yet implemented!");
        //}

        //public void updateGUI_EndlessVCDown(Result result)
        //{
        //    string ticks = (string)result.value;
        //    Debug.WriteLine("EndlessVCDown update ticks.");
        //    this.Ticks.Text = ticks.ToString();
        //}

        //public void updateGUI_EndlessVCUp(Result result)
        //{
        //    string ticks = (string) result.value;
        //    Debug.WriteLine("EndlessVCUp update ticks.");
        //    this.Ticks.Text = ticks;
        //}
    }
}
