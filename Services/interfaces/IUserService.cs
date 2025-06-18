using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.DTOs.Request;
using Project.DTOs.Response;
using Project.Entities;
using ProjectBE.Dtos.Response;
using ProjectBE.DTOs.Request;

namespace Project.Services.interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetUserById(int id);
        Task<UpdateUserResponse> Update(UpdateUserRequest dto, int userId);
        Task Delete(int id);
    }
}