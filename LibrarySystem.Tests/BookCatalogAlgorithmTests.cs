using LibrarySystem.Core.Services;
using LibrarySystem.Core.Models;
using Xunit;

namespace LibrarySystem.Tests;

public class BookCatalogAlgorithmTests
{
  [Fact]
  public void Search_ShouldReturnBooksThatMatchTerm()
  {
    // Arrange
    var catalog = new BookCatalog();
    catalog.Add(new Book("1", "Sagan om ringen", "J.R.R. Tolkien", 1954));
    catalog.Add(new Book("2", "Hobbiten", "J.R.R. Tolkien", 1937));
    catalog.Add(new Book("3", "Harry Potter", "J.K. Rowling", 1997));

    // Act
    var result = catalog.Search("Tolkien").ToList();

    // Assert
    Assert.Equal(2, result.Count);
    Assert.All(result, b => Assert.Contains("Tolkien", b.Author));
  }

  [Theory]
  [InlineData("")]
  [InlineData("  ")]
  public void Search_ShouldReturnEmpty_WhenTermIsEmpty(string term)
  {
    // Arrange
    var catalog = new BookCatalog();
    catalog.Add(new Book("1", "Sagan om ringen", "J.R.R. Tolkien", 1954));

    // Act
    var result = catalog.Search(term).ToList();

    // Assert
    Assert.Empty(result);
  }

  [Fact]
  public void SortByTitle_ShouldReturnAlphabeticalOrder()
  {
    // Arrange
    var catalog = new BookCatalog();
    catalog.Add(new Book("1", "Zoo", "A", 2000));
    catalog.Add(new Book("2", "alpha", "A", 2000));
    catalog.Add(new Book("3", "Beta", "A", 2000));

    // Act
    var sorted = catalog.SortByTitle().Select(b => b.Title).ToList();

    // Assert
    Assert.Equal(new[] { "alpha", "Beta", "Zoo" }, sorted);
  }
}