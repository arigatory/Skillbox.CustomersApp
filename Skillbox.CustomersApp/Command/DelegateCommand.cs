using System;
using System.Windows.Input;

namespace Skillbox.CustomersApp.Command
{
    /// <summary>
    /// Этот класс реализует интерфейс ICommand. 
    /// Идея состоит в том, что модель представления создает экземпляр этого класса 
    /// и присваивает его собственному свойству AddCommand. 
    /// Конструктор класса DelegateCommand принимает делегат Action.
    /// DelegateCommand вызывает методы (делегаты), которые мы назначим команде, 
    /// когда вызывается логика Execute и CanExecute.
    /// Как и сама ViewModel, класс DelegateCommand не зависит от пользовательского интерфейса. 
    /// Он не знает ни о каких элементах пользовательского интерфейса. 
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Метод, который команда будет исполнять
        /// </summary>
        private readonly Action<object?> _execute;

        /// <summary>
        /// Метод для проверки возможности выполнения
        /// </summary>
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// Конструктор DelegateCommand
        /// </summary>
        /// <param name="execute">Метод, который будет выполняться</param>
        /// <param name="canExecute">Метод, который будет вызываться каждый раз, когда необходимо проверить возможность
        /// выполнения команды</param>
        public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            ArgumentNullException.ThrowIfNull(execute);
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Метод для проверки возможного изменения состояния команды (возможности исполнения)
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Переменная для возможности подписки на событие, которое будет происходить при изменения состояния команды.
        /// </summary>
        public event EventHandler? CanExecuteChanged;


        /// <summary>
        /// Метод, который вызывает переданный делегат для проверки возможности выполнения команды,
        /// вызывается всегда, если ничего не передано в конструктор.
        /// </summary>
        /// <param name="parameter">Делегат для проверки возможности выполнения команды</param>
        /// <returns>Всегда true, если ничего не передано в конструктор, иначе результат работы делегата, 
        /// который был передан
        /// </returns>
        public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

        /// <summary>
        /// При нажатии кнопки, на которую назначена команда, вызывается метод Execute.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter) => _execute(parameter);
    }
}
