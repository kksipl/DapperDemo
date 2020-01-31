using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperDemo.Model;

namespace DapperDemo.DAL.Repository
{
   public interface ITenant
    {
        IList<Tenant> GetAllTenant();
        Tenant GetTenantById(int tenantId);
        int AddTenant(Tenant tenantId);
        bool UpdateTenant(int tenantId, Tenant objTenant);
        bool DeleteTenant(int tenantId);
    }
}
