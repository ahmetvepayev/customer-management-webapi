namespace CustomerManagement.Core.Application.Dtos.ApiModelWrappers;

public class ObjectResponse<T> : StatusResponse
{
    public T Data { get; set; }

    public ObjectResponse(List<string> errors = null) : base(errors)
    {
        
    }

    public ObjectResponse(T data) : base()
    {
        Data = data;
    }

    public ObjectResponse(int statusCode, List<string> errors = null) : base(statusCode, errors)
    {
        
    }

    public ObjectResponse(T data, int statusCode, List<string> errors = null) : base(statusCode, errors)
    {
        Data = data;
    }
}