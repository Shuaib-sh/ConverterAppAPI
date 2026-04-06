using App.Application.Common;
using App.Application.DTOs.Email.cs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConverterApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly App.Application.Interfaces.IEmailService _emailService;
        public EmailController(App.Application.Interfaces.IEmailService emailService)
        {
            _emailService = emailService;
        }

        [Authorize]
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequestDto request)
        {
            var fromEmail = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            await _emailService.SendEmailAsync(request, fromEmail);

            return Ok(ApiResponse<string>.SuccessResponse("Email sent successfully", "Email delivered"));
        }
    }
}
