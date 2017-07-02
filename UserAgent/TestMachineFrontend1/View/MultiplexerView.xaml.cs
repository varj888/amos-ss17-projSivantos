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
    /// Interaction logic for MultiplexerView.xaml
    /// </summary>
    public partial class MultiplexerView : UserControl
    {
        MainWindowViewModel vm;
        DetectTabViewModel vmCurrent;
        DebugViewModel vmDebug;
        MultiplexerViewModel vmMux;
        public MultiplexerView()
        {
            InitializeComponent();
            vm = (MainWindowViewModel)DataContext;
            vmCurrent = (DetectTabViewModel)vm.CurrentViewModelDetectTab;
            vmDebug = (DebugViewModel)vm.CurrentViewModelDebug;
            vmMux = (MultiplexerViewModel)vm.CurrentViewModelMultiplexer;
        }

        private void setPinsButton_Click(object sender, RoutedEventArgs e)
        {
            vmCurrent.sendRequest(new Request("ConnectPins", new object[] { (int)vmMux.ValueX, (int)vmMux.ValueY }));
        }

        private void resetMux_Click(object sender, RoutedEventArgs e)
        {
            vmCurrent.sendRequest(new Request("ResetMux", 0));
        }

        private void availableHI_Click(object sender, RoutedEventArgs e)
        {
            vmCurrent.sendRequest(new Request("GetAvailableHI", 0));
        }

        private void setHI_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem ci;
            try
            {
                ci = (ComboBoxItem)availableHIList.Items.GetItemAt(availableHIList.SelectedIndex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                vmDebug.AddDebugInfo("setHI_Click", "No valid model selected.");
                return;
            }

            string model = ci.Content.ToString();
            string family = ci.Name;

            vmCurrent.sendRequest(new Request("SetHI", new Object[] { family, model }));
        }
    }
}
