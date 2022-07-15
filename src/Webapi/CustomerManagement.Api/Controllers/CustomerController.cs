using CustomerManagement.Api.Extensions;
using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _customerService.GetAll();

        return response.GetActionResult();
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = _customerService.GetById(id);

        return response.GetActionResult();
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public IActionResult Add(CustomerAddRequest request)
    {
        var response = _customerService.Add(request);
        
        return response.GetActionResult();
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, CustomerUpdateRequest request)
    {
        var response = _customerService.Update(id, request);
        
        return response.GetActionResult();
    }

    [Authorize(Roles = AppRoleConstants.Admin, AuthenticationSchemes = "Bearer")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = _customerService.Delete(id);
        
        return response.GetActionResult();
    }

    [Authorize(Roles = AppRoleConstants.Admin, AuthenticationSchemes = "Bearer")]
    [HttpDelete]
    public IActionResult MassDelete(List<int> ids)
    {
        var response = _customerService.MassDelete(ids);

        return response.GetActionResult();
    }
}