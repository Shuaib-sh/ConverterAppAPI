using App.Application.DTOs.Tools;
using App.Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Tools
{
    public class ImageToPdfTool : IToolProcessor
    {
        public string ToolName => "image-to-pdf";
        public readonly IFileToolService _fileToolService;

        public ImageToPdfTool(IFileToolService fileToolService)
        {
            _fileToolService = fileToolService;
        }

        public async Task<ToolResult> ProcessAsync(string input, Dictionary<string, string>? options)
        {
            var images = JsonConvert.DeserializeObject<List<string>>(input);

            var imageBytes = images
                .Select(Convert.FromBase64String)
                .ToList();

            var pdfBytes = await _fileToolService.ConvertImagesToPdf(imageBytes);

            return new ToolResult
            {
                FileBytes = pdfBytes,
                ContentType = "application/pdf",
                FileName = "images-to-pdf.pdf"
            };
        }
    }
}
