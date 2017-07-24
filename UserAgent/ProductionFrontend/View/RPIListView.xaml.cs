using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for RPIListView.xaml
    /// </summary>
    public partial class RPIListView : UserControl
    {
        public RPIListView()
        {
            InitializeComponent();
        }

        // Only allow entry of IP Address related characters
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^.0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            AboutView av = new AboutView();
            av.Show();
        }
    }
}
