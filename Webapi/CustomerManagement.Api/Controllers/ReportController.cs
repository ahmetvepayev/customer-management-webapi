using CustomerManagement.Api.Extensions;
using CustomerManagement.Core.Application.Auth;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers;

[Authorize(Roles = AppRoleConstants.Admin)]
[ApiController]
[Route("api/[controller]s")]
public class ReportController : ControllerBase
{
    private readonly IReportsListService _reportsListService;

    public ReportController(IReportsListService reportsListService)
    {
        _reportsListService = reportsListService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = _reportsListService.GetAll();

        return response.GetActionResult();
    }
    
    [HttpGet("{id}")]
    public IActionResult DownloadFile(int id)
    {
        var stream = _reportsListService.GetFileByIdInStream(id);

        return File(stream, "application/octet-stream");
    }
}