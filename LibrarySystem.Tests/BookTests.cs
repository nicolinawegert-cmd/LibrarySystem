using LibrarySystem.Core.Models;

namespace LibrarySystem.Core.Models.Tests;

public class BookTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        //Arrange
        var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

        //Act & Assert
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
        Assert.Equal("Testbok by Testförfattare (2024) - ISBN: 978-91-0-012345-6", info);
    }


}