using FluentAssertions;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PhoneBook.Domain;

namespace PhoneBook.Application.Tests;

public class CreateContactCommandTest
{
    [Fact]
    public async Task Send_Should_Call_Repository_AddAsync_Once_And_Return_ContactId()
    {
        // Arrange

        ServiceCollection services = new();

        Mock<IContactRepository> contactRepository = new();

        contactRepository.Setup(c => c.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

        services.AddSingleton(contactRepository.Object);

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateContactCommand>());

        services.AddValidatorsFromAssemblyContaining<CreateContactCommand>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IMediator MediatR = serviceProvider.GetRequiredService<IMediator>();

        CreateContactCommand command = new(
            FirstName: "Ali",
            LastName: "Fakhri",
            PhoneNumber: "09121234567",
            Tags: new[] { "Tarabarnet" }
        );

        // Act

        Guid id = await MediatR.Send(command);

        // Assert
        contactRepository.Verify(r => r.AddAsync(It.Is<Contact>(c => c.FirstName == "Ali" && c.LastName == "Fakhri"), It.IsAny<CancellationToken>()), Times.Once);
    }
}
