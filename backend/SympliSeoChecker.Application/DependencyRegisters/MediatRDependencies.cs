using Microsoft.Extensions.DependencyInjection;

namespace SympliSeoChecker.Application.DependencyRegisters
{
    public static class MediatRDependencies
    {
        public static IServiceCollection RegisterMediatRDependencies(this IServiceCollection services)
        {
            return services
                .AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(MediatRDependencies).Assembly));
        }
    }
}
