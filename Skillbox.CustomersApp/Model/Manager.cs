﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox.CustomersApp.Model
{
    public class Manager : User
    {
        public override string GetTitle() => "менеджер";
    }
}
