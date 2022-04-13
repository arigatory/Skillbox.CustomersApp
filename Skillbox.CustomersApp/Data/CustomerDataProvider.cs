using Skillbox.CustomersApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Data
{
    public interface ICustomersDataProvider
    {
        Task<IEnumerable<Customer>?> GetAllAsync();
        Task SaveAllAsync(IEnumerable<Customer> customers);
    }

    public class CustomerDataProvider : ICustomersDataProvider
    {
        private readonly string _fileName = "customers.json";

        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            try
            {
                using FileStream openStream = File.OpenRead(_fileName);
                return await JsonSerializer.DeserializeAsync<List<Customer>>(openStream);
            }
            catch (Exception)
            {
                return new List<Customer> {
                new Customer{Id = 1, FirstName = "Иван", MiddleName = "Иванович", LastName = "Панченко", PassportNumber = "1234 56785678", PhoneNumber = "+7(905)761-09-52"},
                new Customer{Id = 2, FirstName = "Андрей", MiddleName = "Анатольевич", LastName = "Иванов", PassportNumber = "4444 323232", PhoneNumber = "+7(555)444-09-52" },
                new Customer{Id = 3, FirstName = "Игорь", MiddleName = "Иванович", LastName = "Сахаров", PassportNumber = "9999 010101", PhoneNumber = "+7(222)333-09-52"},
                new Customer{Id = 4, FirstName = "Светлана", MiddleName = "Ивановна", LastName = "Журавлева",  PassportNumber = "7070 88444", PhoneNumber = "+7(777)222-09-52"}
                };
            }

        }

        public async Task SaveAllAsync(IEnumerable<Customer> customers)
        {
            string fileName = "customers.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, customers);
            await createStream.DisposeAsync();
        }
    }
}
