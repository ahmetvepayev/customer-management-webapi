using CustomerManagement.Core.Application.Dtos.EntityDtos.CustomerDtos;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers;

[ApiController]
[Route("[controller]s")]
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
        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    [HttpPost]
    public IActionResult Add(CustomerAddRequest request)
    {
        var response = _customerService.Add(request);
        if (response.Errors == null || !response.Errors.Any())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }
}