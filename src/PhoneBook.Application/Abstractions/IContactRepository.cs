using PhoneBook.Domain;

namespace PhoneBook.Application;

public interface IContactRepository
{
    Task AddAsync(Contact contact, CancellationToken cancellationToken = default);
}
