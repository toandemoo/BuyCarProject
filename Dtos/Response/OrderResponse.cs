using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Response
{
    public class OrderResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public OrderResponse() { }

        public OrderResponse(T data, bool success = true, string? message = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}