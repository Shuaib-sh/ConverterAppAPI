using App.Application.DTOs.Tools;
using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Application.Tools
{
    public class JsonFormatterTool : IToolProcessor
    {
        public string ToolName => "json";
        public readonly IDataFormatService dataFormatService;

        public JsonFormatterTool(IDataFormatService dataFormatService)
        {
            this.dataFormatService = dataFormatService;
        }

        public async Task<ToolResult> ProcessAsync(string input, Dictionary<string, string>? options)
        {
            var mode = options?.GetValueOrDefault("mode") ?? "pretty";
            var result = await dataFormatService.FormatJsonAsync(input, mode);
            return new ToolResult
            {
                Data = result,
            };
        }
    }
}
