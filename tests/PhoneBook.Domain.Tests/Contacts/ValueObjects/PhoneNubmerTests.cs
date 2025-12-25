using FluentAssertions;

namespace PhoneBook.Domain.Tests;

public class PhoneNubmerTests
{
    [Theory]
    [InlineData("09121234567")]
    [InlineData("+989121234567")]
    public void Create_Should_Accept_Valid_PhoneNumber(string input)
    {
        // Act

        PhoneNumber phone = PhoneNumber.Create(input);

        // Assert

        phone.Value.Should().Be(input.Trim());
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("HiThere123")]
    public void Create_Should_Reject_Invalid_PhoneNumber(string input)
    {
        // Act
        var phone = () => PhoneNumber.Create(input);

        // Assert
        phone.Should().Throw<DomainException>();
    }
}
