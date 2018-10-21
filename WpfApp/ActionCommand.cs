using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp
{
    class ActionCommand : ICommand
    {
        private bool enable;
        private Action action;
        public event EventHandler CanExecuteChanged;
        public ActionCommand(Action action) { this.action = action; }

        public bool CanExecute(object parameter)
        {
            return enable;
        }

        public void Execute(object parameter)
        {

            action();
        }

        public void SetEnable(bool enable)
        {
            this.enable = enable;
            CanExecuteChanged(this, new EventArgs());
        }
    }
}
