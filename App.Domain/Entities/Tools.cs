using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Domain.Entities
{
    public class Tool
    {
        public int Id { get; set; }
        public int ToolGroupId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconName { get; set; }
        public string RouteUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
    }
}
