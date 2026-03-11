using App.Application.Common;
using App.Application.Factory;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConverterApp.Controllers
{
    [ApiController]
    [Route("api/tools")]
    public class ToolController : ControllerBase
    {
        private readonly ToolFactory _toolFactory;

        public ToolController(ToolFactory toolFactory)
        {
            _toolFactory = toolFactory;
        }

        [HttpPost("{toolName}")]
        public async Task<IActionResult> ExecuteTool(string toolName)
        {
            var stopwatch = Stopwatch.StartNew();

            string input = "";
            Dictionary<string, string>? options = null;

            if (Request.HasFormContentType)
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files.FirstOrDefault();

                if (file != null)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    input = Convert.ToBase64String(ms.ToArray());
                }
            }
            else
            {
                var request = await HttpContext.Request.ReadFromJsonAsync<ToolRequest>();
                input = request?.Input ?? "";
                options = request?.Options;
            }

            var tool = _toolFactory.GetTool(toolName);
            var result = await tool.ProcessAsync(input, options);

            stopwatch.Stop();

            var meta = new MetaData
            {
                ProcessingTimeMs = stopwatch.ElapsedMilliseconds,
                InputSize = input.Length,
                OutputSize = result?.Data?.ToString()?.Length ?? result?.FileBytes?.Length ?? 0,
                ToolName = toolName
            };

            if (result.FileBytes != null)
            {
                return File(
                    result.FileBytes,
                    result.ContentType ?? "application/octet-stream",
                    result.FileName ?? "download"
                );
            }

            return Ok(ApiResponse<object>.SuccessResponse(
                result.Data,
                $"{toolName} processed successfully",
                meta
            ));
        }
    }

    public class ToolRequest
    {
        public string Input { get; set; }
        public Dictionary<string, string>? Options { get; set; }
    }
}