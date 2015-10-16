namespace Defender.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Defender.Model.Extensions;

    public class RelayCommand : ICommand
    {
        private Action<object> _execute;

        private Predicate<object> _canexecute;

        private event EventHandler _canexecutechanged;

        public RelayCommand(Action<object> _execute) 
            : this(_execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canexecute)
        {
            if (execute == null) throw new ArgumentNullException();
            if (canexecute == null) throw new ArgumentNullException();

            this._execute = execute;
            this._canexecute = canexecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this._canexecutechanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                this._canexecutechanged -= value;
            }
        }

        public bool CanExecute(object param)
        {
            return this._canexecute != null && this.CanExecute(param);
        }

        public void Execute(object param)
        {
            this._execute(param);
        }

        public void OnExecuteChanged()
        {
            EventHandler ehandler = this._canexecutechanged;

            if (ehandler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => ehandler.Invoke(this, EventArgs.Empty));
                ehandler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this._canexecute = @this => false;
            this._execute = @this => { return; };
        }

        public static bool DefaultCanExecute(object param)
        {
            return true;
        }
    }
}
