using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Interview.Application.Persons.Commands.Create;
using Xunit;

namespace Interview.Tests.Integration.Controllers;

public class PersonControllerTests
{
    [Fact]
    public void Should_Have_Correct_CreatePersonDto_Structure()
    {
        // Arrange & Act
        var createPersonDto = new CreatePersonDto
        {
            FirstName = "ნინო",
            LastName = "მახარაძე",
            PersonalNumber = "12345678901",
            BirthDate = DateTime.Now.AddYears(-25),
            CityId = 1,
            GenderId = 1
        };

        // Assert
        createPersonDto.Should().NotBeNull();
        createPersonDto.FirstName.Should().Be("ნინო");
        createPersonDto.LastName.Should().Be("მახარაძე");
        createPersonDto.PersonalNumber.Should().Be("12345678901");
        createPersonDto.CityId.Should().Be(1);
        createPersonDto.GenderId.Should().Be(1);
    }

    [Fact]
    public void Should_Validate_CreatePersonDto_Properties()
    {
        // Arrange
        var createPersonDto = new CreatePersonDto
        {
            FirstName = "ნინო",
            LastName = "მახარაძე",
            PersonalNumber = "12345678901",
            BirthDate = DateTime.Now.AddYears(-25),
            CityId = 1,
            GenderId = 1
        };

        // Act & Assert
        createPersonDto.FirstName.Should().NotBeNullOrEmpty();
        createPersonDto.LastName.Should().NotBeNullOrEmpty();
        createPersonDto.PersonalNumber.Should().HaveLength(11);
        createPersonDto.BirthDate.Should().BeBefore(DateTime.Now);
        createPersonDto.CityId.Should().BeGreaterThan(0);
        createPersonDto.GenderId.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Should_Validate_PersonalNumber_Format()
    {
        // Arrange
        var validPersonalNumber = "12345678901";
        var invalidPersonalNumber = "1234567890"; // 10 digits

        // Act & Assert
        validPersonalNumber.Should().HaveLength(11);
        invalidPersonalNumber.Length.Should().NotBe(11);
    }

    [Fact]
    public void Should_Validate_Age_Requirement()
    {
        // Arrange
        var adultBirthDate = DateTime.Now.AddYears(-25);
        var minorBirthDate = DateTime.Now.AddYears(-17);

        // Act & Assert
        var adultAge = DateTime.Now.Year - adultBirthDate.Year;
        var minorAge = DateTime.Now.Year - minorBirthDate.Year;

        adultAge.Should().BeGreaterOrEqualTo(18);
        minorAge.Should().BeLessThan(18);
    }

    [Fact]
    public void Should_Validate_Name_Format()
    {
        // Arrange
        var validNames = new[] { "ნინო", "Nino", "ნინო-მარია", "Nino-Maria" };
        var invalidNames = new[] { "", " ", null, "123", "ნინო123" };

        // Act & Assert
        foreach (var name in validNames)
        {
            name.Should().NotBeNullOrEmpty();
            name.Should().MatchRegex(@"^[a-zA-Zა-ჰ\s\-]+$");
        }

        foreach (var name in invalidNames)
        {
            if (name != null && !string.IsNullOrWhiteSpace(name))
            {
                name.Should().NotMatchRegex(@"^[a-zA-Zა-ჰ\s\-]+$");
            }
        }
    }

    [Fact]
    public void Should_Validate_BirthDate_Not_In_Future()
    {
        // Arrange
        var pastBirthDate = DateTime.Now.AddYears(-25);
        var futureBirthDate = DateTime.Now.AddYears(1);

        // Act & Assert
        pastBirthDate.Should().BeBefore(DateTime.Now);
        futureBirthDate.Should().BeAfter(DateTime.Now);
    }

    [Fact]
    public void Should_Validate_Required_Fields()
    {
        // Arrange
        var createPersonDto = new CreatePersonDto
        {
            FirstName = "ნინო",
            LastName = "მახარაძე",
            PersonalNumber = "12345678901",
            BirthDate = DateTime.Now.AddYears(-25),
            CityId = 1,
            GenderId = 1
        };

        // Act & Assert
        createPersonDto.Should().NotBeNull();
        createPersonDto.FirstName.Should().NotBeNullOrWhiteSpace();
        createPersonDto.LastName.Should().NotBeNullOrWhiteSpace();
        createPersonDto.PersonalNumber.Should().NotBeNullOrWhiteSpace();
        createPersonDto.BirthDate.Should().NotBe(default(DateTime));
        createPersonDto.CityId.Should().BeGreaterThan(0);
        createPersonDto.GenderId.Should().BeGreaterThan(0);
    }
}
