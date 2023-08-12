using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPAPI.Services.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ASPAPI.Controllers
{   
    // [EnableCors("corsPolicy1")] //make this whole controller affected by this policy
    // [DisableCors]  //this particular controller with the action method cannot be accessed for any application or domain.
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController:ControllerBase
    {
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService service)
    {
      _customerService = service;
    }

    [AllowAnonymous] //it can ignore the authentication
    [HttpGet("getallcustomers")]
    public async Task<IActionResult> GetCustomers()
    {
      var customers = await _customerService.GetCustomers();
      if(customers is null){
        return NotFound();
      }
      return Ok(customers);
    }

    [DisableRateLimiting]
    [HttpGet("GetCustomer")]
    public async Task<IActionResult> GetCustomerByCode(string code)
    {
      return Ok(await _customerService.GetCustomerByCode(code));
    }

    [HttpPost("customers")]
    public async Task<IActionResult> AddCustomer(AddCustomerDto addCustomerDto)
    {
      return Ok(await _customerService.AddCustomer(addCustomerDto));
    }

    // [EnableCors("corsPolicy1")] //to influence this method only
    [HttpDelete("delete-customer")]
    public async Task<IActionResult> RemoveCustomerByCode( string code)
    {
      return Ok(await _customerService.RemoveCustomerByCode(code));
    }

    [HttpPut("update-customer")]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto, string code)
    {
      return Ok(await _customerService.UpdateCustomer(updateCustomerDto, code));
    }
  }
}