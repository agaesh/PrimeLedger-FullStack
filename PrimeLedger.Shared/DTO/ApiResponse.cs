using System;

namespace PrimeLedger.Shared.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public object? Errors { get; set; }

        // ✅ Correct usage of the generic type parameter
        public T Data { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null) =>
       new ApiResponse<T> { Success = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(string message, object? errors = null) =>
            new ApiResponse<T> { Success = false, Message = message, Errors = errors };
    }
}
