using MediatR;
using PhoneBook.Application;
using PhoneBook.Domain;

public sealed class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
{

    private readonly IContactRepository _contactRepository;

    public UpdateContactCommandHandler(IContactRepository contactRepository) => _contactRepository = contactRepository;

    public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        Contact contact = await _contactRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new DomainException("Contact not found");

        contact.Update(request.FirstName, request.LastName, PhoneNumber.Create(request.PhoneNumber), request.Tags.Select(Tag.Create));

        await _contactRepository.UpdateAsync(contact);
    }
}