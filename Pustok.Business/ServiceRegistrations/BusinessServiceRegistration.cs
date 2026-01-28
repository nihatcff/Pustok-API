using Microsoft.Extensions.DependencyInjection;
using Pustok.Business.Services.Abstractions;
using Pustok.Business.Services.Implementations;

namespace Pustok.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddAutoMapper(_ => { }, typeof(BusinessServiceRegistration).Assembly);

        return services;
    }
}
