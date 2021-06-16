using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Ifarstructures
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        bool CheckEmployeeCodeExist(string EmployeeCode);

        //List<Employee> Search(string s);

        //List<Employee> Paging(int indexStart, int count);
    }
}
