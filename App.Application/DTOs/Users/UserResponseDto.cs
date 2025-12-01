using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
