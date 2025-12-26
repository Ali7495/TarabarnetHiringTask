using MediatR;
using PhoneBook.Application;

public sealed class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
{
    private readonly IContactRepository _contactRepository;

    public DeleteContactCommandHandler(IContactRepository contactRepository) => _contactRepository = contactRepository;
    
    public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        await _contactRepository.DeleteAsync(request.id, cancellationToken);
    }
}