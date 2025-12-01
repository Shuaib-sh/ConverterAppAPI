using App.Application.DTOs.Users;
using App.Application.Interfaces;
using App.Domain.Entities;
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
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
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

    }
}
