using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }
        public MetaData? Meta { get; private set; }
        public List<ApiError>? Errors { get; private set; }

        private ApiResponse() { }

        public static ApiResponse<T> SuccessResponse(
            T data,
            string message = "Success",
            MetaData? meta = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Meta = meta
            };
        }

        public static ApiResponse<T> FailureResponse(
            string message,
            List<ApiError>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }

    public class MetaData
    {
        public long? ProcessingTimeMs { get; set; }
        public long? InputSize { get; set; }
        public long? OutputSize { get; set; }
        public string? ToolName { get; set; }
    }

    public class ApiError
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ApiError(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
