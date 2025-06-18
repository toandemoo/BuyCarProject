using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using Project.Repository;
using ProjectBE.Dtos;

namespace ProjectBE.Repository.interfaces
{
   public interface IWishlistRepository : IRepository<WishList>
   {
      Task<List<WishlistDTO>> GetWishlistByUserIdAsync(int userId);
      Task DeleteAsync(int userId, int carId);
      Task UpdateAsync(int userId, int carId, int quantity);

   }
}