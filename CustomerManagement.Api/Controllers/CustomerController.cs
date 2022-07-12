using CustomerManagement.Api.Extensions;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _customerService.GetAll();

        return response.GetActionResult();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = _customerService.GetById(id);

        return response.GetActionResult();
    }

    [HttpPost]
    public IActionResult Add(CustomerAddRequest request)
    {
        var response = _customerService.Add(request);
        
        return response.GetActionResult();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CustomerUpdateRequest request)
    {
        var response = _customerService.Update(id, request);
        
        return response.GetActionResult();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = _customerService.Delete(id);
        
        return response.GetActionResult();
    }
}