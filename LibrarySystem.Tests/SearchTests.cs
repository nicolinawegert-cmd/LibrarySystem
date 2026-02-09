using LibrarySystem.Core.Models;
using Xunit;

namespace LibrarySystem.Tests;

public class SearchTests
{
  [Theory]
  [InlineData("Tolkien", true)]
  [InlineData("tolkien", true)]
  [InlineData("Rowling", false)]
  [InlineData("123", true)] // ISBN
  [InlineData("ringen", true)] // Title
  [InlineData("xyz", false)]

  public void Book_Matches_ShouldFindByAuthor(string term, bool expected)
  {
    // Arrange
    var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

    // Act
    var result = book.Matches(term);

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void Book_ShouldImplement_ISearchable()
  {
    // Arrange
    var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

    // Act & Assert
    Assert.IsAssignableFrom<ISearchable>(book);
  }
}