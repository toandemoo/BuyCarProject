using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using Project.Models.Pagination;

namespace Project.Response
{
    public class BrandResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public BrandResponse() { }

        public BrandResponse(T data, bool success = true, string? message = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}