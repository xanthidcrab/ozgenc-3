using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMapp2
{
    class RelayCommand : ICommand // Icommand interfacemizi implemente ediyoruz.
    {
        private readonly Action<object> execute; // Komut çalıştırma eylemi
        private readonly Predicate<object> canExecute; // Komutun çalıştırılabilirliğini belirleyen fonksiyon

        public event EventHandler CanExecuteChanged; // Komutun çalıştırılabilirliğinde bir değişiklik olduğunda tetiklenen olay

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            // Eğer canExecute tanımlı değilse (null) her zaman true döner
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter); // Eylemi çalıştırır
        }

        // CanExecuteChanged olayını tetikleyen metod
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
