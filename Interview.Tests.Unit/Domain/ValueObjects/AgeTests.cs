using FluentAssertions;
using Interview.Domain.ValueObjects;
using Xunit;

namespace Interview.Tests.Unit.Domain.ValueObjects;

public class AgeTests
{
    [Theory]
    // ვალიდური ასაკები (18+ წელი)
    [InlineData(18)]
    [InlineData(25)]
    [InlineData(50)]
    public void Should_Create_Valid_Age(int validAge)
    {
        // Act
        var age = Age.Create(validAge);

        // Assert
        age.Value.Should().Be(validAge);
    }

    [Theory]
    // არასრულწლოვანი ასაკები (18 წლის ქვევით)
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    public void Should_Throw_Exception_For_Underage(int invalidAge)
    {
        // Act & Assert
        var action = () => Age.Create(invalidAge);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*ასაკი უნდა იყოს მინიმუმ 18 წელი*");
    }

    [Theory]
    // ზუსტად 18 წლის
    [InlineData(18)]
    public void Should_Allow_Exactly_18_Years_Old(int exactly18)
    {
        // Act
        var age = Age.Create(exactly18);

        // Assert
        age.Value.Should().Be(18);
    }

    [Theory]
    // დიდი ასაკი
    [InlineData(80)]
    [InlineData(90)]
    public void Should_Allow_Old_Ages(int oldAge)
    {
        // Act
        var age = Age.Create(oldAge);

        // Assert
        age.Value.Should().Be(oldAge);
    }

    [Fact]
    public void Should_Be_Equal_When_Values_Are_Same()
    {
        // Arrange
        var age1 = Age.Create(25);
        var age2 = Age.Create(25);

        // Act & Assert
        age1.Should().Be(age2);
        age1.GetHashCode().Should().Be(age2.GetHashCode());
    }

    [Fact]
    public void Should_Not_Be_Equal_When_Values_Are_Different()
    {
        // Arrange
        var age1 = Age.Create(25);
        var age2 = Age.Create(30);

        // Act & Assert
        age1.Should().NotBe(age2);
    }

    [Fact]
    public void Should_Return_Correct_String_Representation()
    {
        // Arrange
        var age = Age.Create(25);

        // Act & Assert
        age.ToString().Should().Be("25");
    }

    [Theory]
    // FromBirthDate მეთოდის ტესტირება
    [InlineData(1990, 1, 1)] 
    [InlineData(2000, 6, 15)] 
    [InlineData(1985, 12, 31)] 
    public void Should_Create_Age_From_BirthDate(int year, int month, int day)
    {
        // Arrange
        var birthDate = new DateTime(year, month, day);

        // Act
        var age = Age.FromBirthDate(birthDate);

        // Assert
        age.Value.Should().BeGreaterOrEqualTo(18);
        age.Value.Should().BeLessThan(150); // რეალური ასაკი
    }

    [Theory]
    // არასრულწლოვანი პირების დაბადების თარიღები
    [InlineData(2010, 1, 1)]   // 15 წლის
    [InlineData(2015, 6, 15)]  // 10 წლის
    public void Should_Throw_Exception_For_Underage_BirthDate(int year, int month, int day)
    {
        // Arrange
        var underageBirthDate = new DateTime(year, month, day);

        // Act & Assert
        var action = () => Age.FromBirthDate(underageBirthDate);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*ასაკი უნდა იყოს მინიმუმ 18 წელი*");
    }

    [Theory]
    // მომავლის თარიღები
    [InlineData(2026, 1, 1)]
    [InlineData(2030, 6, 15)]
    public void Should_Throw_Exception_For_Future_BirthDate(int year, int month, int day)
    {
        // Arrange
        var futureBirthDate = new DateTime(year, month, day);

        // Act & Assert
        var action = () => Age.FromBirthDate(futureBirthDate);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*ასაკი უნდა იყოს მინიმუმ 18 წელი*");
    }

    [Theory]
    // ზუსტად 18 წლის პირების დაბადების თარიღები
    [InlineData(2007, 1, 1)]   // 18 წლის
    [InlineData(2007, 6, 15)]  // 18 წლის
    public void Should_Allow_Exactly_18_Years_Old_From_BirthDate(int year, int month, int day)
    {
        // Arrange
        var exactly18BirthDate = new DateTime(year, month, day);

        // Act
        var age = Age.FromBirthDate(exactly18BirthDate);

        // Assert
        age.Value.Should().Be(18);
    }

    [Fact]
    public void Should_Handle_Leap_Year_BirthDate()
    {
        // Arrange
        var leapYearBirthDate = new DateTime(2000, 2, 29); // ნაკიანი წელი

        // Act
        var age = Age.FromBirthDate(leapYearBirthDate);

        // Assert
        age.Value.Should().BeGreaterOrEqualTo(18);
        age.Value.Should().BeLessThan(150);
    }

    [Theory]
    // სხვადასხვა ასაკების შედარება
    [InlineData(18, 18, true)]   // იგივე ასაკი
    [InlineData(18, 25, false)]  // სხვადასხვა ასაკი
    public void Should_Compare_Ages_Correctly(int age1Value, int age2Value, bool shouldBeEqual)
    {
        // Arrange
        var age1 = Age.Create(age1Value);
        var age2 = Age.Create(age2Value);

        // Act & Assert
        if (shouldBeEqual)
        {
            age1.Should().Be(age2);
        }
        else
        {
            age1.Should().NotBe(age2);
        }
    }

    [Theory]
    // ძალიან დიდი ასაკები
    [InlineData(200)]
    [InlineData(300)]
    [InlineData(500)]
    public void Should_Allow_Very_Old_Ages(int veryOldAge)
    {
        // Act
        var age = Age.Create(veryOldAge);

        // Assert
        age.Value.Should().Be(veryOldAge);
    }
}
