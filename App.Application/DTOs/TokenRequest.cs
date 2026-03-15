using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public class TokenRequest
    {
        public class RefreshTokenRequest
        {
            public string RefreshToken { get; set; }
        }

        public class RefreshTokenResponse
        {
            public string AccessToken { get; set; }
        }
    }
}
