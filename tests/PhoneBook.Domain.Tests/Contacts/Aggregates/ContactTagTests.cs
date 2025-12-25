using FluentAssertions;

namespace PhoneBook.Domain.Tests;

public class ContactTagTests
{
    [Fact]
    public void AddTag_Should_Not_Allow_duplicates()
    {
        // Arrange

        Contact contact = Contact.Create(
            firstName: "Ali",
            lastName: "Fakhri",
            phoneNumber: PhoneNumber.Create("09123456789"),
            tags: new[] { Tag.Create("Work") }
        );

        // Act
        contact.AddTag(Tag.Create("work"));

        // Assert
        contact.Tags.Should().HaveCount(1);
        contact.Tags.Single().Value.Should().Be("work");
    }
}
