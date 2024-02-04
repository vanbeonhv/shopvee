namespace shopveeAPI.Responses
{
    public class ApiResponse
    {
        public ApiResponse(string message, int statusCode, object? data = null)
        {
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }

        public string Message { get; set; }
        public int StatusCode { get; set; }
        public object? Data { get; set; }
    }
}