using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Entities;

namespace Project.Repository
{
   public class Repository<T> : IRepository<T> where T : class
   {
      public readonly AppDbContext _context;
      public DbSet<T> _dbSet;

      public Repository(AppDbContext context)
      {
         _context = context;
         _dbSet = _context.Set<T>();
      }

      public virtual async Task<List<T>> GetAllAsync()
      {
         return await _dbSet.ToListAsync();
      }

      public virtual async Task<T> GetByIdAsync(int id)
      {
         return await _dbSet.FindAsync(id);
      }

      public virtual async Task AddAsync(T entity)
      {
         await _dbSet.AddAsync(entity);
         await _context.SaveChangesAsync();
      }

      public virtual async Task UpdateAsync(T entity, int id)
      {
         var existingEntity = await _dbSet.FindAsync(id);
         if (existingEntity == null)
            throw new KeyNotFoundException($"Entity with ID {id} not found");

         _context.Entry(existingEntity).CurrentValues.SetValues(entity);
         await _context.SaveChangesAsync();
      }

      public async Task DeleteAsync(int id)
      {
         var entity = await _dbSet.FindAsync(id);
         if (entity != null)
         {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
         }
      }
   }
}