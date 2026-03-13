using Bunit;
using LibrarySystem.Core.Models;
using LibrarySystem.Web.Components.Shared;

namespace LibrarySystem.Tests;

public class BookCardTests : TestContext
{
  [Fact]
  public void BookCard_ShouldDisplayAvailableBookDetails()
  {
    // Arrange
    var book = new Book("123", "Testbok", "Författare", 2024);

    // Act
    var cut = RenderComponent<BookCard>(parameters =>
      parameters.Add(p => p.Book, book));

    // Assert
    Assert.Contains("Testbok", cut.Markup);
    Assert.Contains("Författare (2024)", cut.Markup);
    Assert.Contains("ISBN:", cut.Markup);
    Assert.Contains("123", cut.Markup);
    Assert.Contains("Tillgänglig", cut.Markup);
    cut.Find("span").ClassList.Contains("bg-success");
  }

  [Fact]
  public void BookCard_ShouldDisplayBorrowedStatus_WhenBookIsUnavailable()
  {
    // Arrange
    var book = new Book("456", "Utlånad bok", "Författare", 2020);
    book.MarkAsBorrowed();

    // Act
    var cut = RenderComponent<BookCard>(parameters =>
      parameters.Add(p => p.Book, book));

    // Assert
    Assert.Contains("Utlånad bok", cut.Markup);
    Assert.Contains("Utlånad", cut.Markup);
    cut.Find("span").ClassList.Contains("bg-danger");
  }
}
