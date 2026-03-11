using App.Application.Common;
using App.Application.DTOs.DataFormat;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConverterApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataFormatController : ControllerBase
    {
        private readonly IDataFormatService _dataFormatService;
        public DataFormatController(IDataFormatService dataFormatService)
        {
            _dataFormatService = dataFormatService;
        }

        [HttpPost("parse-hl7")]
        public async Task<IActionResult> ParseHL7([FromBody] ParseHL7Request hL7Request)
        {
            var result = await _dataFormatService.ParseHL7(hL7Request.Input);

            return Ok(ApiResponse<object>.SuccessResponse(result, "HL7 parsed successfully"));
        }

        //[HttpPost]
        //public async Task<IActionResult> FormatJson([FromBody]JsonFormatter request)
        //{
        //    var stopwatch = Stopwatch.StartNew();

        //    var result = await _dataFormatService.FormatJsonAsync(request.Input, request.Mode);

        //    stopwatch.Stop();

        //    var meta = new MetaData
        //    {
        //        ProcessingTimeMs = stopwatch.ElapsedMilliseconds,
        //        InputSize = request.Input.Length,
        //        OutputSize = result.Length,
        //        ToolName = "JSON Formatter"
        //    };

        //    return Ok(ApiResponse<string>.SuccessResponse(result, "JSON processed successfully", meta));
        //}
    }
}
