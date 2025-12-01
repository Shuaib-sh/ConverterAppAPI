using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IUserRepo
    {
        Task<int> CreateOrUpdateUserAsync(User user);
        Task<int> DeleteUserAsync(int userId, string modifiedBy);
        Task<bool> UserExistsAsync(string email);
        Task<int> GetUserCountAsync();
    }
}
