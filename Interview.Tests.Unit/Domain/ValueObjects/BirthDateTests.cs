using FluentAssertions;
using Interview.Domain.ValueObjects;
using Xunit;

namespace Interview.Tests.Unit.Domain.ValueObjects;

public class BirthDateTests
{
    [Theory]
    // ვალიდური დაბადების თარიღები (18+ წლის)
    [InlineData(1990, 1, 1)]   // 34 წლის
    [InlineData(2000, 6, 15)]  // 24 წლის
    public void Should_Create_Valid_BirthDate(int year, int month, int day)
    {
        // Arrange
        var birthDate = new DateTime(year, month, day);

        // Act
        var result = BirthDate.Create(birthDate);

        // Assert
        result.Value.Should().Be(birthDate);
        result.GetAge().Should().BeGreaterOrEqualTo(18);
    }

    [Theory]
    // მომავლის თარიღები
    [InlineData(2026, 1, 1)]
    [InlineData(2030, 6, 15)]
    public void Should_Throw_Exception_For_Future_Date(int year, int month, int day)
    {
        // Arrange
        var futureDate = new DateTime(year, month, day);

        // Act & Assert
        var action = () => BirthDate.Create(futureDate);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*დაბადების თარიღი არ შეიძლება იყოს მომავალში*");
    }

    [Theory]
    // არასრულწლოვანი პირები (18 წლის ქვევით)
    [InlineData(2010, 1, 1)]   // 14 წლის
    [InlineData(2015, 6, 15)]  // 9 წლის
    public void Should_Throw_Exception_For_Underage_Person(int year, int month, int day)
    {
        // Arrange
        var underageDate = new DateTime(year, month, day);

        // Act & Assert
        var action = () => BirthDate.Create(underageDate);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*პირი უნდა იყოს მინიმუმ 18 წლის*");
    }

    [Theory]
    // ზუსტად 18 წლის პირები
    [InlineData(2007, 1, 1)]   // 18 წლის
    [InlineData(2007, 6, 15)]  // 18 წლის
    public void Should_Allow_Exactly_18_Years_Old(int year, int month, int day)
    {
        // Arrange
        var exactly18Date = new DateTime(year, month, day);

        // Act
        var result = BirthDate.Create(exactly18Date);

        // Assert
        result.Value.Should().Be(exactly18Date);
        result.GetAge().Should().Be(18);
    }

    [Theory]
    // ძველი თარიღები
    [InlineData(1900, 1, 1)]   // 124 წლის
    [InlineData(1950, 6, 15)]  // 74 წლის
    [InlineData(1980, 12, 31)] // 44 წლის
    [InlineData(1970, 3, 10)]  // 54 წლის
    [InlineData(1960, 8, 20)]  // 64 წლის
    public void Should_Allow_Old_People(int year, int month, int day)
    {
        // Arrange
        var oldDate = new DateTime(year, month, day);

        // Act
        var result = BirthDate.Create(oldDate);

        // Assert
        result.Value.Should().Be(oldDate);
        result.GetAge().Should().BeGreaterThan(18);
    }

    [Fact]
    public void Should_Be_Equal_When_Values_Are_Same()
    {
        // Arrange
        var birthDate1 = BirthDate.Create(new DateTime(1990, 1, 1));
        var birthDate2 = BirthDate.Create(new DateTime(1990, 1, 1));

        // Act & Assert
        birthDate1.Should().Be(birthDate2);
        birthDate1.GetHashCode().Should().Be(birthDate2.GetHashCode());
    }

    [Fact]
    public void Should_Not_Be_Equal_When_Values_Are_Different()
    {
        // Arrange
        var birthDate1 = BirthDate.Create(new DateTime(1990, 1, 1));
        var birthDate2 = BirthDate.Create(new DateTime(1991, 1, 1));

        // Act & Assert
        birthDate1.Should().NotBe(birthDate2);
    }

    [Fact]
    public void Should_Return_Correct_String_Representation()
    {
        // Arrange
        var birthDate = BirthDate.Create(new DateTime(1990, 1, 1));

        // Act & Assert
        birthDate.ToString().Should().Be("1990-01-01");
    }

    [Theory]
    // სხვადასხვა თარიღები GetAge მეთოდის ტესტირებისთვის
    [InlineData(1990, 1, 1)] 
    [InlineData(2000, 6, 15)] 
    [InlineData(1985, 12, 31)] 
    [InlineData(2005, 3, 10)] 
    public void Should_Calculate_Correct_Age(int year, int month, int day)
    {
        // Arrange
        var birthDate = BirthDate.Create(new DateTime(year, month, day));

        // Act
        var actualAge = birthDate.GetAge();

        // Assert
        actualAge.Should().BeGreaterOrEqualTo(18);
        actualAge.Should().BeLessThan(150); // რეალისტური ასაკი
    }

    [Theory]
    // ზღვრული შემთხვევები
    [InlineData(2007, 1, 1)]   // ზუსტად 18 წლის
    public void Should_Handle_Edge_Cases(int year, int month, int day)
    {
        // Arrange
        var edgeCaseDate = new DateTime(year, month, day);

        // Act
        var result = BirthDate.Create(edgeCaseDate);

        // Assert
        result.Value.Should().Be(edgeCaseDate);
        result.GetAge().Should().Be(18);
    }

    [Fact]
    public void Should_Handle_Leap_Year_BirthDate()
    {
        // Arrange
        var leapYearDate = new DateTime(2000, 2, 29); // ნაკიანი წელი

        // Act
        var result = BirthDate.Create(leapYearDate);

        // Assert
        result.Value.Should().Be(leapYearDate);
        result.GetAge().Should().Be(25); // 2025-2000 = 25
    }

    [Theory]
    // თარიღები რომლებიც ახლა უკვე 18+ არიან
    [InlineData(2006, 1, 1)]   // 19 წლის
    [InlineData(2006, 6, 15)]  // 19 წლის
    public void Should_Allow_Recently_Turned_18(int year, int month, int day)
    {
        // Arrange
        var recently18Date = new DateTime(year, month, day);

        // Act
        var result = BirthDate.Create(recently18Date);

        // Assert
        result.Value.Should().Be(recently18Date);
        result.GetAge().Should().BeGreaterOrEqualTo(18);
    }
}
