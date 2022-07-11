using CustomerManagement.Core.Application.Dtos.EntityDtos.CommercialTransactionDtos;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers;

[ApiController]
[Route("[controller]s")]
public class CommercialTransactionController : ControllerBase
{
    private readonly ICommercialTransactionService _commercialTransactionService;

    public CommercialTransactionController(ICommercialTransactionService commercialTransactionService)
    {
        _commercialTransactionService = commercialTransactionService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _commercialTransactionService.GetAll();

        if (response.Data == null && (response.Errors == null || !response.Errors.Any()))
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = _commercialTransactionService.GetById(id);

        if (response.Data == null && (response.Errors == null || !response.Errors.Any()))
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode =  response.StatusCode};
    }

    [HttpGet("/Customers/{customerId}/[controller]s")]
    public IActionResult GetAllForCustomer(int customerId)
    {
        var response = _commercialTransactionService.GetAllForCustomer(customerId);

        if (response.Data == null && (response.Errors == null || !response.Errors.Any()))
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode =  response.StatusCode};
    }

    [HttpPost]
    public IActionResult Add(CommercialTransactionAddRequest request)
    {
        var response = _commercialTransactionService.Add(request);
        if (response.Errors == null || !response.Errors.Any())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CommercialTransactionUpdateRequest request)
    {
        var response = _commercialTransactionService.Update(id, request);
        if (response.Errors == null || !response.Errors.Any())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = _commercialTransactionService.Delete(id);
        if (response.Errors == null || !response.Errors.Any())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }
}