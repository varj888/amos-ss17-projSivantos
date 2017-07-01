using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestMachineFrontend1.Helpers
{
    public class RelayCommand<T> : ICommand where T : EventArgs
    {
        private readonly Action<T> execute;
        private readonly Func<bool> canExecute;
        //private Action<MouseDoubleClick.DependencyPropertyEventArgs> mouseClickCallBack;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        //public RelayCommand(Action<MouseDoubleClick.DependencyPropertyEventArgs> mouseClickCallBack)
        //{
        //    this.mouseClickCallBack = mouseClickCallBack;
        //}

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        public void Execute(object parameter)
        {
            execute(parameter as T);
        }
    }
}
