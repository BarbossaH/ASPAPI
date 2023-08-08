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
    private readonly ILogger<CustomerService> _logger;
    public CustomerService(DataContext context, IMapper mapper,ILogger<CustomerService> logger)
    {
      _context = context;
      _mapper = mapper;
      _logger = logger;
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
      ServiceResponse<List<GetCustomerDto>> _response = new();
      try
      {
        var customer = await _context.Customers.FindAsync(code);
        if(customer!=null){
          //remove from cache
          _context.Customers.Remove(customer);
          //save the result after removing.
          await _context.SaveChangesAsync();
          _response.Success = true;
        var customers = await _context.Customers.ToListAsync();
        _response.Data = customers.Select(c => _mapper.Map<GetCustomerDto>(c)).ToList();
        }else{
          _response.ResponseCode = 404;
          _response.Message = "Data not found";
        }
  
      }
      catch (Exception ex)
      {
        _response.Success = false;
        _response.Message = ex.Message;
      }
      return _response;
    }

      public async Task<ServiceResponse<List<GetCustomerDto>>> AddCustomer(AddCustomerDto addCustomerDto)
      {
        ServiceResponse<List<GetCustomerDto>> _response = new();
      try {
        _logger.LogInformation("Add a customer");
        Customer customer = _mapper.Map<AddCustomerDto, Customer>(addCustomerDto);
         _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        _response.Success = true;
        _response.ResponseCode = 201;
        _response.Message = "Success";
        //get the newest data from database and then send it to front end
        var dbCustomers = await _context.Customers.ToListAsync();
        _response.Data = dbCustomers.Select(c=>_mapper.Map<GetCustomerDto>(c)).ToList();
       }
      catch(Exception ex) {
        _response.ResponseCode = 400;
        _response.Message = ex.Message;
        _logger.LogError(ex.Message,ex);

       }
      return _response;
   }

        public async Task<ServiceResponse<List<GetCustomerDto>>> UpdateCustomer(UpdateCustomerDto updateCustomerDto, string code)
        {
          ServiceResponse<List<GetCustomerDto>> _response = new();
          try
          {
        // var customer = await _context.Customers.FindAsync(code);
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Code == code);
        if(customer is null){
          _response.Message = "Code not found";
          _response.Success = false;
        }
        else{
          _mapper.Map(updateCustomerDto, customer);
          await _context.SaveChangesAsync();
          var customers = await _context.Customers.ToListAsync();
          _response.Data = customers.Select(c=>_mapper.Map<GetCustomerDto>(c)).ToList() ;
        }
          }
          catch (Exception ex)
          {

        _response.Message = ex.Message;
        _response.Success = false;
          }
      return _response;
        }
    }
}