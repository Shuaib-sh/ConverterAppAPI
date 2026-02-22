using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(int userId, string username, string email);
        string GenerateRefreshToken();
    }
}
