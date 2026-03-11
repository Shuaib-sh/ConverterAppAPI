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
    public class Hl7ParserTool : IToolProcessor
    {
        public string ToolName => "hl7";

        private readonly IDataFormatService _service;

        public Hl7ParserTool(IDataFormatService service)
        {
            _service = service;
        }

        public async Task<ToolResult> ProcessAsync(string input, Dictionary<string, string>? options)
        {
            var result = await _service.ParseHL7(input);
            return new ToolResult
            {
                Data = result,
            };
        }
    }
}
