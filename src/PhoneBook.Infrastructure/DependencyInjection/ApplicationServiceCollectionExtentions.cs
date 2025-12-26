using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Application;
using FluentValidation;
using MediatR;

namespace PhoneBook.Infrastructure;

public static class ApplicationServiceCollectionExtentions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config=>
        {
            config.RegisterServicesFromAssemblyContaining<CreateContactCommand>();
        });

        services.AddValidatorsFromAssemblyContaining<CreateContactCommand>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
