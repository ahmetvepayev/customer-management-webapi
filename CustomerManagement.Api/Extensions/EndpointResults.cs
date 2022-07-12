using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Utility.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Extensions;

public static class EndpointResults
{
    public static IActionResult GetActionResult(this StatusResponse response)
    {
        if (response.Errors.IsNullOrEmpty())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode = response.StatusCode};
    }

    public static IActionResult GetActionResult<T>(this ObjectResponse<T> response)
    {
        if (response.Data == null && response.Errors.IsNullOrEmpty())
        {
            return new StatusCodeResult(response.StatusCode);
        }

        return new ObjectResult(response){StatusCode =  response.StatusCode};
    }
}