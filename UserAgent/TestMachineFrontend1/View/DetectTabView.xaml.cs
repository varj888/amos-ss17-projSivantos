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
        //DetectTabViewModel vmCurrent;
        MainWindowViewModel mwVM = MainWindowViewModel.Instance;
        public DetectTabView()
        {
            InitializeComponent();
            //vmCurrent = MainWindowViewModel.CurrentViewModelDetectTab;
        }

        private void setVolume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider el = sender as Slider;
            //TODO check
            Request request = new Request("SetAnalogVolume", Convert.ToByte(el.Value));
            mwVM.sendRequest(request);
            //vmCurrent.getResult(request);
            //sendRequest(new Request("SetAnalogVolume", Convert.ToByte(el.Value)));
        }

        private void vcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider el = sender as Slider;
            //TODO check
            Request request = new Request("TurnHIOn", el.Value);
            mwVM.sendRequest(request);
            //vmCurrent.getResult(request);
            //sendRequest(new Request("TurnHIOn", el.Value));
        }
    }
}
