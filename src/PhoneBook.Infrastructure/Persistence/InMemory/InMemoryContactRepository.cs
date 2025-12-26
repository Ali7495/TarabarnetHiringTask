using System.Collections.Concurrent;
using PhoneBook.Application;
using PhoneBook.Domain;

namespace PhoneBook.Infrastructure;

public class InMemoryContactRepository : IContactRepository
{
    private readonly ConcurrentDictionary<Guid, Contact> _store = new();

    public Task AddAsync(Contact contact, CancellationToken cancellationToken = default)
    {
        _store[contact.Id] = contact;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    public Task<Contact> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _store.TryGetValue(id, out Contact contact);
        return Task.FromResult(contact);
    }

    public Task<IReadOnlyList<Contact>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult((IReadOnlyList<Contact>)_store.Values.ToList());
    }

    public Task UpdateAsync(Contact contact, CancellationToken cancellationToken = default)
    {
        _store[contact.Id] = contact;
        return Task.CompletedTask;
    }
}
