using MediatR;
using PhoneBook.Domain;

namespace PhoneBook.Application;

public sealed class GetContactsByTagQueryHandler : IRequestHandler<GetContactsByTagQuery, IReadOnlyList<ContactResponse>>
{
    private readonly IContactRepository _contactRepository;

    public GetContactsByTagQueryHandler(IContactRepository contactRepository) => _contactRepository = contactRepository;

    public async Task<IReadOnlyList<ContactResponse>> Handle(GetContactsByTagQuery request, CancellationToken cancellationToken)
    {
        Tag tag = Tag.Create(request.tag);

        IReadOnlyList<Contact> allData = await _contactRepository.GetListAsync(cancellationToken);

        List<ContactResponse> filteredData = allData.Where(t => t.Tags.Any(a => a.Value == tag.Value))
                                .Select(s => new ContactResponse(s.Id, s.FirstName, s.LastName, s.PhoneNumber.Value, s.Tags.Select(x => x.Value).ToList())).ToList();

        return filteredData;

    }
}
