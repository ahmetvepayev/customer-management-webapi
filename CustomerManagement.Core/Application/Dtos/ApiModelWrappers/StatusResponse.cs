using System.Text.Json.Serialization;

namespace CustomerManagement.Core.Application.Dtos.ApiModelWrappers;

public class StatusResponse
{
    [JsonIgnore]
    public int StatusCode { get; set; }
    
    public List<string> Errors { get; set; }

    public StatusResponse(IEnumerable<string> errors = null)
    {
        Errors = (errors == null) ? new List<string>() : new List<string>(errors);
    }

    public StatusResponse(int statusCode, IEnumerable<string> errors = null) : this(errors)
    {
        StatusCode = statusCode;
    }
}