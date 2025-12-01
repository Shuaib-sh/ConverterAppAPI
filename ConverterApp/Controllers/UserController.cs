using App.Application.DTOs.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConverterApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly App.Application.Interfaces.IUserService _userService;
        public UserController(App.Application.Interfaces.IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("CreateOrUpdateUser")]
        public async Task<int> CreateOrUpdateUserAsync([FromBody]UserCreateDto createDto)
        {
            return await _userService.CreateOrUpdateUserAsync(createDto);
        }
    }
}
