using DapperDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace DapperDemo.DAL.Repository
{
   public class TenantRepository : ITenant
    {
        ConnectionFactory _objConnection = new ConnectionFactory("Data Source=DESKTOP-LPMM462;Initial Catalog=TenantDb;User ID=sa;Password=123");
        public int AddTenant(Tenant objTenant)
        {
            string procName = "SP_TenantInsert";
            var param = new DynamicParameters();
            int tenantId = 0;

            param.Add("@TenantId", objTenant.TenantId, null, ParameterDirection.Output);
            param.Add("@FirstName", objTenant.FirstName);
            param.Add("@MiddleName", objTenant.MiddleName);
            param.Add("@LastName", objTenant.LastName);
            param.Add("@ContactNumber1", objTenant.ContactNumber1);
            param.Add("@ContactNumber2", objTenant.ContactNumber2);
            
            try
            {
                SqlMapper.Execute(_objConnection.GetConnection,procName, param, commandType: CommandType.StoredProcedure);

                tenantId = param.Get<int>("@TenantId");
            }
            finally
            {
                _objConnection.CloseConnection();
            }

            return tenantId;
        }

        public bool UpdateTenant(int TenantId, Tenant objTenant)
        {
            string procName = "SP_TenantUpdate";
            var param = new DynamicParameters();
            bool IsSuccess = true;

            param.Add("@TenantId", TenantId, null, ParameterDirection.Input);
            param.Add("@FirstName", objTenant.FirstName);
            param.Add("@MiddleName", objTenant.MiddleName);
            param.Add("@LastName", objTenant.LastName);
            param.Add("@ContactNumber1", objTenant.ContactNumber1);
            param.Add("@ContactNumber2", objTenant.ContactNumber2);
         
            try
            {
                var rowsAffected = SqlMapper.Execute(_objConnection.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure);
                if (rowsAffected <= 0)
                {
                    IsSuccess = false;
                }
            }
            finally
            {
                _objConnection.CloseConnection();
            }

            return IsSuccess;
        }

        public bool DeleteTenant(int tenantId)
        {
            bool IsDeleted = true;
            var SqlQuery = @"DELETE FROM tblTenant WHERE TenantId = @TenantId";

            using (IDbConnection conn = _objConnection.GetConnection)
            {
                var rowsaffected = conn.Execute(SqlQuery, new { TenantId = tenantId });
                if (rowsaffected <= 0)
                {
                    IsDeleted = false;
                }
            }
            return IsDeleted;
        }

        public Tenant GetTenantById(int tenantId)
        {
            var objTenant = new Tenant();
            var procName = "SP_GetTenantById";
            var param = new DynamicParameters();
            param.Add("@TenantId", tenantId);

            try
            {
                using (var multiResult = SqlMapper.QueryMultiple(_objConnection.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure))
                {
                    objTenant = multiResult.ReadFirstOrDefault<Tenant>();
                }
            }
            finally
            {
                _objConnection.CloseConnection();
            }

            return objTenant;
        }

        public IList<Tenant> GetAllTenant()
        {
            var EmpList = new List<Tenant>();
            var SqlQuery = @"SELECT [TenantId],[FirstName],[MiddleName],[LastName],[ContactNumber1],[ContactNumber2] FROM [tblTenant]";

            using (IDbConnection conn = _objConnection.GetConnection)
            {
                var result = conn.Query<Tenant>(SqlQuery);
                return result.ToList();
            }
        }
    }
}
