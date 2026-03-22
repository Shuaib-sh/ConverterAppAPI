using App.Application.DTOs.Users;
using App.Application.Interfaces;
using App.Domain.Entities;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IConfiguration _config;
        public UserService(IUserRepo userRepo, IJwtTokenGenerator jwt,IConfiguration configuration)
        {
            _userRepo = userRepo;
            _jwt = jwt;
            _config = configuration;
        }
        public async Task<UserResponseDto> CreateOrUpdateUser(UserCreateDto dto)
        {
            try
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                var userEntity = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    PasswordHash = passwordHash,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };

                var userId = await _userRepo.CreateOrUpdateUserAsync(userEntity);

                return new UserResponseDto
                {
                    Id = userId,
                    Username = userEntity.Username,
                    Email = userEntity.Email
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to create or update user", ex);
            }
        }
        public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email
            });
        }
        public async Task<UserLoginResponseDto> UserLogin(UserLoginDto dto)
        {
            var user = await _userRepo.GetUserByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var accessToken = _jwt.GenerateAccessToken(user.Id, user.Username, user.Email);
            var refreshToken = _jwt.GenerateRefreshToken();
            var refreshExpiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpiryDays"]));

            await _userRepo.SaveRefreshTokenAsync(user.Id, refreshToken, refreshExpiry);

            return new UserLoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                user = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                }
            };
        }

        public async Task<string?> GetAccessTokenFromRefreshToken(string refreshToken)
        {
            var tokenRecord = await _userRepo.GetRefreshTokenAsync(refreshToken);

            if (tokenRecord == null)
                return null;

            if (tokenRecord.ExpiresAt <= DateTime.UtcNow)
                return null;

            var user = await _userRepo.GetUserByIdAsync(tokenRecord.UserId);

            if (user == null)
                return null;

            var newAccessToken = _jwt.GenerateAccessToken(user.Id, user.Username, user.Email);

            return newAccessToken;
        }

        public async Task<UserLoginResponseDto> GoogleLoginAsync(string idToken)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            }
            catch
            {
                throw new UnauthorizedAccessException("Invalid Google token");
            }

            if (payload.EmailVerified != true)
                throw new UnauthorizedAccessException("Google email not verified");

            var email = payload.Email;
            var name = payload.Name;
            var googleId = payload.Subject;

            var user = await _userRepo.GetUserByEmailAsync(email);

            if (user == null)
            {
               
                user = await _userRepo.CreateGoogleUserAsync(new User
                {
                    Email = email,
                    Username = name,
                    GoogleId = googleId,
                    CreatedAt = DateTime.UtcNow
                });
            }
            else
            {
                if (string.IsNullOrEmpty(user.GoogleId))
                {
                    await _userRepo.UpdateGoogleIdAsync(user.Id, googleId);
                }
            }

            var accessToken = _jwt.GenerateAccessToken(user.Id, user.Username, user.Email);
            var refreshToken = _jwt.GenerateRefreshToken();
            var refreshExpiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpiryDays"]));

            await _userRepo.SaveRefreshTokenAsync(user.Id, refreshToken, refreshExpiry);

            return new UserLoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                user = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                }
            };
        }
    }
}
