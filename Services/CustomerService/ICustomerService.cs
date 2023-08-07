using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ASPAPI.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<GetCustomerDto>>> GetCustomers();
        Task<ServiceResponse<GetCustomerDto>> GetCustomerByCode(string code);
        Task<ServiceResponse<List<GetCustomerDto>>> RemoveCustomerByCode(string code);
        Task<ServiceResponse<List<GetCustomerDto>>> AddCustomer(AddCustomerDto addCustomerDto);
        Task<ServiceResponse<GetCustomerDto>> UpdateCustomer(UpdateCustomerDto updateCustomerDto, string code);
    }
}