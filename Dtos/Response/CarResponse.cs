using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;

namespace Project.Response
{
    public class CarResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public CarResponse() { }

        public CarResponse(T data, bool success = true, string? message = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}