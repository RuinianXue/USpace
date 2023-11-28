using System;
using System.Windows.Input;

namespace TomatoClock.Command
{
    class MyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public MyCommand(Action<object> execute) : this(execute, null)
        {

        }

        public MyCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_execute == null) return;
            _execute(parameter);
        }

        private Action<object> _execute;
        private Func<object, bool> _canExecute;
    }
}
