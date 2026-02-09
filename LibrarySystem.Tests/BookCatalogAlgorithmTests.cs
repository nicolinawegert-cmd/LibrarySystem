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

}