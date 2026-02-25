using App.Application.Common;
using App.Application.DTOs.Tools;
using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class ToolsService : IToolsService
    {
        private readonly IToolsRepo _toolRepo;
        public ToolsService(IToolsRepo toolRepo)
        {
            _toolRepo = toolRepo;
        }
        public async Task<ApiResponse<List<ToolGroupDto>>> GetAllTools()
        {
            var rawData = await _toolRepo.GetPublicToolsAsync();

            var grouped = rawData
                .GroupBy(x => new
                {
                    x.ToolGroupId,
                    x.ToolGroupName,
                    x.GroupDisplayOrder
                })
                .OrderBy(g => g.Key.GroupDisplayOrder)
                .Select(g => new ToolGroupDto
                {
                    GroupName = g.Key.ToolGroupName,
                    Tools = g
                        .OrderBy(t => t.ToolDisplayOrder)
                        .Select(t => new GetToolsDto
                        {
                            Name = t.ToolName,
                            Description = t.Description,
                            IconName = t.IconName,
                            RouteUrl = t.RouteUrl
                        })
                        .ToList()
                })
                .ToList();

            return ApiResponse<List<ToolGroupDto>>.SuccessResponse(grouped, "Tools fetched successfully");
        }
    }
}
