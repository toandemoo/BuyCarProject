using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Project.DTOs;
using Project.Entities;
using Project.Repository;
using Project.Repository.interfaces;
using ProjectBE.Dtos;
using ProjectBE.Dtos.Request;
using ProjectBE.Dtos.Response;
using ProjectBE.Repository.interfaces;
using ProjectBE.Services.interfaces;

namespace ProjectBE.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly ILogger<WishlistService> _logger;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _usersRepository;

        public WishlistService(IWishlistRepository wishlistRepository, ILogger<WishlistService> logger, ICarRepository carRepository, IUserRepository usersRepository)
        {
            _wishlistRepository = wishlistRepository;
            _logger = logger;
            _carRepository = carRepository;
            _usersRepository = usersRepository;
        }

        public async Task<WishlistResponse<List<WishlistDTO>>> GetWishlistByUserIdAsync(int userId)
        {
            var wishlist = await _wishlistRepository.GetWishlistByUserIdAsync(userId);

            if (wishlist == null)
            {
                throw new KeyNotFoundException($"Wishlist not found for user with ID {userId}");
            }

            return new WishlistResponse<List<WishlistDTO>>(
                success: true,
                message: "Wishlist retrieved successfully",
                data: wishlist
            );
        }

        public async Task<WishlistResponse<WishList>> AddToWishlistAsync(int userId, int carId)
        {
            if (userId <= 0 || carId <= 0)
            {
                throw new ArgumentException("User ID and Car ID must be greater than zero");
            }

            var car = await _carRepository.GetByIdAsync(carId);

            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found");
            }
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            await _wishlistRepository.AddAsync(new WishList
            {
                Userid = userId,
                Carid = carId,
                Quantity = 1,
                CreatedAt = DateTime.UtcNow,
                Cars = car,
                Users = user
            });

            return new WishlistResponse<WishList>(
                success: true,
                message: "Car added to wishlist successfully",
                data: null
            );
        }

        public async Task<WishlistResponse<WishList>> RemoveFromWishlistAsync(int userId, int carId)
        {
            if (userId <= 0 || carId <= 0)
            {
                throw new ArgumentException("User ID and Car ID must be greater than zero");
            }

            var existingWishlist = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
            if (existingWishlist == null)
            {
                throw new KeyNotFoundException($"Wishlist not found for user with ID {userId}");
            }

            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found");
            }

            await _wishlistRepository.DeleteAsync(userId, carId);

            return new WishlistResponse<WishList>(
                success: true,
                message: "Car removed from wishlist successfully",
                data: null
            );
        }

        public async Task<WishlistResponse<WishList>> UpdateWishlistAsync(int userId, int carId, UpdateWishlistRequest quantity)
        {
            if (userId <= 0 || carId <= 0)
            {
                throw new ArgumentException("User ID and Car ID must be greater than zero");
            }

            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found");
            }

            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }
            await _wishlistRepository.UpdateAsync(userId, carId, quantity.quantity);

            return new WishlistResponse<WishList>(
                success: true,
                message: "Wishlist updated successfully",
                data: null
            );
        }

    }
}