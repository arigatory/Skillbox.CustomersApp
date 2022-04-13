using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Model
{
    public class Consultant : User
    {
        public override string GetTitle() => "консультант";
    }
}
