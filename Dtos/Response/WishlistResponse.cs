using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;

namespace ProjectBE.Dtos.Response
{
    public class WishlistResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }

        public WishlistResponse() { }

        public WishlistResponse(T data, bool success = true, string? message = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}