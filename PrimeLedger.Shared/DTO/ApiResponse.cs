using System;

namespace PrimeLedger.Shared.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        // ✅ Correct usage of the generic type parameter
        public T Data { get; set; }
    }
}
