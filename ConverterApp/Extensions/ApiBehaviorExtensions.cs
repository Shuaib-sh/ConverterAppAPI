using App.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace ConverterApp.Extensions
{
    public static class ApiBehaviorExtensions
    {
        public static IServiceCollection AddCustomApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors.Select(e =>
                            new ApiError(x.Key, e.ErrorMessage)))
                        .ToList();

                    var response = ApiResponse<string>.FailureResponse(
                        "Validation Failed",
                        errors);

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
