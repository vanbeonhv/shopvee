namespace shopveeAPI.Common;

public class ServiceResponse
{
    public object? Data { get; init; } 
    public int StatusCode { get; set; }
    public bool IsSucceed { get; set; }
    public string Message { get; init; }

    private ServiceResponse()
    {
    }
 
    public static ServiceResponse Succeed(int statusCode, object data, string message = "Success")
    {
        return new ServiceResponse()
        {
            IsSucceed = true,
            StatusCode = statusCode,
            Data = data,
            Message = message,
        };
    }

    public static ServiceResponse Fail(int statusCode, object? data, string message = "Failed")
    {
        return new ServiceResponse()
        {
            IsSucceed = false,
            StatusCode = statusCode,
            Data = data,
            Message = message,
        };
    }
    
    public T? GetData<T>()
    {
        try
        {
            var result = Convert.ChangeType(Data, typeof(T));
            return (T)result!;
        }
        catch (InvalidCastException)
        {
            throw new InvalidCastException("Cannot cast data to the specified type");
        } 
    }
}