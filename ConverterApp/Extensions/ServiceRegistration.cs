using App.Application.Interfaces;
using App.Application.Services;
using App.Infrastructure.Auth;
using App.Infrastructure.DB;
using App.Infrastructure.Repositories;

namespace ConverterApp.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IDapperContext, DapperContext>();
            services.AddScoped<IUserRepo, UserRepo>();
            return services;
        }
    }
}
