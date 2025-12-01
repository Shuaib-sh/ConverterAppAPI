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
        public async Task<int> CreateOrUpdateUserAsync(UserCreateDto dto)
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

                return await _userRepo.CreateOrUpdateUserAsync(userEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to create or update user", ex);
            }
        }
    }
}
