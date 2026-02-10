using FluentAssertions;
using Interview.Domain.ValueObjects;
using Xunit;

namespace Interview.Tests.Unit.Domain.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    // მობილური ნომრები
    [InlineData("+995555123456")]
    [InlineData("995555123456")]
    [InlineData("555123456")]
    [InlineData("+995 555 123 456")]
    [InlineData("555-123-456")]
    // ქალაქის ნომრები
    [InlineData("+995032123456")]
    [InlineData("995032123456")]
    [InlineData("032123456")]
    [InlineData("+995032212345")]
    [InlineData("995032212345")]
    [InlineData("032212345")]
    public void Should_Create_Valid_PhoneNumber(string validPhoneNumber)
    {
        // Act
        var phoneNumber = PhoneNumber.Create(validPhoneNumber);

        // Assert
        phoneNumber.Value.Should().NotBeNullOrEmpty();
        phoneNumber.Value.Should().MatchRegex(@"^\+?[\d]+$");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_For_Empty_PhoneNumber(string invalidPhoneNumber)
    {
        // Act & Assert
        var action = () => PhoneNumber.Create(invalidPhoneNumber);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*ტელეფონის ნომერი არ შეიძლება იყოს ცარიელი*");
    }

    [Theory]
    // არასწორი მობილური ნომრები
    [InlineData("123456789")]           // ძალიან მოკლე
    [InlineData("1234567890123456789")] // ძალიან გრძელი
    [InlineData("1234567890")]          // არასწორი ფორმატი
    [InlineData("12345678901")]         // არასწორი ფორმატი
    [InlineData("123456789012")]        // არასწორი ფორმატი
    [InlineData("1234567890123")]       // არასწორი ფორმატი
    [InlineData("12345678901234")]      // არასწორი ფორმატი
    [InlineData("123456789012345")]     // არასწორი ფორმატი
    [InlineData("1234567890123456")]    // არასწორი ფორმატი
    [InlineData("12345678901234567")]   // არასწორი ფორმატი
    [InlineData("123456789012345678")]  // არასწორი ფორმატი
    public void Should_Throw_Exception_For_Invalid_PhoneNumber(string invalidPhoneNumber)
    {
        // Act & Assert
        var action = () => PhoneNumber.Create(invalidPhoneNumber);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*არასწორი ქართული ტელეფონის ნომერი*");
    }

    [Theory]
    // მობილური ნომრები სპეციალური სიმბოლოებით
    [InlineData("+995 555 123 456")]
    [InlineData("995-555-123-456")]
    [InlineData("555.123.456")]
    [InlineData("(555) 123-456")]
    [InlineData("555 123 456")]
    // ქალაქის ნომრები სპეციალური სიმბოლოებით
    [InlineData("+995 032 123 456")]
    [InlineData("995-032-123-456")]
    [InlineData("032.123.456")]
    [InlineData("(032) 123-456")]
    [InlineData("032 123 456")]
    public void Should_Normalize_PhoneNumber_With_Special_Characters(string phoneNumberWithSpecialChars)
    {
        // Act
        var phoneNumber = PhoneNumber.Create(phoneNumberWithSpecialChars);

        // Assert
        phoneNumber.Value.Should().NotBeNullOrEmpty();
        phoneNumber.Value.Should().MatchRegex(@"^\+?[\d]+$");
    }

    [Theory]
    // მობილური ნომრები სხვადასხვა ფორმატით
    [InlineData("+995555123456", "+995555123456")]
    [InlineData("995555123456", "+995555123456")]
    [InlineData("555123456", "+995555123456")]
    // ქალაქის ნომრები სხვადასხვა ფორმატით
    [InlineData("+995032123456", "+995032123456")]
    [InlineData("995032123456", "+995032123456")]
    [InlineData("032123456", "+995032123456")]
    [InlineData("+995032212345", "+995032212345")]
    [InlineData("995032212345", "+995032212345")]
    [InlineData("032212345", "+995032212345")]
    public void Should_Normalize_PhoneNumber_To_Standard_Format(string input, string expectedOutput)
    {
        // Act
        var phoneNumber = PhoneNumber.Create(input);

        // Assert
        phoneNumber.Value.Should().Be(expectedOutput);
    }

    [Fact]
    public void Should_Be_Equal_When_Values_Are_Same()
    {
        // Arrange
        var phoneNumber1 = PhoneNumber.Create("555123456");
        var phoneNumber2 = PhoneNumber.Create("555123456");

        // Act & Assert
        phoneNumber1.Should().Be(phoneNumber2);
        phoneNumber1.GetHashCode().Should().Be(phoneNumber2.GetHashCode());
    }

    [Fact]
    public void Should_Not_Be_Equal_When_Values_Are_Different()
    {
        // Arrange
        var phoneNumber1 = PhoneNumber.Create("555123456");
        var phoneNumber2 = PhoneNumber.Create("555654321");

        // Act & Assert
        phoneNumber1.Should().NotBe(phoneNumber2);
    }

    [Fact]
    public void Should_Return_Correct_String_Representation()
    {
        // Arrange
        var phoneNumber = PhoneNumber.Create("555123456");

        // Act & Assert
        phoneNumber.ToString().Should().Be("+995555123456");
    }

    [Theory]
    // არასწორი ქართული ნომრები
    [InlineData("+995123456789")]  // არასწორი პრეფიქსი
    [InlineData("+99512345678")]   // არასწორი პრეფიქსი
    [InlineData("+9951234567")]    // არასწორი პრეფიქსი
    [InlineData("+995123456")]     // არასწორი პრეფიქსი
    [InlineData("+99512345")]      // არასწორი პრეფიქსი
    [InlineData("+9951234")]       // არასწორი პრეფიქსი
    [InlineData("+995123")]        // არასწორი პრეფიქსი
    [InlineData("+99512")]         // არასწორი პრეფიქსი
    [InlineData("+9951")]          // არასწორი პრეფიქსი
    public void Should_Throw_Exception_For_Invalid_Georgian_PhoneNumber(string invalidPhoneNumber)
    {
        // Act & Assert
        var action = () => PhoneNumber.Create(invalidPhoneNumber);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*არასწორი ქართული ტელეფონის ნომერი*");
    }
}

