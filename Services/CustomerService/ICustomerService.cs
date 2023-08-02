using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Services.CustomerService
{
    public interface ICustomerService
    {
    List<Customer> GetCustomers();
    }
}