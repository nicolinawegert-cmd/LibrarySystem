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

  [Fact]
  public void MemberRegistry_ShouldAddAndFindMembersById()
  {
    // Arrange
    var registry = new MemberRegistry();
    var member = new Member("M001", "Testmedlem", "test@example.com");

    // Act
    registry.Add(member);
    var found = registry.FindById("M001");

    // Assert
    Assert.NotNull(found);
    Assert.Equal("M001", found!.MemberId);
  }

  [Fact]
  public void Library_ShouldExposeLoanManager()
  {
    // Arrange & Act
    var library = new Library();

    //Assert
    Assert.NotNull(library.Loans);
  }
}
