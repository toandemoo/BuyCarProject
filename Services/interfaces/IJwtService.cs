using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.DTOs.Request;
using Project.DTOs.Response;
using Project.Entities;

namespace ProjectBE.Services.interfaces
{
    public interface IJwtService
    {
        public Task<RegisterResponse> Register(RegisterRequest dto);
        public Task<LoginResponse> Login(LoginRequest dto);
        public Task<VerifiedResponse> Verified(string token);
        public Task<string> GenerateJwtToken(Users user);
        public Task<LoginResponse> ValidateRefreshToken(string token);
        public Task<string> GenerateRefreshToken(int userId);
        public Task<ChangePasswordResponse> ChangePassword(int userid, ChangePasswordRequest changePasswordRequest);
    }
}