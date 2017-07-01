using System.Windows;
using System.Windows.Controls;

namespace TestMachineFrontend1.View
{
    /// <summary>
    /// Interaction logic for DetectTabView.xaml
    /// </summary>
    public partial class DetectTabViewOld : UserControl
    {
        public DetectTabViewOld()
        {
            InitializeComponent();
        }

        //private void setVolume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    Slider el = sender as Slider;
        //    //TODO
        //    //sendRequest(new Request("SetAnalogVolume", Convert.ToByte(el.Value)));
        //}

        //private void vcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    Slider el = sender as Slider;
        //    //TODO
        //    //sendRequest(new Request("TurnHIOn", el.Value));
        //}
    }
}
