using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Model
{
    /// <summary>
    /// POCO (Plain Old CLR Objects) класс для хранения всей информации о клиенте.
    /// По классике он не содержит метдов, и даже конструктора.
    /// Id не используется в приложении, но при подключении БД будет нужен. 
    /// Guid не исползуем всесто Id для простоты реализации
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassportNumber { get; set; }
        public DateTime? LastEdited { get; set; }
        public string? EditedBy { get; set; }
    }
}
