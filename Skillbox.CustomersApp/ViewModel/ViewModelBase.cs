using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.ViewModel
{
    /// <summary>
    /// Базовый класс для ViewModel, реализующий INotifyPropertyChanged
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// событие требуется для реализации INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Метод, который будем вызывать при изменении свойств ViewModel для перерисовки View.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Любая ViewModel должна при отображении соответствующего View загрузиться (загрузить данные, например).
        /// Если ничего не будет делать такого, то можно не переопределять.
        /// </summary>
        /// <returns></returns>
        public virtual Task LoadAsync() => Task.CompletedTask;
    }
}
