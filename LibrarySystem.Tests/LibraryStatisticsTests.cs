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

  [Fact]
  public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
  {
    // Arrange
    var library = new Library();

    var anna = new Member("M001", "Anna", "anna@example.com");
    var bob = new Member("M002", "Bob", "bob@example.com");

    library.Members.Add(anna);
    library.Members.Add(bob);

    var book1 = new Book("1", "A", "X", 2000);
    var book2 = new Book("2", "B", "Y", 2001);
    var book3 = new Book("3", "C", "Z", 2002);

    library.Catalog.Add(book1);
    library.Catalog.Add(book2);
    library.Catalog.Add(book3);

    library.Loans.Borrow(book1, anna, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));
    library.Loans.Borrow(book2, anna, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));
    library.Loans.Borrow(book3, bob, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Act
    var mostActive = library.GetMostActiveBorrower();

    // Assert
    Assert.Equal(anna, mostActive);
}
