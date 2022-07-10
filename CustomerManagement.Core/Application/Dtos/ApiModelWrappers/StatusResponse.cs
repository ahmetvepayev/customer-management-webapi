using System.Text.Json.Serialization;

namespace CustomerManagement.Core.Application.Dtos.ApiModelWrappers;

public class StatusResponse
{
    [JsonIgnore]
    public int StatusCode { get; set; }
    
    public List<string> Errors { get; set; }

    public StatusResponse(List<string> errors = null)
    {
        Errors = errors ?? new();
    }

    public StatusResponse(int statusCode, List<string> errors = null) : this(errors)
    {
        StatusCode = statusCode;
    }
}