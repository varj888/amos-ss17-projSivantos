using CommonFiles.TransferObjects;
using System;
using System.Windows;
using System.Windows.Controls;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.View
{
    /// <summary>
    /// Interaction logic for DetectTabView.xaml
    /// </summary>
    public partial class DetectTabView : UserControl
    {
        public DetectTabView()
        {
            InitializeComponent();
        }

        private void setVolume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider el = sender as Slider;
            //TODO check
            DetectTabViewModel vm = (DetectTabViewModel)DataContext;
            vm.sendRequest(new Request("SetAnalogVolume", Convert.ToByte(el.Value)));
            //sendRequest(new Request("SetAnalogVolume", Convert.ToByte(el.Value)));
        }

        private void vcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider el = sender as Slider;
            //TODO check
            DetectTabViewModel vm = (DetectTabViewModel)DataContext;
            vm.sendRequest(new Request("TurnHIOn", el.Value));
            //sendRequest(new Request("TurnHIOn", el.Value));
        }
    }
}
