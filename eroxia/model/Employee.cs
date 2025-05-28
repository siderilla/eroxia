﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia.model
{
    internal class Employee : Person
    {
        public DateTime Dob { get; set; }

        public Employee(string fiscalCode, string name, string surname, DateTime dob) : base(fiscalCode, name, surname)
        {
            Dob = dob;
        }

        public override string? ToString()
        {
            return $"{Name} {Surname} ({FiscalCode}) - {Dob:dd/MM/yyyy}";
        }
    }
}
