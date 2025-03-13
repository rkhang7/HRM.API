namespace HRM.API.Domain.DTOs
{
    public class ApiResponse<T>
    {
        public bool IsOK { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool isOK, string message, T data)
        {
            IsOK = isOK;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>(false, message, default);
        }


    }
}
