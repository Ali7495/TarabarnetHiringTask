using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Application;

namespace PhoneBook.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IContactRepository, InMemoryContactRepository>();
        return services;
    }
}
