namespace shopveeAPI.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(string message, int statusCode, T data)
        {
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }
        
        public ApiResponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
            Data = default!;
        }
        
        public ApiResponse()
        {
            Message = default!;
            StatusCode = default!;
            Data = default!;
        }

        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}