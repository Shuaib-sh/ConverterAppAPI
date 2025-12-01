using App.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateOrUpdateUser(UserCreateDto dto);
        Task<IEnumerable<UserResponseDto>> GetAllUsers();
    }
}
