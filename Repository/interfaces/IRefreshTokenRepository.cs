using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Repository;
using ProjectBE.Entities;

namespace ProjectBE.Repository.interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        public Task<RefreshToken> GetByTokenAsync(string token);
    }
}