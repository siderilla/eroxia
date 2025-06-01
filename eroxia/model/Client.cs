using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia.model
{
    internal class Client : Person
    {
        public Employee? Employee { get; set; }
        public string Address { get; set; }
        public string? FiscalCodeEmployee { get; set; }

        public Client(string fiscalCode, string name, string surname, string address, Employee? employee)
            : base(fiscalCode, name, surname)
        {
            Address = address;
            Employee = employee;
            FiscalCodeEmployee = employee?.FiscalCode;
        }


        public Client(string fiscalCode, string name, string surname, string address)
            : base(fiscalCode, name, surname)
        {
            Address = address;
        }

        public Client(string fiscalCode, string name, string surname, string address, string? fiscalCodeEmployee)
            : base(fiscalCode, name, surname)
        {
            Address = address;
            FiscalCodeEmployee = fiscalCodeEmployee;
        }


        public override string? ToString()
        {
            var employeeInfo = Employee != null
                ? $" (Gestito da: {Employee.Name} {Employee.Surname})"
                : " (Nessun employee associato)";

            return $"{Name} {Surname} ({FiscalCode}) - {Address}{employeeInfo}";
        }


    }
}
