using MediatR;

namespace PhoneBook.Application;

public record GetContactsByTagQuery(string tag) : IRequest<IReadOnlyList<ContactResponse>>;