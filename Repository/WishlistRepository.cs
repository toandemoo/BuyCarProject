using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.DTOs;
using Project.Entities;
using Project.Repository;
using ProjectBE.Dtos;
using ProjectBE.Repository.interfaces;

namespace ProjectBE.Repository
{
   public class WishlistRepository : Repository<WishList>, IWishlistRepository
   {
      public WishlistRepository(AppDbContext context) : base(context)
      {
      }

      public async Task<List<WishlistDTO>> GetWishlistByUserIdAsync(int userId)
      {
         return await _context.wishLists
            .Where(w => w.Userid == userId)
            .Include(w => w.Cars)
            .Select(w => new WishlistDTO
            {
               UserId = userId,
               Quantity = w.Quantity,
               Car = new CarDTO(w.Cars)
            }).ToListAsync();

      }

      public async Task DeleteAsync(int userId, int carId)
      {
         var wishlist = await _dbSet.FindAsync(userId, carId);
         if (wishlist != null)
         {
            _dbSet.Remove(wishlist);
            await _context.SaveChangesAsync();
         }
         else
         {
            throw new KeyNotFoundException($"Wishlist with User ID {userId} and Car ID {carId} not found");
         }
      }

      public async Task UpdateAsync(int userId, int carId, int quantity)
      {
         var wishlist = await _dbSet.FindAsync(userId, carId);
         if (wishlist != null)
         {
            wishlist.Quantity = quantity;
            _context.Entry(wishlist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
         }
         else
         {
            throw new KeyNotFoundException($"Wishlist with User ID {userId} and Car ID {carId} not found");
         }
      }
   }
}