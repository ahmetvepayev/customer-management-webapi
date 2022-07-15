using CustomerManagement.Api.Extensions;
using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class CommercialTransactionController : ControllerBase
{
    private readonly ICommercialTransactionService _commercialTransactionService;

    public CommercialTransactionController(ICommercialTransactionService commercialTransactionService)
    {
        _commercialTransactionService = commercialTransactionService;
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _commercialTransactionService.GetAll();

        return response.GetActionResult();
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = _commercialTransactionService.GetById(id);

        return response.GetActionResult();
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpGet("/api/Customers/{customerId}/[controller]s")]
    public IActionResult GetAllForCustomer(int customerId)
    {
        var response = _commercialTransactionService.GetAllForCustomer(customerId);

        return response.GetActionResult();
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public IActionResult Add(CommercialTransactionAddRequest request)
    {
        var response = _commercialTransactionService.Add(request);
        
        return response.GetActionResult();
    }

    [Authorize(Roles = $"{AppRoleConstants.Admin},{AppRoleConstants.Editor}", AuthenticationSchemes = "Bearer")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, CommercialTransactionUpdateRequest request)
    {
        var response = _commercialTransactionService.Update(id, request);
        
        return response.GetActionResult();
    }

    [Authorize(Roles = AppRoleConstants.Admin, AuthenticationSchemes = "Bearer")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = _commercialTransactionService.Delete(id);
        
        return response.GetActionResult();
    }
}