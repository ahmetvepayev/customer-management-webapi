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
        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = _commercialTransactionService.GetById(id);
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
}