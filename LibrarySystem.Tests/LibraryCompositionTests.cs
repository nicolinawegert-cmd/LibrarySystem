using LibrarySystem.Core.Services;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
  public class LibraryCompositionTests
  {
    [Fact]
    public void Library_ShouldExposeServices()
    {
      // Arrange & Act
      var library = new Library();

      // Assert
      Assert.NotNull(library.Catalog);
    }
  }
}

public partial class LibraryCompositionTests
{
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
