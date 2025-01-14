using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SympliSeoChecker.Application.DependencyRegisters
{
    public static class FluentValidationDependencies
    {
        public static IServiceCollection RegisterFluentValidationDependencies(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssembly(typeof(FluentValidationDependencies).Assembly);
        }
    }
}
