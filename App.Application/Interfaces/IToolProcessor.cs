using App.Application.DTOs.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IToolProcessor
    {
        string ToolName { get; }
        Task<ToolResult> ProcessAsync(string input, Dictionary<string, string>? options);
    }
}
