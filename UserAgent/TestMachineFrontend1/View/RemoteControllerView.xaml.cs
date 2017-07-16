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
    /// Interaction logic for RemoteControllerView.xaml
    /// </summary>
    public partial class RemoteControllerView : UserControl
    {
        DebugViewModel vmDebug;
        RemoteControllerViewModel remoteVM;
        public RemoteControllerView()
        {
            InitializeComponent();
            vmDebug = MainWindowViewModel.CurrentViewModelDebug;
            remoteVM = MainWindowViewModel.CurrentViewModelRemoteController;
        }

        private void press_Combination(object sender, RoutedEventArgs e)
        {
            if (remoteVM.getDuration() != -1)
            {
                int[] param = new int[4];
                for (int i = 0; i < param.Length; i++)
                {
                    param[i] = 0;
                }
                param[param.Length - 1] = remoteVM.getDuration();

                int duration = remoteVM.getDuration();
                //if (rockerswitch_Down_Checkbox.IsChecked == true)
                //{
                //    param[0] = 1;
                //}
                //if (rockerswitch_Up_Checkbox.IsChecked == true)
                //{
                //    param[1] = 1;
                //}
                //if (pushButton_Checkbox.IsChecked == true)
                //{
                //    param[2] = 1;
                //}
                Request request = new Request("PressCombination", param);
                remoteVM.sendRequest(request);
                remoteVM.getResult(request);
            }
            else
            {
                vmDebug.AddDebugInfo("Debug", "Invalid duration");
            }
        }

        private void power_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void power_Slider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void power_Slider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }
    }
}
