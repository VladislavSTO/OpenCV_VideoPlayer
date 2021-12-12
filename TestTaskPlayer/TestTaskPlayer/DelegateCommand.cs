using System;
using System.Windows.Input;

namespace TestTaskPlayer.ViewModel
{
    public class DelegateCommand : ICommand
    {
        Action<Object> execute;
        Func<Object, bool> canExecute;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public DelegateCommand(Action<object> executeAction) : this(executeAction, null)
        {

        }
        public DelegateCommand(Action<object> executeAction, Func<Object, bool> canExecuteFunk)
        {
            canExecute = canExecuteFunk;
            execute = executeAction;
        }
    }
}