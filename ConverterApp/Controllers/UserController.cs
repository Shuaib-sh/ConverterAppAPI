using App.Application.Common;
using App.Application.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConverterApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly App.Application.Interfaces.IUserService _userService;
        public UserController(App.Application.Interfaces.IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("CreateOrUpdateUser")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]UserCreateDto createDto)
        {
           var userResponse = await _userService.CreateOrUpdateUser(createDto);
           return Ok(ApiResponse<UserResponseDto>.SuccessResponse(userResponse, "User created successfully"));
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsers();
            return Ok(ApiResponse<IEnumerable<UserResponseDto>>.SuccessResponse(users, "Users retrieved successfully"));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var loginResponse = await _userService.UserLogin(loginDto);
            return Ok(ApiResponse<UserLoginResponseDto>.SuccessResponse(loginResponse, "Login successful"));
        }
    }
}
