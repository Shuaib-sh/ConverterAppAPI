using App.Application.DTOs.Tools;
using App.Application.Interfaces;
using App.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Tools
{
    public class TextToPdfTool :IToolProcessor
    {
        public string ToolName => "text-to-pdf";

        private readonly IFileToolService _service;

        public TextToPdfTool(IFileToolService service)
        {
            _service = service;
        }
        public async Task<ToolResult> ProcessAsync(string input, Dictionary<string, string>? options)
        {
            var pdfBytes = await _service.GeneratePdfFromText(input);

            return new ToolResult
            {
                FileBytes = pdfBytes,
                FileName = $"text-to-pdf-{DateTime.UtcNow.Ticks}.pdf",
                ContentType = "application/pdf"
            };
        }
    }
}