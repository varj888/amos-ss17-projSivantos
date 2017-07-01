using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestMachineFrontend1.View
{
    /// <summary>
    /// Interaction logic for UserControlsView.xaml
    /// </summary>
    public partial class UserControlsView : UserControl
    {
        public UserControlsView()
        {
            InitializeComponent();
        }

        private void soundSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void press_Combination(object sender, RoutedEventArgs e)
        {
            //if (getDuration() != -1)
            //{
            //    int[] param = new int[4];
            //    for (int i = 0; i < param.Length; i++)
            //    {
            //        param[i] = 0;
            //    }
            //    param[param.Length - 1] = getDuration();

            //    int duration = getDuration();
            //    if (rockerswitch_Down_Checkbox.IsChecked == true)
            //    {
            //        param[0] = 1;
            //    }
            //    if (rockerswitch_Up_Checkbox.IsChecked == true)
            //    {
            //        param[1] = 1;
            //    }
            //    if (pushButton_Checkbox.IsChecked == true)
            //    {
            //        param[2] = 1;
            //    }
            //    sendRequest(new Request("PressCombination", param));
            //}
            //else
            //{
            //    this.addMessage("Debug", "Invalid duration");
            //}
        }

        private void receiverUpdate_Click(object sender, RoutedEventArgs e)
        {
            //int a = this.receiverBox.SelectedIndex;
            //ComboBoxItem s = (ComboBoxItem)receiverBox.Items[a];
            //sendRequest(new Request("SetARDVoltage", s.Content));
        }

        //private void Endless_VC_Up_Click(object sender, RoutedEventArgs e)
        //{
        //    sendRequest(new Request("EndlessVCUp", new int[] { }));
        //    this.addMessage("Endless_VC_Up", "+1");
        //}

        //private void Endless_VC_Down_Click(object sender, RoutedEventArgs e)
        //{
        //    sendRequest(new Request("EndlessVCDown", new int[] { }));
        //    this.addMessage("Endless_VC_Down", "-1");
        //}

        private void SetVolume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //throw new NotImplementedException();
        }
    }
}
