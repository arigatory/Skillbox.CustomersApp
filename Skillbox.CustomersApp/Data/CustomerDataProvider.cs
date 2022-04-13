using Skillbox.CustomersApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Data
{
    public interface ICustomersDataProvider
    {
        Task<IEnumerable<Customer>?> GetAllAsync();
    }

    public class CustomerDataProvider : ICustomersDataProvider
    {
        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            await Task.Delay(100); // Simulate a bit of server work

            return new List<Customer>
            {
                new Customer{Id = 1, FirstName = "Иван", MiddleName = "Иванович", LastName = "Панченко", PassportNumber = "1234 56785678", PhoneNumber = "+7(905)761-09-52"},
                new Customer{Id = 2, FirstName = "Андрей", MiddleName = "Анатольевич", LastName = "Иванов", PassportNumber = "1234 56785678" },
                new Customer{Id = 3, FirstName = "Игорь", MiddleName = "Иванович", LastName = "Сахаров", PassportNumber = "1234 56785678", PhoneNumber = "+7(905)761-09-52"},
                new Customer{Id = 4, FirstName = "Светлана", MiddleName = "Ивановна", LastName = "Журавлева",  PassportNumber = "1234 56785678", PhoneNumber = "+7(905)761-09-52"}
            };
        }
    }
}
