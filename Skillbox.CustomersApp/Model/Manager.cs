using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Model
{
    /// <summary>
    /// От менеджера нам нужно знать как он называется, чтобы сохранять в файл информцию о действиях пользователя.
    /// Весь основной функционал реализует ManagerViewModel, в БД, файле и т.п. нам не нужно ничего особо хранить того,
    /// что связано с пользователем.
    /// </summary>
    public class Manager : User
    {
        /// <summary>
        /// Переопределяем метод, чтобы пользователь назывался менеджером в истории действий.
        /// </summary>
        /// <returns>
        /// Возвращает "менеджер" вместо "пользователь". При дальнейшем развитии приложения
        /// может возвращать конкретного человека, который работает менеджером
        /// </returns>
        public override string GetTitle() => "менеджер";
    }
}
