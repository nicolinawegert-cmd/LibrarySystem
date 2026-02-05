using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using Xunit;

namespace LibrarySystem.Tests;

public class LibraryCompositionTests
{
  [Fact]
  public void Library_ShouldExposeBookCatalog()
  {
    // Arrange & Act
    var library = new Library();

    // Assert
    Assert.NotNull(library.Catalog);
  }

  [Fact]
  public void Library_ShouldExposeMemberRegistry()
  {
    // Arrange & Act
    var library = new Library();

    // Assert
    Assert.NotNull(library.Members);
  }

  [Fact]
  public void BookCatalog_ShouldAddBooks()
  {
    // Arrange
    var catalog = new BookCatalog();
    var book = new Book("123", "Testbok", "Testförfattare", 2024);

    // Act
    catalog.Add(book);

    // Assert
    Assert.Single(catalog.Books);
    Assert.Equal("123", catalog.Books[0].ISBN);
  }
}
