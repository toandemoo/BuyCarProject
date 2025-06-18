using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Project.DTOs;
using Project.DTOs.Request;
using Project.Entities;
using Project.Repository;
using Project.Services;
using Project.Services.interfaces;
using ProjectBE.DTOs.Request;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var user = await _userService.GetUserById(id);
            if (user is null)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest dto)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var result = await _userService.Update(dto, userId);
            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _userService.GetUserById(id);
            if (user is null)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok("Xóa User thành công");
        }

    }
}

