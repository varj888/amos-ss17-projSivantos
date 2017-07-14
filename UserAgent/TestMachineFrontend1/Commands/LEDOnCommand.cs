using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    class LEDOnCommand : ICommand
    {
        public LEDOnCommand() { }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            String result;
            try
            {
                result = await MainWindowViewModel.raspberryPi.LightLED(1);
            }
            catch (Exception e)
            {
                Debug.WriteLine("LightLED :" + e.Message);
                return;
            }

            Debug.WriteLine("LEDOn: " + result);
        }
    }
}
