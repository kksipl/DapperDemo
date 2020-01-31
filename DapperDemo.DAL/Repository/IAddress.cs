using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperDemo.Model;

namespace DapperDemo.DAL.Repository
{
    public interface IAddress
    {
        IList<Address> GetAllAddress();
        Address GetAddressById(int addressId);
        List<Address> GetAddressByTenantId(int tenantId);
        int AddAddress(Address tenantId);
        bool UpdateAddress(int addressId, Address objAddress);
        bool DeleteAddress(int addressId);
    }
}
