using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.ViewModel
{
    /// <summary>
    /// Класс ValidationViewModelBase, реализующий интерфейс INotifyDataErrorInfo.
    /// Наследуемся от ViewModelBase, чтобы любая ViewModel наследовалась либо от этого класса,
    /// если ей нужна валидация, либо от ViewModelBase, если не нужна
    /// </summary>
    public class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// Список ошибок. У каждого свойства им может быть несколько, поэтому словать.
        /// </summary>
        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();

        /// <summary>
        /// Требуется контактом INotifyDataErrorInfo, чтобы смотреть, если ли ошибки во ViewModel
        /// </summary>
        public bool HasErrors => _errorsByPropertyName.Any();

        /// <summary>
        /// Требуется контактом INotifyDataErrorInfo.
        /// Метод, который будет обрабатывать событие, когда событие предоставляет данные.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        /// <summary>
        /// Получаем все ошибки для переданного свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства, дла которого получаем возможные ошибки</param>
        /// <returns>Список ошибок, либо пустая коллекция, если ошибок нет</returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyName is not null && _errorsByPropertyName.ContainsKey(propertyName)
                ? _errorsByPropertyName[propertyName]
                : Enumerable.Empty<string>();
        }

        /// <summary>
        /// Метод, который будет вызывать ViewModel, когда захочет проверить, исправлены ли ошибки
        /// </summary>
        /// <param name="e">Имя свойства, для котороку осуществляется проверка</param>
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Добавляем свои ошибки, если они нужны
        /// </summary>
        /// <param name="error">Текст ошибки</param>
        /// <param name="propertyName">Имя свойства, которому присваивается ошибка</param>
        protected void AddError(string error, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) return;

            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        /// <summary>
        /// Очищаем свойство от всех ошибок.
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) return;

            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
            }
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }
}
