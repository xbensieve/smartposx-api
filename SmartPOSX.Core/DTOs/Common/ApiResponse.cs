﻿namespace SmartPOSX.Core.DTOs.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "")
        {
            return new ApiResponse<T> { Success = true, Message = message, Data = data };
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T> { Success = false, Message = message, Data = default };
        }

        public static ApiResponse<T> Fail(string message, T data)
        {
            return new ApiResponse<T> { Success = false, Message = message, Data = data };
        }
    }
}
