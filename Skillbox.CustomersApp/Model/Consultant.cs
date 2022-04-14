using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Model
{
    /// <summary>
    /// От консультанта нам нужно знать как он называется, чтобы сохранять в файл информцию о действиях пользователя.
    /// Весь основной функционал реализует ConsultantViewModel, в БД, файле и т.п. нам не нужно ничего особо хранить того,
    /// что связано с пользователем.
    /// </summary>
    public class Consultant : User
    {
        /// <summary>
        /// Переопределяем метод, чтобы пользователь назывался консультантом в истории действий.
        /// </summary>
        /// <returns>
        /// Возвращает "консультант" вместо "пользователь". При дальнейшем развитии приложения
        /// может возвращать конкретного человека, который работает консультантом
        /// </returns>
        public override string GetTitle() => "консультант";
    }
}
