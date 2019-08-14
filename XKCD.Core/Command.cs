using System;
using System.Windows.Input;

namespace XKCD.Core.ViewModel
{
    public class Command : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public Command( Action<object> execute, Func<object, bool> canExecute = null )
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute( object parameter )
        {
            return canExecute?.Invoke( parameter ) ?? true;
        }

        public void Execute( object parameter )
        {
            execute( parameter );
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke( this, EventArgs.Empty );
        }
    }
}
