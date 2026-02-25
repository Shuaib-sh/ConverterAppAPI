using App.Application.DTOs.Tools;
using App.Application.Interfaces;
using App.Domain.Entities;
using App.Infrastructure.MappingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class ToolsRepo : IToolsRepo
    {
        private IDapperContext _dapperContext;
        public ToolsRepo(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<ToolWithGroupRaw>>GetPublicToolsAsync()
        {
            var sql = @"
                       SELECT 
                           tg.id AS ToolGroupId,
                           tg.name AS ToolGroupName,
                           tg.display_order AS GroupDisplayOrder,
                   
                           t.id AS ToolId,
                           t.name AS ToolName,
                           t.description,
                           t.icon_name AS IconName,
                           t.route_url AS RouteUrl,
                           t.display_order AS ToolDisplayOrder
                   
                       FROM public.toolgroups tg
                       LEFT JOIN public.tools t 
                           ON tg.id = t.toolgroup_id
                   
                       WHERE 
                           tg.is_deleted = false
                           AND tg.is_active = true
                           AND t.is_deleted = false
                           AND t.is_active = true
                           AND t.is_public = true
                   
                       ORDER BY 
                           tg.display_order,
                           t.display_order;
                           ";

            return await _dapperContext.QueryListAsync<ToolWithGroupRaw>(sql);
        }
    }
}
