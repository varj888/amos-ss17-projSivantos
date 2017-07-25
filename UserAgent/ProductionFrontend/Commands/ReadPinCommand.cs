using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMachineFrontend1.ViewModel;

namespace TestMachineFrontend1.Commands
{
    public class ReadPinCommand : ICommand
    {
        private TestDebugTabViewModel testDebugVM;

        public ReadPinCommand()
        {
            testDebugVM = MainWindowViewModel.CurrentTestDebugTab;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await testDebugVM.ReadPin();
        }
    }
}
