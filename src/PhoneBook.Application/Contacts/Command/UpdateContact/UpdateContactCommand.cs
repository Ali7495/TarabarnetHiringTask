using MediatR;

public sealed record UpdateContactCommand(
Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string[] Tags
) : IRequest;