using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Email.cs
{
    public class SendEmailRequestDto
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string OutputContent { get; set; }
    }
}
