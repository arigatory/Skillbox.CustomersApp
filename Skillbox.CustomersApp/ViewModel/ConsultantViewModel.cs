using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;

namespace Skillbox.CustomersApp.ViewModel
{
    /// <summary>
    /// Класс отвечает за все действия консультанта в системе. Они аналогичны действиям менеджера, поэтому наследуемся от
    /// общего класса UserViewModel
    /// </summary>
    public class ConsultantViewModel : UserViewModel
    {
        /// <summary>
        /// Особенность в том, что в базовый конструктор мы передаем Consultant.
        /// Это основное отличие от ManagerViewModel
        /// </summary>
        /// <param name="customersDataProvider">Поставщик данных. В нашем случае будет подставлен класс для работы с 
        /// json файлом</param>
        /// <param name="consultant">Консультант, представляющий пользователя</param>
        public ConsultantViewModel(ICustomersDataProvider customersDataProvider, Consultant consultant) 
            : base(customersDataProvider, consultant)
        {
        }
    }
}
