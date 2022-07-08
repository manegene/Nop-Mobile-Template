using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace habahabamall.Commands
{
    /// <summary>
    /// This class extends from ICommand.
    /// To avoid pressing button multiple times.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private bool _canExecute = true;
        private readonly Action<object, ItemTappedEventArgs> onFeaturedSelected;

        public DelegateCommand(Action<object> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public DelegateCommand(Action<object, ItemTappedEventArgs> onFeaturedSelected)
        {
            this.onFeaturedSelected = onFeaturedSelected;
        }

        public int Delay { get; set; } = 100;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            try
            {
                _canExecute = false;
                RaiseCanExecuteChanged();

                _execute(parameter);
                await Task.Delay(Delay);
            }
            finally
            {
                _canExecute = true;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                try
                {
                    handler(this, EventArgs.Empty);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}