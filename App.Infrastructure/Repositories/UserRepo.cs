using App.Application.Interfaces;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IDapperContext _dapperContext;
        public UserRepo(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }   
        public async Task<int> GetUserCountAsync()
        {
            var query = "SELECT COUNT(*) FROM Users WHERE IsDeleted = false";
            return await _dapperContext.ExecuteScalarAsync<int>(query);
        }
        public async Task<bool> UserExistsAsync(string email)
        {
            var query = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
            var count = await _dapperContext.ExecuteScalarAsync<int>(query, new { Email = email });
            return count > 0;
        }
        public async Task<int> CreateOrUpdateUserAsync(User user)
        {
            var query = @"
                INSERT INTO Users (Username, Email, PasswordHash, CreatedAt, CreatedBy, IsDeleted)
                VALUES (@Username, @Email, @PasswordHash, NOW(), @CreatedBy, false)
                ON CONFLICT (Email)
                DO UPDATE SET
                    Username = EXCLUDED.Username,
                    PasswordHash = EXCLUDED.PasswordHash,
                    ModifiedAt = NOW(),
                    ModifiedBy = @ModifiedBy,
                    IsDeleted = false
                RETURNING Id;
            ";

            return await _dapperContext.ExecuteScalarAsync<int>(query, user);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var query = @"
                SELECT Id, Username, Email, PasswordHash, CreatedAt, ModifiedAt, CreatedBy, ModifiedBy, IsDeleted
                FROM Users
                WHERE IsDeleted = false
                ORDER BY CreatedAt DESC;
            ";

            return await _dapperContext.QueryListAsync<User>(query);
        }

        public async Task<int> DeleteUserAsync(int userId, string modifiedBy)
        {
            var query = @"
                UPDATE Users
                SET IsDeleted = true, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy
                WHERE Id = @UserId AND IsDeleted = false;
            ";
            var parameters = new
            {
                UserId = userId,
                ModifiedAt = DateTime.UtcNow,
                ModifiedBy = modifiedBy
            };
            return await _dapperContext.ExecuteAsync(query, parameters);
        }
    }
}
