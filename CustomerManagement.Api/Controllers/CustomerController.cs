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

        if (response.Data == null && (response.Errors == null || !response.Errors.Any()))
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = _customerService.GetById(id);

        if (response.Data == null && (response.Errors == null || !response.Errors.Any()))
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode =  response.StatusCode};
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

    [HttpPut("{id}")]
    public IActionResult Update(int id, CustomerUpdateRequest request)
    {
        var response = _customerService.Update(id, request);
        if (response.Errors == null || !response.Errors.Any())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = _customerService.Delete(id);
        if (response.Errors == null || !response.Errors.Any())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }
}