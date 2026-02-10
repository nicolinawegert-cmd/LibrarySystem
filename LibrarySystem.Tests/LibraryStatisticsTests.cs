using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using Xunit;

namespace LibrarySystem.Tests;

public class LibraryStatisticsTests
{
  [Fact]
  public void GetTotalBooks_ShouldReturnCorrectCount()
  {
    var library = new Library();
    library.Catalog.Add(new Book("1", "A", "X", 2000));
    library.Catalog.Add(new Book("2", "B", "Y", 2001));

    var total = library.GetTotalBooks();

    Assert.Equal(2, total);
  }

  [Fact]
  public void GetBorrowedBooksCount_ShouldReturnZero_WhenNoLoansExist()
  {
    // Arrange
    var library = new Library();

    // Act
    var borrowed = library.GetBorrowedBooksCount();

    // Assert
    Assert.Equal(0, borrowed);
  }

  [Fact]
  public void GetBorrowedBooksCount_ShouldReturnOne_WhenOneBookIsBorrowed()
  {
    // Arrange
    var library = new Library();
    var book = new Book("1", "A", "X", 2000);
    var member = new Member("M001", "Testmedlem", "test@example.com");
    
    library.Catalog.Add(book);
    library.Members.Add(member);
    
    library.Loans.Borrow(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Act
    var borrowed = library.GetBorrowedBooksCount();

    // Assert
    Assert.Equal(1, borrowed);
  }
}
