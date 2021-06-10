using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Ifarstructures
{
    public interface IEmployeeRepository
    {
        // lấy ra tất cả nhân viên trong database
        List<Employee> GetAll();

        // lấy ra nhân viên có mã là EmployeeId
        Employee GetById(Guid EmployeeId);

        List<Employee> Search(string s);

        List<Employee> Paging(int indexStart, int count);
    }
}
