using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Repository;
using ProjectBE.Entities;
using ProjectBE.Repository.interfaces;

namespace ProjectBE.Repository
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context) : base(context) { }

        public Task<RefreshToken> GetByTokenAsync(string token)
        {
            return _dbSet.FirstOrDefaultAsync(rt => rt.Token == token);
        }
    }
}