using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Model
{
    /// <summary>
    /// Представляет того, кто будет работать в приложении.
    /// Можно сделать абстрактным, но не обязательно.
    /// </summary>
    public class User
    {
        public virtual string GetTitle() => "неизвестный пользователь";
    }
}
