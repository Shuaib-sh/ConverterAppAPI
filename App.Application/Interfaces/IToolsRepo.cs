using App.Infrastructure.MappingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IToolsRepo
    {
        Task<IEnumerable<ToolWithGroupRaw>> GetPublicToolsAsync();
    }
}
