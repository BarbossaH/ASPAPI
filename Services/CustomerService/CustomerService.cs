using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Services.CustomerService
{
  public class CustomerService : ICustomerService
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CustomerService(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetCustomerDto>>> GetCustomers()
    {
      ServiceResponse<List<GetCustomerDto>> _response = new ServiceResponse<List<GetCustomerDto>> ();
      try
      {
         var customers = await _context.Customers.ToListAsync();
          if(customers is not null )
          {  
             _response.Data = _mapper.Map<List<Customer>, List<GetCustomerDto>>(customers);
          }
      }
      catch (System.Exception)
      {        
        throw new Exception("Not customer in Database");
      }    
  
      return _response;
    }

    public async Task<ServiceResponse<GetCustomerDto>> GetCustomerByCode(string code)
    {
      ServiceResponse<GetCustomerDto> _response = new ServiceResponse<GetCustomerDto> ();
      try{
        var customers = await _context.Customers.ToListAsync();
        var customer = customers.FirstOrDefault(c => c.Code == code);
        if(customer is null){
          _response.Success = false;
          _response.ResponseCode = 400;
          _response.Message = $"Code:{code} not found";
        }else{
          _response.Success = true;
          _response.Message = $"Code:{code} found";
          // _response.Data = _mapper.Map<GetCustomerDto>(customer);
          _response.Data = _mapper.Map<Customer, GetCustomerDto>(customer);
        }
      }
      catch(Exception ex){
        _response.Success = false;
        _response.Message = ex.Message;
      }
      return _response;
    }
    public async Task<ServiceResponse<List<GetCustomerDto>>> RemoveCustomerByCode(string code)
    {
      ServiceResponse<List<GetCustomerDto>> _response = new ServiceResponse<List<GetCustomerDto>> ();
      return _response;
    }

       public async Task<ServiceResponse<List<GetCustomerDto>>> AddCustomer(AddCustomerDto addCustomerDto)
        {
        ServiceResponse<List<GetCustomerDto>> _response = new();
      try {
        Customer customer = _mapper.Map<AddCustomerDto, Customer>(addCustomerDto);
         _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        _response.Success = true;
        _response.Message = "Success";
        var dbCustomers = await _context.Customers.ToListAsync();
        _response.Data = dbCustomers.Select(c=>_mapper.Map<GetCustomerDto>(c)).ToList();
       }
      catch { }
      return _response;
        }

        public async Task<ServiceResponse<GetCustomerDto>> UpdateCustomer(UpdateCustomerDto updateCustomerDto, string code)
        {
            throw new NotImplementedException();
        }
    }
}