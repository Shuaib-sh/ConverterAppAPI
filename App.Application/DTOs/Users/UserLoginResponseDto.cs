using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Users
{
    public class UserLoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserResponseDto user { get; set; }
    }
}
