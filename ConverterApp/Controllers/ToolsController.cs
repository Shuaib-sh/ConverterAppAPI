using App.Application.Common;
using App.Application.DTOs.Users;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConverterApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly IToolsService _toolService;
        public ToolsController(IToolsService toolService)
        {
            _toolService = toolService;
        }

        [HttpGet]
        public async Task<IActionResult> Getll()
        {
            var res = await _toolService.GetAllTools();
            return Ok(res);
        }
    }
}