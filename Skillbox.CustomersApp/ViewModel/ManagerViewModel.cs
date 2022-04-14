using Skillbox.CustomersApp.Data;
using Skillbox.CustomersApp.Model;

namespace Skillbox.CustomersApp.ViewModel
{
    /// <summary>
    /// Класс отвечает за все действия менеджера в системе. Они аналогичны действиям консультанта, поэтому наследуемся от
    /// общего класса UserViewModel
    /// </summary>
    public class ManagerViewModel : UserViewModel
    {
        /// <summary>
        /// Особенность в том, что в базовый конструктор мы передаем Consultant.
        /// Это основное отличие от ManagerViewModel
        /// </summary>
        /// <param name="customersDataProvider">Поставщик данных. В нашем случае будет подставлен класс для работы с 
        /// json файлом</param>
        /// <param name="consultant">Менеджер, представляющий пользователя</param>
        public ManagerViewModel(ICustomersDataProvider customersDataProvider, Manager manager) 
            : base(customersDataProvider, manager)
        {
        }
    }
}
