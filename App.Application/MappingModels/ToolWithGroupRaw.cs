using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.MappingModels
{
    public class ToolWithGroupRaw
    {
        public int ToolGroupId { get; set; }
        public string ToolGroupName { get; set; }
        public int GroupDisplayOrder { get; set; }

        public int ToolId { get; set; }
        public string ToolName { get; set; }
        public string? Description { get; set; }
        public string? IconName { get; set; }
        public string RouteUrl { get; set; }
        public int ToolDisplayOrder { get; set; }
    }
}
