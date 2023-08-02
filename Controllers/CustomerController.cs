using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPAPI.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController:ControllerBase
    {
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService service)
    {
      _customerService = service;
    }
    [HttpGet]
    public IActionResult GetCustomers()
    {
      var customers = _customerService.GetCustomers();
      if(customers is null){
        return NotFound();
      }
      return Ok(customers);
    }
  }
}