using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public int Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string IdentityCard { get; set; }

        public string Position { get; set; }

        public string CompanyName { get; set; }

        public string AccountNumber { get; set; }

        public string BankName { get; set; }

        public string Branch { get; set; }


    }
}
