using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs.DataFormat
{
    public class JsonFormatter
    {
        public string Input { get; set; }
        public string Mode { get; set; } = "pretty";
    }
}
