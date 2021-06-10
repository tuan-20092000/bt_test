using Dapper;
using MISA.Core.Entities;
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
    public class EmployeeService : IEmployeeService
    {
        // chuỗi kết nối vs database
        string connectionString = "" +
           "Host = 127.0.0.1;" +
           "Port = 3306;" +
           "Database = employee;" +
           "User Id = root;" +
           "Password = 20092902";
        IDbConnection dbConnection;

        public EmployeeService()
        {
            dbConnection = new MySqlConnection(connectionString);
        }

        /// <summary>
        ///   Hàm xóa nhân viên
        ///   CreatedBy: Tuấn Nguyễn (27/05/2021)
        /// </summary>
        /// <param name="EmployeeId">Nhân viên</param>
        /// <returns>số bản ghi bị ảnh hưởng (xóa được)</returns>
        public int? Delete(string EmployeeId)
        {
            var sqlQuery = $"Delete from employee where EmployeeId = '{EmployeeId}'";
            var rowAffects = dbConnection.Execute(sqlQuery);
            return rowAffects;
        }


        /// <summary>
        ///  Hàm thêm mới 1 nhân viên
        ///  CreatedBy: Tuấn Nguyễn (27/05/2021)
        /// </summary>
        /// <param name="employee">nhân viên</param>
        /// <returns>số bản ghi bị ảnh hưởng (thêm mới được)</returns>
        public int? Insert(Employee employee)
        {
            if (CheckCode(employee))
            {
                return 0;
            }
            employee.EmployeeId = Guid.NewGuid();
            var rowAffects = dbConnection.Execute("Proc_InsertEmployee", employee, commandType: CommandType.StoredProcedure);
            return rowAffects;
        }

        // hàm check xem có trùng mã không
        // trả về true nếu trùng, false nếu không trùng (dùng cho Insert)
        public bool CheckCode(Employee employee)
        {
            var EmployeeId = employee.EmployeeId.ToString();
            var EmployeeCode = employee.EmployeeCode;
            var sqlQuery = "Select * from employee " +
                $"where EmployeeCode = '{EmployeeCode}'";
            var res = dbConnection.Query<Employee>(sqlQuery).FirstOrDefault();
            if(res != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        ///     Hàm update nhân viên
        ///     CreatedBy: Tuấn Nguyễn (27/05/2021)
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng (update được)</returns>
        public int? Update(Employee employee)
        {
            if (CheckCodeAndId(employee))
            {
                return 0;
            }
            var rowAffect = dbConnection.Execute("Proc_UpdateEmployee", employee, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }

        // hàm check xem có trùng mã không
        // trả về true nếu trùng, false nếu không trùng (dùng cho Update)
        public bool CheckCodeAndId(Employee employee)
        {
            var EmployeeId = employee.EmployeeId.ToString();
            var EmployeeCode = employee.EmployeeCode;
            var sqlQuery = "Select * from employee " +
                $"where EmployeeCode = '{EmployeeCode}' and EmployeeId != '{EmployeeId}' ";
            var res = dbConnection.Query<Employee>(sqlQuery).FirstOrDefault();
            if (res != null)
            {
                return true;
            }
            return false;
        }
    }


}
