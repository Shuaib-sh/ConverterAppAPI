using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.FileTools
{
    public class FileToolResult
    {
        public object? Data { get; set; }
        public byte[]? File { get; set; }
        public string? ContentType { get; set; }
    }
}
