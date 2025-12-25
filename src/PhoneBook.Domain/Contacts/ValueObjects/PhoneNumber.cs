using System.Text.RegularExpressions;

namespace PhoneBook.Domain;

public sealed record PhoneNumber
{
    private readonly static Regex ValidPattern = new(@"^\+?\d{10,15}$", RegexOptions.Compiled);

    public string Value { get; }

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new DomainException("The phone number can not be empty!");

        string normalInput = input.Trim();

        if (!ValidPattern.IsMatch(normalInput))
            throw new DomainException("The phone number is invalid!");

        return new(input);
    }

    public override string ToString() => Value;
}
