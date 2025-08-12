using FluentAssertions;
using Interview.Domain.ValueObjects;
using Xunit;

namespace Interview.Tests.Unit.Domain.ValueObjects;

public class ImagePathTests
{
    [Theory]
    // ვალიდური ფაილის გაფართოებები
    [InlineData("images/persons/photo.jpg")]
    [InlineData("images/persons/photo.jpeg")]
    [InlineData("images/persons/photo.png")]
    [InlineData("images/persons/photo.gif")]
    [InlineData("images/persons/photo.bmp")]
    [InlineData("images/persons/user123.jpg")]
    [InlineData("images/persons/profile_photo.png")]
    [InlineData("images/persons/avatar.gif")]
    [InlineData("images/persons/photo.BMP")]  // დიდი ასოებით
    [InlineData("images/persons/photo.JPG")]  // დიდი ასოებით
    public void Should_Create_Valid_ImagePath(string validImagePath)
    {
        // Act
        var imagePath = ImagePath.Create(validImagePath);

        // Assert
        imagePath.Value.Should().Be(validImagePath);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_For_Empty_ImagePath(string invalidImagePath)
    {
        // Act & Assert
        var action = () => ImagePath.Create(invalidImagePath);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*სურათის მისამართი არ შეიძლება იყოს ცარიელი*");
    }

    [Theory]
    // ძალიან გრძელი მისამართი
    [InlineData("images/persons/very_long_filename_that_exceeds_the_maximum_length_allowed_by_the_system_and_should_throw_an_exception_because_it_is_too_long_for_the_database_to_handle_properly_and_would_cause_issues_with_file_system_paths_that_have_limitations_on_how_many_characters_they_can_contain_in_a_single_path_component_which_is_typically_around_255_characters_for_most_file_systems_but_we_are_setting_a_limit_of_500_characters_for_the_entire_path_which_should_be_more_than_sufficient_for_most_use_cases_including_deep_directory_structures_and_long_filenames_with_extensions.jpg")]
    public void Should_Throw_Exception_For_Too_Long_ImagePath(string invalidImagePath)
    {
        // Act & Assert
        var action = () => ImagePath.Create(invalidImagePath);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*სურათის მისამართი არ უნდა აღემატებოდეს 500 სიმბოლოს*");
    }

    [Theory]
    // არასწორი ფაილის გაფართოებები
    [InlineData("images/persons/photo.txt")]
    [InlineData("images/persons/photo.doc")]
    [InlineData("images/persons/photo.pdf")]
    [InlineData("images/persons/photo.exe")]
    [InlineData("images/persons/photo.bat")]
    [InlineData("images/persons/photo.sh")]
    [InlineData("images/persons/photo.mp4")]
    [InlineData("images/persons/photo.avi")]
    [InlineData("images/persons/photo.mp3")]
    [InlineData("images/persons/photo.zip")]
    [InlineData("images/persons/photo.rar")]
    [InlineData("images/persons/photo.7z")]
    [InlineData("images/persons/photo.xml")]
    [InlineData("images/persons/photo.json")]
    [InlineData("images/persons/photo.html")]
    public void Should_Throw_Exception_For_Invalid_File_Extension(string invalidImagePath)
    {
        // Act & Assert
        var action = () => ImagePath.Create(invalidImagePath);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*არასწორი ფაილის ფორმატი. დაშვებულია: jpg, jpeg, png, gif, bmp*");
    }

    [Theory]
    // ფაილები გაფართოების გარეშე
    [InlineData("images/persons/photo")]
    [InlineData("images/persons/photo.")]
    [InlineData("images/persons/photo..")]
    [InlineData("images/persons/photo_")]
    [InlineData("images/persons/photo123")]
    public void Should_Allow_ImagePath_Without_Extension(string imagePathWithoutExtension)
    {
        // Act
        var imagePath = ImagePath.Create(imagePathWithoutExtension);

        // Assert
        imagePath.Value.Should().Be(imagePathWithoutExtension);
    }

    [Theory]
    // ფაილები არასწორი გაფართოებით
    [InlineData("images/persons/.photo")]
    [InlineData("images/persons/.jp")]
    public void Should_Throw_Exception_For_Invalid_Extension_Format(string invalidImagePath)
    {
        // Act & Assert
        var action = () => ImagePath.Create(invalidImagePath);
        action.Should().Throw<ArgumentException>()
            .WithMessage("*არასწორი ფაილის ფორმატი. დაშვებულია: jpg, jpeg, png, gif, bmp*");
    }

    [Theory]
    // ფაილები რომლებიც არ იწვევენ exception-ს
    [InlineData("images/persons/.jpg")]
    [InlineData("images/persons/.png")]
    public void Should_Allow_Files_With_Dot_Prefix(string imagePathWithDotPrefix)
    {
        // Act
        var imagePath = ImagePath.Create(imagePathWithDotPrefix);

        // Assert
        imagePath.Value.Should().Be(imagePathWithDotPrefix);
    }

    [Theory]
    // სხვადასხვა ფოლდერის სტრუქტურები
    [InlineData("photo.jpg")]
    [InlineData("persons/photo.jpg")]
    [InlineData("images/persons/photo.jpg")]
    [InlineData("wwwroot/images/persons/photo.jpg")]
    [InlineData("uploads/images/persons/photo.jpg")]
    [InlineData("static/images/persons/photo.jpg")]
    [InlineData("assets/images/persons/photo.jpg")]
    public void Should_Allow_Different_Folder_Structures(string validImagePath)
    {
        // Act
        var imagePath = ImagePath.Create(validImagePath);

        // Assert
        imagePath.Value.Should().Be(validImagePath);
    }

    [Theory]
    // სპეციალური სიმბოლოები ფაილის სახელში
    [InlineData("images/persons/photo-123.jpg")]
    [InlineData("images/persons/photo_123.jpg")]
    [InlineData("images/persons/photo.123.jpg")]
    [InlineData("images/persons/photo (1).jpg")]
    [InlineData("images/persons/photo[1].jpg")]
    [InlineData("images/persons/photo{1}.jpg")]
    [InlineData("images/persons/photo@123.jpg")]
    [InlineData("images/persons/photo#123.jpg")]
    [InlineData("images/persons/photo$123.jpg")]
    [InlineData("images/persons/photo%123.jpg")]
    public void Should_Allow_Special_Characters_In_Filename(string validImagePath)
    {
        // Act
        var imagePath = ImagePath.Create(validImagePath);

        // Assert
        imagePath.Value.Should().Be(validImagePath);
    }

    [Fact]
    public void Should_Be_Equal_When_Values_Are_Same()
    {
        // Arrange
        var imagePath1 = ImagePath.Create("images/persons/photo.jpg");
        var imagePath2 = ImagePath.Create("images/persons/photo.jpg");

        // Act & Assert
        imagePath1.Should().Be(imagePath2);
        imagePath1.GetHashCode().Should().Be(imagePath2.GetHashCode());
    }

    [Fact]
    public void Should_Not_Be_Equal_When_Values_Are_Different()
    {
        // Arrange
        var imagePath1 = ImagePath.Create("images/persons/photo1.jpg");
        var imagePath2 = ImagePath.Create("images/persons/photo2.jpg");

        // Act & Assert
        imagePath1.Should().NotBe(imagePath2);
    }

    [Fact]
    public void Should_Return_Correct_String_Representation()
    {
        // Arrange
        var imagePath = ImagePath.Create("images/persons/photo.jpg");

        // Act & Assert
        imagePath.ToString().Should().Be("images/persons/photo.jpg");
    }

    [Theory]
    // ფაილის სახელები სხვადასხვა სიგრძით
    [InlineData("a.jpg")]
    [InlineData("ab.jpg")]
    [InlineData("abc.jpg")]
    [InlineData("abcd.jpg")]
    [InlineData("abcde.jpg")]
    [InlineData("abcdef.jpg")]
    [InlineData("abcdefg.jpg")]
    [InlineData("abcdefgh.jpg")]
    [InlineData("abcdefghi.jpg")]
    [InlineData("abcdefghij.jpg")]
    public void Should_Allow_Short_Filenames(string validImagePath)
    {
        // Act
        var imagePath = ImagePath.Create(validImagePath);

        // Assert
        imagePath.Value.Should().Be(validImagePath);
    }

    [Theory]
    // ფაილის სახელები სფეისებით
    [InlineData("images/persons/my photo.jpg")]
    [InlineData("images/persons/my photo.png")]
    [InlineData("images/persons/my photo.gif")]
    [InlineData("images/persons/my photo.bmp")]
    [InlineData("images/persons/my photo.jpeg")]
    public void Should_Allow_Filenames_With_Spaces(string validImagePath)
    {
        // Act
        var imagePath = ImagePath.Create(validImagePath);

        // Assert
        imagePath.Value.Should().Be(validImagePath);
    }
}
