using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }
        public static ApiResponse<T> FailureResponse(string message)
        {
            return new ApiResponse<T> { Success = false, Data = default(T), Message = message };
        }
    }
}
