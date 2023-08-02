using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Services.CustomerService
{
  public class CustomerService : ICustomerService
  {
    private readonly DataContext _context;
    public CustomerService(DataContext context)
    {
      _context = context;
    }
    public List<Customer> GetCustomers()
    {
      var customers = _context.Customers;
      if(customers is not null ){
        return customers.ToList();

      }else{
        throw new Exception("Not customer in Database");
      }
        

    }
  }
}