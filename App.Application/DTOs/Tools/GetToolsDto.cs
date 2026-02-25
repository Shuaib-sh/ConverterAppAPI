using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.Tools
{
    public class GetToolsDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconName { get; set; }
        public string RouteUrl { get; set; }
    }
    public class ToolGroupDto
    {
        public string GroupName { get; set; }
        public List<GetToolsDto> Tools { get; set; }
    }
}
