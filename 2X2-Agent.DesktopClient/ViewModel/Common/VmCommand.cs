using System;
using System.Windows.Input;

namespace _2X2_Agent.DesktopClient.ViewModel.Common
{
    class VmCommand : System.Windows.Input.ICommand
    {

        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public VmCommand(Action<object> execute) : this(execute, null){}

        public VmCommand(Action<object> _execute, Predicate<object> _canExecute = null)
        {
            if (_execute == null) throw new ArgumentNullException("execute");
            execute = _execute;
            canExecute = _canExecute;
        }

        
        public bool CanExecute(object parameter)
        {
            if (canExecute == null) return true;
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
