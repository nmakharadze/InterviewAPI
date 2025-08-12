using FluentAssertions;
using Interview.Domain.ValueObjects;
using Xunit;

namespace Interview.Tests.Unit.Domain.ValueObjects;

public class NameTests
{
    [Theory]
    [InlineData("ნინო")]
    [InlineData("Nino")]
    [InlineData("ნინო-მარია")]
    [InlineData("Nino-Maria")]
    public void Should_Create_Valid_Name(string validName)
    {
        // Act
        var name = Name.Create(validName);

        // Assert
        name.Value.Should().Be(validName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Throw_Exception_For_Empty_Name(string invalidName)
    {
        // Act & Assert
        var action = () => Name.Create(invalidName);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*სახელი არ შეიძლება იყოს ცარიელი*");
    }

    [Theory]
    [InlineData("123")]
    public void Should_Throw_Exception_For_Only_Numbers(string invalidName)
    {
        // Act & Assert
        var action = () => Name.Create(invalidName);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*სახელი უნდა შეიცავდეს ქართულ ან ლათინურ ასოებს*");
    }

    [Theory]
    [InlineData("გიორგი123")]
    [InlineData("Giorgi123")]
    [InlineData("გიორგი@")]
    [InlineData("Giorgi@")]
    public void Should_Allow_Mixed_Characters_With_Letters(string mixedName)
    {
        // Act & Assert
        var action = () => Name.Create(mixedName);
        // Note: Current validation allows mixed characters as long as they contain letters
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData("ნინოNino")]
    [InlineData("Ninoნინო")]
    public void Should_Throw_Exception_For_Mixed_Georgian_And_Latin(string mixedName)
    {
        // Act & Assert
        var action = () => Name.Create(mixedName);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*სახელი არ შეიძლება შეიცავდეს ქართულ და ლათინურ ასოებს ერთდროულად*");
    }

    [Fact]
    public void Should_Be_Equal_When_Values_Are_Same()
    {
        // Arrange
        var name1 = Name.Create("ნინო");
        var name2 = Name.Create("ნინო");

        // Act & Assert
        name1.Should().Be(name2);
        name1.GetHashCode().Should().Be(name2.GetHashCode());
    }

    [Fact]
    public void Should_Not_Be_Equal_When_Values_Are_Different()
    {
        // Arrange
        var name1 = Name.Create("ნინო");
        var name2 = Name.Create("მარია");

        // Act & Assert
        name1.Should().NotBe(name2);
    }
}
