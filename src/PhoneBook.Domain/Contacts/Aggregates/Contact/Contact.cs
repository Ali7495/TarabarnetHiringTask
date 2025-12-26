using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace PhoneBook.Domain;

public sealed class Contact : BaseEntity
{
    private readonly List<Tag> _tags = new();

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    private Contact() { }

    private Contact(string firstName, string lastName, PhoneNumber phoneNumber, IEnumerable<Tag> tags)
    {
        SetName(firstName, lastName);

        PhoneNumber = phoneNumber ?? throw new DomainException("Phone number is required!");

        List<Tag> tagList = tags.ToList() ?? new();

        if (tagList.Count == 0)
            throw new DomainException("There is at least 1 tag required!");

        foreach (Tag tag in tagList)
            AddTag(tag);
    }

    public static Contact Create(string firstName, string lastName, PhoneNumber phoneNumber, IEnumerable<Tag> tags)
        => new(firstName, lastName, phoneNumber, tags);

    public void AddTag(Tag tag)
    {
        if (tag is null)
            throw new DomainException("Tag can not be empty!");

        if (_tags.Any(t => t.Value == tag.Value))
            throw new DomainException("A tag with the same name is already exist");

        _tags.Add(tag);
    }

    public void Update(string firstName, string lastName, PhoneNumber phoneNumber, IEnumerable<Tag> tags)
    {
        SetName(firstName, lastName);

        PhoneNumber = phoneNumber ?? throw new DomainException("Phone number is required.");

        _tags.Clear();

        var list = tags?.ToList() ?? new List<Tag>();
        if (list.Count == 0) throw new DomainException("At least one tag is required.");

        foreach (var t in list) AddTag(t); // دوباره Duplicate-safe
    }

    private void SetName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name con not be empty!");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name con not be empty!");

        FirstName = firstName;
        LastName = lastName;
    }
}
