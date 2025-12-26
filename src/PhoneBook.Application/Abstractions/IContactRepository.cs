using PhoneBook.Domain;

namespace PhoneBook.Application;

public interface IContactRepository
{
    Task AddAsync(Contact contact, CancellationToken cancellationToken = default);
    Task<Contact> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Contact>> GetListAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Contact contact, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
