using MediatR;
using PhoneBook.Domain;

namespace PhoneBook.Application;

public sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IContactRepository _contactRepository;

    public CreateContactCommandHandler(IContactRepository contactRepository) => _contactRepository = contactRepository;

    public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        PhoneNumber phone = PhoneNumber.Create(request.PhoneNumber);

        Tag[] tags = request.Tags.Select(Tag.Create).ToArray();

        Contact contact = Contact.Create(
            request.FirstName,
            request.LastName,
            phone,
            tags
        );

        await _contactRepository.AddAsync(contact, cancellationToken);

        return contact.Id;
    }
}