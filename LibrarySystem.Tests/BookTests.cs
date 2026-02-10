using LibrarySystem.Core.Models;
using Xunit;

namespace LibrarySystem.Core.Models.Tests;

public class BookTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        // Arrange & Act
        var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

        // Assert
        Assert.Equal("978-91-0-012345-6", book.ISBN);
        Assert.Equal("Testbok", book.Title);
        Assert.Equal("Testförfattare", book.Author);
        Assert.Equal(2024, book.PublishedYear);
        Assert.True(book.IsAvailable);
    }

    [Fact]
    public void GetInfo_ShouldReturnFormattedString()
    {
        //Arrange
        var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

        //Act
        var info = book.GetInfo();

        //Assert
        Assert.Equal("Testbok av Testförfattare (2024) - Tillgänglig", info);
    }

    [Fact]
    public void GetInfo_ShouldShowBorrowed_WhenBookIsNotAvailable()
    {
        //Arrange
        var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

        book.MarkAsBorrowed();

        //Act
        var info = book.GetInfo();

        //Assert
        Assert.Contains("Utlånad", info);
    }

    [Fact]
    public void IsAvailable_ShouldBeTrue_ForNewBook()
    {
        // Arrange & Act
        var book = new Book("123", "T", "A", 2020);

        // Assert
        Assert.True(book.IsAvailable);
    }

}