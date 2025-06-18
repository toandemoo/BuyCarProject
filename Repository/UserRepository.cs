using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Entities;
using Project.Repository;
using ProjectBE.DTOs.Request;

namespace Project.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<Users> GetByEmailAsync(String email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateUserAsync(UpdateUserRequest user, int userId)
        {
            var existingUser = await _dbSet.FindAsync(userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                // existingUser.Email = user.Email;
                // existingUser.Password = user.Password; // Consider hashing the password
                existingUser.PhoneNumber = user.PhoneNumber;
                // existingUser.Address = user.Address;
                existingUser.profileImage = user.ProfileImage;

                _context.Entry(existingUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }
        }
    }
}