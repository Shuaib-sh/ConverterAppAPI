using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Tools
{
    public class ToolResult
    {
        public object? Data { get; set; }

        public byte[]? FileBytes { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }
    }
}
