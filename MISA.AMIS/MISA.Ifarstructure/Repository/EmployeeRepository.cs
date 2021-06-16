using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public bool CheckEmployeeCodeExist(string EmployeeCode)
        {
            var procedure = $"Proc_Get{ClassName}ByCode";
            DynamicParameters dynamic = new DynamicParameters();
            dynamic.Add("@EmployeeCode", EmployeeCode);
            var res = DbConnection.Query<Employee>(procedure, dynamic, commandType: CommandType.StoredProcedure).FirstOrDefault();
            if (res != null)
            {
                return true;
            }
            return false;
        }
    }
}
