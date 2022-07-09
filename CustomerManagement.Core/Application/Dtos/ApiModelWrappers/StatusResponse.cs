using System.Text.Json.Serialization;

namespace CustomerManagement.Core.Application.Dtos.ApiModelWrappers;

public class StatusResponse
{
    [JsonIgnore]
    public int StatusCode { get; set; }
    
    public List<string> Errors { get; set; }

    public StatusResponse()
    {
        Errors = new List<string>();
    }

    public StatusResponse(int statusCode) : this()
    {
        StatusCode = statusCode;
    }

    public StatusResponse(int statusCode, IEnumerable<string> errors) : this(statusCode)
    {
        Errors = new List<string>(errors);
    }
}