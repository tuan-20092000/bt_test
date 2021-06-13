using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Ifarstructures;
using MISA.Core.Interfaces.IServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        ServiceResult serviceResult = new ServiceResult();
        public EmployeeService(IEmployeeRepository employeeRepository) :base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public override ServiceResult Insert(Employee employee)
        {
            var employeeCode = employee.EmployeeCode;
            if (CheckEmployeeCodeExist(employeeCode))
            {
                serviceResult.isValid = false;
                serviceResult.Messengers.Add($"Mã nhân viên <{employeeCode}> đã tồn tại, vui lòng kiểm tra lại.");
                serviceResult.Data.Add(employee);
                serviceResult.VFieldError = "Mã nhân viên";
                serviceResult.EFieldError = "EmployeeCode";
                return serviceResult;
            }
            return _employeeRepository.Insert(employee);
        }

        public bool CheckEmployeeCodeExist(string EmployeeCode)
        {
            return _employeeRepository.CheckEmployeeCodeExist(EmployeeCode);
        }
    }


}
