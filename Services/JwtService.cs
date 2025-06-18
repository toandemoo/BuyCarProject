using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Project.DTOs;
using Project.DTOs.Request;
using Project.DTOs.Response;
using Project.Entities;
using Project.Models;
using Project.Repository;
using Project.Services.interfaces;
using ProjectBE.Entities;
using ProjectBE.Models;
using ProjectBE.Repository.interfaces;
using ProjectBE.Services.interfaces;

namespace ProjectBE.Services
{
    public class JwtService : IJwtService
    {
        private readonly ILogger<JwtService> _logger;

        private readonly IUserRepository _userRepository;

        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IConfiguration _configuration;

        private readonly IMailService _mailService;

        public JwtService(IMailService mailService, IUserRepository userRepository, IConfiguration configuration, ILogger<JwtService> logger, IRefreshTokenRepository refreshTokenRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _mailService = mailService;
        }
        public async Task<RegisterResponse> Register(RegisterRequest dto)
        {
            try
            {
                var userExists = await _userRepository.GetByEmailAsync(dto.Email);
                if (userExists != null)
                {
                    return new RegisterResponse { Message = "Email đã được sử dụng", Status = false };
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                var user = new Users
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Password = hashedPassword,
                    Role = dto.Role,
                    CreatedAt = DateTime.Now,
                    PhoneNumber = null,
                    profileImage = null,
                    EmailConfirmed = false,
                };
                await _userRepository.AddAsync(user);

                var token = await GenerateJwtToken(user);

                var confirmationLink = $"http://localhost:5153/api/jwt/verify-email?token={token}";
                MailRequest mailRequest = new MailRequest
                {
                    EmailTo = dto.Email,
                    EmailName = "Pham Duc Toan",
                    EmailSubject = "Xác thực tài khoản",
                    EmailBody = $@"
                        <h3>Xác thực tài khoản</h3>
                        <p>Vui lòng nhấn vào liên kết bên dưới để xác thực email của bạn:</p>
                        <a href='{confirmationLink}'>Xác thực email</a>
                    "
                };
                await _mailService.SendMail(mailRequest);

                return new RegisterResponse { Message = "Tạo tài khoản thành công. Vui lòng kiểm tra Email để xác thực !", Status = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lỗi đăng ký: {Message}", e.Message);
                return new RegisterResponse { Message = "Lỗi tạo tài khoản !", Status = false };
            }
        }

        public async Task<VerifiedResponse> Verified(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return new VerifiedResponse { Status = false, Message = "User linked to this token does not exist." };

            if (user.EmailConfirmed)
                return new VerifiedResponse { Status = false, Message = "Email has already been verified." };

            user.EmailConfirmed = true;
            await _userRepository.UpdateAsync(user, user.Id);

            return new VerifiedResponse { Status = true, Message = "Email has been verified successfully." };
        }

        public async Task<LoginResponse> Login(LoginRequest dto)
        {
            var userExists = await _userRepository.GetByEmailAsync(dto.Email);

            if (userExists == null || !BCrypt.Net.BCrypt.Verify(dto.Password, userExists.Password))
            {
                return new LoginResponse { message = "Email hoặc mật khẩu không đúng", Status = false, accesstoken = null };
            }

            var Token = await GenerateJwtToken(userExists);

            return new LoginResponse
            {
                message = "Đăng nhập thành công",
                accesstoken = Token,
                refreshtoken = await GenerateRefreshToken(userExists.Id),
                Status = true
            };
        }

        public async Task<string> GenerateJwtToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("id",user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ChangePasswordResponse> ChangePassword(int userid, ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                if (!string.Equals(changePasswordRequest.NewPassword, changePasswordRequest.ConfirmPassword))
                    return new ChangePasswordResponse { Status = false, Message = "Có lỗi khi thay đổi mật khẩu !" };
                var user = await _userRepository.GetByIdAsync(userid);
                user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordRequest.NewPassword);
                await _userRepository.UpdateAsync(user, userid);
                return new ChangePasswordResponse { Status = true, Message = "Thay đổi mật khẩu thành công !" };
            }
            catch (Exception e)
            {
                _logger.LogError("Lỗi thay đổi mật khẩu: ", e.Message);
                return new ChangePasswordResponse { Status = false, Message = "Có lỗi khi thay đổi mật khẩu !" };
            }
        }

        public async Task<LoginResponse> ValidateRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
            if (refreshToken == null || refreshToken.Expiration < DateTime.UtcNow)
            {
                return new LoginResponse
                {
                    message = "Refresh token không hợp lệ hoặc đã hết hạn",
                    Status = false,
                    accesstoken = null,
                    refreshtoken = null
                };
            }

            await _refreshTokenRepository.DeleteAsync(refreshToken.Id);

            var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
            if (user == null)
            {
                return new LoginResponse
                {
                    message = "Người dùng không tồn tại",
                    Status = false,
                    accesstoken = null,
                    refreshtoken = null
                };
            }

            var Token = await GenerateJwtToken(user);
            return new LoginResponse
            {
                message = "Refresh token hợp lệ",
                Status = true,
                accesstoken = Token,
                refreshtoken = await GenerateRefreshToken(user.Id)
            };
        }

        public async Task<string> GenerateRefreshToken(int userId)
        {
            var refreshTokenValidityMins = _configuration.GetValue<int>("Jwt:RefreshTokenValidityMins");
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins),
                UserId = userId
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken.Token;
        }

    }
}