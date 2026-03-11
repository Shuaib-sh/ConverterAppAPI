using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Factory
{
    public class ToolFactory
    {
        private readonly IEnumerable<IToolProcessor> _tools;

        public ToolFactory(IEnumerable<IToolProcessor> tools)
        {
            _tools = tools;
        }

        public IToolProcessor GetTool(string toolName)
        {
            var tool = _tools.FirstOrDefault(t => t.ToolName.Equals(toolName, StringComparison.OrdinalIgnoreCase));

            if (tool == null)
                throw new Exception($"Tool '{toolName}' not found");

            return tool;
        }
    }
}
