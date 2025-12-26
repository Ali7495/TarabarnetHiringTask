using MediatR;

namespace PhoneBook.Application;

public sealed record CreateContactCommand(string FirstName, string LastName, string PhoneNumber, string[] Tags) : IRequest<Guid>;