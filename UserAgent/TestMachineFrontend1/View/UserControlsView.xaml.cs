using CommonFiles.TransferObjects;
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
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.View
{
    /// <summary>
    /// Interaction logic for UserControlsView.xaml
    /// </summary>
    public partial class UserControlsView : UserControl
    {
        MainWindowViewModel vm;
        DetectTabViewModel vmCurrent;
        UserControlsViewModel vmUC;
        DebugViewModel vmDebug;
        public UserControlsView()
        {
            InitializeComponent();
            vm = (MainWindowViewModel)DataContext;
            vmCurrent = (DetectTabViewModel)vm.CurrentViewModelDetectTab;
            vmUC = (UserControlsViewModel)vm.CurrentViewModelUserControls;
            vmDebug = (DebugViewModel)vm.CurrentViewModelDebug;
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
            if (vmUC.getDuration() != -1)
            {
                int[] param = new int[4];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = 0;
                }
                param[param.Length - 1] = vmUC.getDuration();

                int duration = vmUC.getDuration();
                if (rockerswitch_Down_Checkbox.IsChecked == true)
                {
                    param[0] = 1;
                }
                if (rockerswitch_Up_Checkbox.IsChecked == true)
                {
                    param[1] = 1;
                }
                if (pushButton_Checkbox.IsChecked == true)
                {
                    param[2] = 1;
                }
                vmCurrent.sendRequest(new Request("PressCombination", param));
            }
            else
            {
                vmDebug.AddDebugInfo("Debug", "Invalid duration");
            }
        }

        private void receiverUpdate_Click(object sender, RoutedEventArgs e)
        {
            int a = this.receiverBox.SelectedIndex;
            ComboBoxItem s = (ComboBoxItem)receiverBox.Items[a];
           vmCurrent.sendRequest(new Request("SetARDVoltage", s.Content));
        }

        private void Endless_VC_Up_Click(object sender, RoutedEventArgs e)
        {
           vmCurrent.sendRequest(new Request("EndlessVCUp", new int[] { }));
            vmDebug.AddDebugInfo("Endless_VC_Up", "+1");
        }

        private void Endless_VC_Down_Click(object sender, RoutedEventArgs e)
        {
            vmCurrent.sendRequest(new Request("EndlessVCDown", new int[] { }));
            vmDebug.AddDebugInfo("Endless_VC_Down", "-1");
        }

        private void SetVolume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //throw new NotImplementedException();
        }
    }
}
