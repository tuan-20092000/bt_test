using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.IServices
{
    public interface IEmployeeService
    {
        // thêm mới 1 nhân viên
        int? Insert(Employee employee);

        // sửa thông tin của 1 nhân viên
        int? Update(Employee employee);

        // xóa 1 nhân viên
        int? Delete(string EmployeeId);
    }
}
