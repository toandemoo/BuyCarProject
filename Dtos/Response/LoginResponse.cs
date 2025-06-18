using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTOs
{
    public class LoginResponse
    {
        public string? message { get; set; }
        public string? accesstoken { get; set; }
        public string? refreshtoken { get; set; }
        public bool Status { get; set; }
    }
}