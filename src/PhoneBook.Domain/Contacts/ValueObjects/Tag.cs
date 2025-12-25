namespace PhoneBook.Domain;

public record Tag
{
    public string Value { get; }

    private Tag(string value) => Value = value;

    public static Tag Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new DomainException("Tags can not be empty!");

        string normalTag = input.Trim().ToLower();

        if (normalTag.Length > 30)
            throw new DomainException("Tags can not have more than 30 characters");

        return new(normalTag);
    }

    public override string ToString() => Value;
}
