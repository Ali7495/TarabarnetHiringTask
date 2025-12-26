namespace PhoneBook.Application;

public sealed record ContactResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    IReadOnlyList<string> Tags
);