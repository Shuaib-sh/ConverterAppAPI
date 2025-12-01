using App.Application.DTOs.Users;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ConverterApp.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationSetup(this IServiceCollection services)
        {
            // Enable automatic validation in ASP.NET pipeline
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<UserCreateDto>();

            return services;
        }
    }
}
