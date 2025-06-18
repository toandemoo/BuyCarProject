using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.DTOs;
using Project.DTOs.Request;
using Project.Services.interfaces;
using ProjectBE.Services.interfaces;

namespace ProjectBE.Controllers
{
    [Route("api/[controller]")]
    public class JwtController : Controller
    {
        private readonly ILogger<JwtController> _logger;

        private readonly IJwtService _jwtService;

        public JwtController(ILogger<JwtController> logger, IJwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
        {
            var result = await _jwtService.Register(dto);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var result = await _jwtService.Login(dto);

            if (!result.Status)
                return Unauthorized(result);

            // HttpContext.Session.SetString("AccessToken", result.accesstoken);

            return Ok(result);
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> Verified([FromQuery]string token)
        {
            var result = await _jwtService.Verified(token);
            if (!result.Status)
                return BadRequest(result);

            // return Ok(result);
            return Redirect("http://localhost:5173/login");
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var result = await _jwtService.ChangePassword(id, changePasswordRequest);
            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Status = false, Message = "Token không được để trống" });
            }
            var result = await _jwtService.ValidateRefreshToken(token);
            if (!result.Status)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}