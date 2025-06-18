using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using ProjectBE.DTOs.Request;

namespace Project.Repository
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users> GetByEmailAsync(String email);
        public Task UpdateUserAsync(UpdateUserRequest user, int userId);
    }
}