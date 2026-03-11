using App.Application.DTOs.Tools;
using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Tools
{
    public class PdfToImageTool : IToolProcessor
    {
        public string ToolName => "pdf-to-image";

        private readonly IFileToolService _service;

        public PdfToImageTool(IFileToolService service)
        {
            _service = service;
        }

        public async Task<ToolResult> ProcessAsync(string input, Dictionary<string, string>? options)
        {
            var pdfBytes = Convert.FromBase64String(input);

            var images = await _service.ConvertPdfToImages(pdfBytes);

            return new ToolResult
            {
                Data = images
            };
        }
    }
}
