using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using ProjectBE.Dtos;
using ProjectBE.Dtos.Request;
using ProjectBE.Dtos.Response;

namespace ProjectBE.Services.interfaces
{
    public interface IWishlistService
    {
        public Task<WishlistResponse<List<WishlistDTO>>> GetWishlistByUserIdAsync(int userId);
        public Task<WishlistResponse<WishList>> AddToWishlistAsync(int userId, int carId);
        public Task<WishlistResponse<WishList>> RemoveFromWishlistAsync(int userId, int carId);
        public Task<WishlistResponse<WishList>> UpdateWishlistAsync(int userId, int carId, UpdateWishlistRequest quantity);
    }
}