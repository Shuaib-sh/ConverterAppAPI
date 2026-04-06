using App.Application.DTOs.Email.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(SendEmailRequestDto request, string fromEmail);
    }
}
