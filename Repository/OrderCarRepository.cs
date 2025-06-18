using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Entities;
using Project.Repository;
using ProjectBE.Repository.interfaces;

namespace ProjectBE.Repository
{
    public class OrderCarRepository : Repository<OrderCars>, IOrderCarRepository
    {
        public OrderCarRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<OrderCars>> GetOrderCarsByOrderId(int orderid)
        {
            return await _dbSet.Include(c => c.Car)
                   .Where(c => c.OrderId == orderid)
                   .ToListAsync();
        }
    }
}