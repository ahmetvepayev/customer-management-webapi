namespace CustomerManagement.Core.Application.Dtos.ApiModelWrappers;

public class ObjectResponse<T> : StatusResponse
{
    public T Data { get; set; }

    public ObjectResponse() : base()
    {
        
    }

    public ObjectResponse(int statusCode) : base(statusCode)
    {
        
    }

    public ObjectResponse(int statusCode, IEnumerable<string> errors) : base(statusCode, errors)
    {
        
    }

    public ObjectResponse(int statusCode, T data) : base()
    {
        Data = data;
    }
}