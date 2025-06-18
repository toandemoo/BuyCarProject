using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBE.Dtos.Request;
using ProjectBE.Services.interfaces;

namespace ProjectBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly ILogger<WishlistController> _logger;

        private readonly IWishlistService _wishlistService;

        public WishlistController(ILogger<WishlistController> logger, IWishlistService wishlistService)
        {
            _logger = logger;
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlistByUserIdAsync()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);

            if (wishlist == null)
            {
                return NotFound($"Wishlist not found for user with ID {userId}");
            }

            return Ok(wishlist);
        }

        [HttpPost("{carId}")]
        public async Task<IActionResult> AddToWishlistAsync([FromRoute] int carId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var result = await _wishlistService.AddToWishlistAsync(userId, carId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{carId}")]
        public async Task<IActionResult> RemoveFromWishlistAsync([FromRoute] int carId)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var result = await _wishlistService.RemoveFromWishlistAsync(userId, carId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPatch("{carId}")]
        public async Task<IActionResult> UpdateWishlistAsync([FromRoute] int carId, [FromBody] UpdateWishlistRequest quantity)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var result = await _wishlistService.UpdateWishlistAsync(userId, carId, quantity);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}