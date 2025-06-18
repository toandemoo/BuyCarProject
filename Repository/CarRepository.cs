using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.DTOs;
using Project.Entities;
using Project.Repository.interfaces;

namespace Project.Repository
{
    public class CarRepository : Repository<Cars>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context) { }

        public override async Task<List<Cars>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Brands)
                .Include(c => c.CarTypes)
                .ToListAsync();
        }

        public override async Task<Cars> GetByIdAsync(int id)
        {
            return await _dbSet.Include(c => c.Brands)
                            .Include(c => c.CarTypes)
                            .FirstOrDefaultAsync(c => c.Id == id);

        }
    }
}