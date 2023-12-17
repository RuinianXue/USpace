using System;
using System.Windows.Input;

namespace TomatoClock.Command
{
    /// <summary>
    /// 实现了 ICommand 接口的自定义命令类。
    /// </summary>
    public class MyCommand : ICommand
    {
        /// <summary>
        /// 当命令的可执行状态更改时发生的事件。
        /// </summary>
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

        /// <summary>
        /// 初始化 MyCommand 类的新实例，指定执行命令的方法。
        /// </summary>
        /// <param name="execute">执行命令的方法。</param>
        public MyCommand(Action<object> execute) : this(execute, null)
        {

        }

        /// <summary>
        /// 初始化 MyCommand 类的新实例，指定执行命令的方法和确定命令是否可执行的方法。
        /// </summary>
        /// <param name="execute">执行命令的方法。</param>
        /// <param name="canExecute">确定命令是否可执行的方法。</param>
        public MyCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        /// <summary>
        /// 确定命令是否可以在其当前状态下执行。
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果不需要传递数据，则可以设置为 null。</param>
        /// <returns>如果命令可以执行，则为 true；否则为 false。</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        /// <summary>
        /// 执行命令的方法。
        /// </summary>
        /// <param name="parameter">命令使用的数据。如果不需要传递数据，则可以设置为 null。</param>
        public void Execute(object parameter)
        {
            if (_execute == null) return;
            _execute(parameter);
        }

        private Action<object> _execute;
        private Func<object, bool> _canExecute;
    }
}
