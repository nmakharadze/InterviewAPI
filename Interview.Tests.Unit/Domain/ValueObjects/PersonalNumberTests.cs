using FluentAssertions;
using Interview.Domain.ValueObjects;
using Xunit;

namespace Interview.Tests.Unit.Domain.ValueObjects;

public class PersonalNumberTests
{
    [Theory]
    [InlineData("12345678901")]
    [InlineData("00000000000")]
    [InlineData("99999999999")]
    public void Should_Create_Valid_PersonalNumber(string validPersonalNumber)
    {
        // Act
        var personalNumber = PersonalNumber.Create(validPersonalNumber);

        // Assert
        personalNumber.Value.Should().Be(validPersonalNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_For_Empty_PersonalNumber(string invalidPersonalNumber)
    {
        // Act & Assert
        var action = () => PersonalNumber.Create(invalidPersonalNumber);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*პირადი ნომერი არ შეიძლება იყოს ცარიელი*");
    }

    [Theory]
    [InlineData("1234567890")]   // 10 ციფრი
    [InlineData("123456789012")] // 12 ციფრი
    [InlineData("123")]          // 3 ციფრი
    public void Should_Throw_Exception_For_Wrong_Length(string invalidPersonalNumber)
    {
        // Act & Assert
        var action = () => PersonalNumber.Create(invalidPersonalNumber);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*პირადი ნომერი უნდა შეიცავდეს ზუსტად 11 ციფრს*");
    }

    [Theory]
    [InlineData("1234567890a")]  // 10 ციფრი + 1 ასო
    [InlineData("a1234567890")]  // 1 ასო + 10 ციფრი
    [InlineData("1234567890@")]  // 10 ციფრი + 1 სიმბოლო
    [InlineData("1234567890!")]  // 10 ციფრი + 1 სიმბოლო
    [InlineData("1234567890-")]  // 10 ციფრი + 1 ტირე
    public void Should_Throw_Exception_For_Non_Digit_Characters(string invalidPersonalNumber)
    {
        // Act & Assert
        var action = () => PersonalNumber.Create(invalidPersonalNumber);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*პირადი ნომერი უნდა შეიცავდეს მხოლოდ ციფრებს*");
    }

    [Fact]
    public void Should_Be_Equal_When_Values_Are_Same()
    {
        // Arrange
        var personalNumber1 = PersonalNumber.Create("12345678901");
        var personalNumber2 = PersonalNumber.Create("12345678901");

        // Act & Assert
        personalNumber1.Should().Be(personalNumber2);
        personalNumber1.GetHashCode().Should().Be(personalNumber2.GetHashCode());
    }

    [Fact]
    public void Should_Not_Be_Equal_When_Values_Are_Different()
    {
        // Arrange
        var personalNumber1 = PersonalNumber.Create("12345678901");
        var personalNumber2 = PersonalNumber.Create("98765432109");

        // Act & Assert
        personalNumber1.Should().NotBe(personalNumber2);
    }

    [Fact]
    public void Should_Return_Correct_String_Representation()
    {
        // Arrange
        var personalNumber = PersonalNumber.Create("12345678901");

        // Act & Assert
        personalNumber.ToString().Should().Be("12345678901");
    }
}
