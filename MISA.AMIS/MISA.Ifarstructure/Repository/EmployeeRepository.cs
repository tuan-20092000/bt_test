using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Ifarstructures;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Ifarstructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // chuỗi kết nối database
        string connectionString = "" +
            "Host = 47.241.69.179;" +
            "Port = 3306;" +
            "Database = MF848_AMIST_NHDUONG;" +
            "User Id = nvmanh;" +
            "Password = 12345678";
        IDbConnection dbConnection;

        public EmployeeRepository()
        {
            dbConnection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Hàm lấy tất cả nhân viên
        /// CreatedBy: Tuấn Nguyễn (27/05/2021)
        /// </summary>
        /// <returns>Tất cả nhân viên</returns>
        public List<Employee> GetAll()
        {
            var sqlQuery = "Select * from Employee";
            var employees = dbConnection.Query<Employee>(sqlQuery).ToList();
            return employees;
        }



        /// <summary>
        /// Hàm lấy ra nhân viên có mã là EmployeeId
        /// CreatedBy: Tuấn Nguyễn (27/05/2021)
        /// </summary>
        /// <param name="EmployeeId">Id nhân viên</param>
        /// <returns>trả về nhân viên có mã là EmployeeId hoặc null nếu ko có</returns>

        public Employee GetById(Guid EmployeeId)
        {
            var sqlQuery = $"Select * from Employee where EmployeeId = '{EmployeeId.ToString()}' ";
            var employee = dbConnection.Query<Employee>(sqlQuery).FirstOrDefault();
            return employee;
        }

        public List<Employee> Search(string s)
        {
            var sqlCommand = $"Select * from employee where EmployeeCode like '%{s}%' or FullName like '%{s}%' order by DateAdd DESC";
            var employees = dbConnection.Query<Employee>(sqlCommand).ToList();
            return employees;
        }

        public List<Employee> Paging (int indexStart, int count)
        {
            var sqlCommand = $"Select * from employee order by DateAdd DESC Limit {indexStart},{count}";
            var employees = dbConnection.Query<Employee>(sqlCommand).ToList();
            return employees;
        }
    }
}
