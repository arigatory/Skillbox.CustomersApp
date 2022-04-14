using Skillbox.CustomersApp.Model;
using System.Text.RegularExpressions;

namespace Skillbox.CustomersApp.ViewModel
{
    /// <summary>
    /// ViewModel для работы с одной записью Клиента
    /// </summary>
    public class CustomerItemViewModel : ValidationViewModelBase
    {
        /// <summary>
        /// В модели храним реальные данные их хранилища.
        /// </summary>
        public Customer Model { get; private set; }

        /// <summary>
        /// Конструктор для создания ViewModel для работы с одной записью Клиента
        /// </summary>
        /// <param name="model">Модель, которую мы берем из какого-либо хранилища</param>
        public CustomerItemViewModel(Customer model)
        {
            Model = model;
        }

        /// <summary>
        /// Пользовательскому интерфейсу не обязательно знать об изменениях Id, поэтому просто возращаем его.
        /// </summary>
        public int Id => Model.Id;

        /// <summary>
        /// При записи имени уведомляем об этом пользовательский интерфейс, чтобы сразу менять текст в панели навигации.
        /// </summary>
        public string? FirstName
        {
            get { return Model.FirstName; }
            set
            {
                Model.FirstName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// При записи отчества уведомляем об этом пользовательский интерфейс, чтобы сразу менять текст в панели навигации.
        /// </summary>
        public string? MiddleName
        {
            get { return Model.MiddleName; }
            set
            {
                Model.MiddleName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// При записи фамилии уведомляем об этом пользовательский интерфейс, чтобы сразу менять текст в панели навигации.
        /// Кроме того проверям, что фамилия обязательно заполнена.
        /// </summary>
        public string? LastName
        {
            get { return Model.LastName; }
            set
            {
                Model.LastName = value;
                RaisePropertyChanged();
                if (string.IsNullOrEmpty(Model.LastName))
                {
                    AddError("Фамилия обязательна");
                }
                else
                {
                    ClearErrors();
                }
            }
        }

        /// <summary>
        /// При записи телефона уведомляем об этом пользовательский интерфейс. В данный момент это не обязательно,
        /// но может потребоваться, если в навигации рядом с именем мы захотим отображать и телефон.
        /// Кроме того проверям регулярным выражением, что телефон обязательно похож на телефон, а не просто строка.
        /// </summary>
        public string? PhoneNumber
        {
            get { return Model.PhoneNumber; }
            set
            {
                Model.PhoneNumber = value;
                RaisePropertyChanged();
                if (string.IsNullOrEmpty(Model.PhoneNumber))
                {
                    AddError("Телефон обязателен");
                }
                else if (!Regex.Match(Model.PhoneNumber, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$").Success)
                {
                    AddError("Не похоже не номер телефона");
                }
                else
                {
                    ClearErrors();
                }
            }
        }

        /// <summary>
        /// При записи данных о паспорте уведомляем об этом пользовательский интерфейс.
        /// </summary>
        public string? PassportNumber
        {
            get { return Model.PassportNumber; }
            set
            {
                Model.PassportNumber = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Свойство для доступа к информации о том, кто внес изменения. У нас это менеджер или консультант, но в более сложном
        /// приложении может потребовать хранить, например, ФИО и должность, поэтому решил оставить строкой для удобства логирования.
        /// </summary>
        public string? EditedBy
        {
            get { return Model.EditedBy; }
            set
            {
                Model.EditedBy = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Info));
            }
        }

        /// <summary>
        /// Вычисляем строку, которую будем показывать пользователю об имеющихся изменениях в записи.
        /// </summary>
        public string Info
        {
            get
            {
                if (string.IsNullOrEmpty(Model.EditedBy))
                {
                    return string.Empty;
                }
                var lastEdited = "неизвестно когда";
                if (Model.LastEdited.HasValue)
                {
                    lastEdited = Model.LastEdited.Value.ToString();
                }
                return $"Изменил {Model.EditedBy} {lastEdited}";
            }
        }
    }
}
