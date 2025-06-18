using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;
using Project.Repository;
using Microsoft.AspNetCore.Mvc;
using Project.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Project.Services.interfaces;
using Project.Models;
using Project.DTOs.Response;
using Project.DTOs.Request;
using ProjectBE.DTOs.Request;
using ProjectBE.Dtos.Response;

namespace Project.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        private readonly IMailService _mailService;

        public UserService(IMailService mailService, IUserRepository userRepository, IConfiguration configuration, ILogger<UserService> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError("Lỗi lấy tất cả user: {Message}", e.Message);
                throw;
            }
        }

        public async Task<Users> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<UpdateUserResponse> Update(UpdateUserRequest dto, int userId)
        {
            var current_user = await _userRepository.GetByIdAsync(userId);
            if (current_user == null)
                return new UpdateUserResponse { Status = false, Message = "User not found" };

            await _userRepository.UpdateUserAsync(dto, userId);

            return new UpdateUserResponse { Status = true, Message = "User updated successfully" };
        }

        public async Task Delete(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("Lỗi xóa User: {Message}", e.Message);
            }
        }
    }
}