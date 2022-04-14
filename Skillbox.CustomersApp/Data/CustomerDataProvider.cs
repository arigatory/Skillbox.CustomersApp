using Skillbox.CustomersApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Data
{
    /// <summary>
    /// Контакт, которому должен следовать источник данных.
    /// Для простоты реализации он должен уметь читать все данные и записывать все данные.
    /// В реальном приложении, где используется обращение к API или базе данных, мы бы добавили больше методов
    /// для возможности работы и с единичной записью
    /// </summary>
    public interface ICustomersDataProvider
    {
        /// <summary>
        /// Запрос для получения всех клиентов из источника данных
        /// </summary>
        /// <returns>Коллекция клиентов</returns>
        Task<IEnumerable<Customer>?> GetAllAsync();

        /// <summary>
        /// Сохраняем все данные
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>Ничего не возвращаем</returns>
        Task SaveAllAsync(IEnumerable<Customer> customers);
    }

    /// <summary>
    /// Простая реализация интерфейса ICustomersDataProvider
    /// </summary>
    public class CustomerDataProvider : ICustomersDataProvider
    {
        /// <summary>
        /// имя файла, в котором будут храниться клиенты
        /// </summary>
        private readonly string _fileName = "customers.json";

        /// <summary>
        /// Реализация метода для получения всех клиентов из источника данных
        /// </summary>
        /// <returns>Коллекция клиентов</returns>
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

        /// <summary>
        /// Реализация метода для сохранения всех данных
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>Ничего не возвращаем</returns>
        public async Task SaveAllAsync(IEnumerable<Customer> customers)
        {
            string fileName = "customers.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, customers);
            await createStream.DisposeAsync();
        }
    }
}
