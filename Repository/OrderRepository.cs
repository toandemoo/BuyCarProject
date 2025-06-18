using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Entities;
using Project.Repository.interfaces;
using Project.Request;
using ProjectBE.Dtos;
using ProjectBE.Repository.interfaces;

namespace Project.Repository
{
   public class OrderRepository : Repository<Orders>, IOrderRepository
   {
      public OrderRepository(AppDbContext context) : base(context)
      {
      }

      public override async Task<List<Orders>> GetAllAsync()
      {
         return await _dbSet.Include(c => c.Users)
                      .Include(c => c.OrderCars)
                      .ToListAsync();
      }

      public async Task<List<Orders>> GetAllOrdersByUserID(int userid)
      {
         return await _dbSet.Include(c => c.Users)
                     .Include(c => c.OrderCars)
                     .Where(c => c.UserId == userid)
                     .ToListAsync();
      }

      public override async Task<Orders> GetByIdAsync(int id)
      {
         return await _dbSet.Include(c => c.Users)
                         .Include(c => c.OrderCars)
                         .ThenInclude(d => d.Car)
                         .Where(c => c.Id == id)
                         .FirstAsync();
      }

      public async Task<Orders> GetDetailOrder(int userid, int orderid)
      {
         var order = await _dbSet.Include(c => c.Users)
                           .Include(c => c.OrderCars)
                           .FirstOrDefaultAsync(c => c.Id == orderid && c.UserId == userid);

         return order;
      }

      public async Task UpdateOrder(OrderStatusEnum orderStatus, int orderid)
      {
         var order = await _dbSet.FindAsync(orderid);
         order.Status = orderStatus;
         await _context.SaveChangesAsync();
      }
   }
}