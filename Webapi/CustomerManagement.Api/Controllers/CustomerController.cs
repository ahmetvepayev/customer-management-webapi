using CustomerManagement.Api.Extensions;
using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers;

[Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}")]
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

    [Authorize(Roles = AppRoleConstants.Admin)]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = _customerService.Delete(id);
        
        return response.GetActionResult();
    }

    [HttpDelete]
    public IActionResult MassDelete(List<int> ids)
    {
        var response = _customerService.MassDelete(ids);

        return response.GetActionResult();
    }
}