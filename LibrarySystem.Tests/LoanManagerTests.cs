using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using Xunit;

namespace LibrarySystem.Tests;

public class LoanManagerTests
{
  [Fact]
  public void Borrow_ShouldCreateLoan_AndMakeBookUnavailable()
  {
    // Arrange
    var manager = new LoanManager();
    var book = new Book("1", "A", "X", 2000);
    var member = new Member("M001", "Testmedlem", "test@example.com");

    // Act
    manager.Borrow(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Assert
    Assert.False(book.IsAvailable);
    Assert.Single(manager.ActiveLoans);
    Assert.Equal(book, manager.ActiveLoans[0].Book);
    Assert.Equal(member, manager.ActiveLoans[0].Member);
  }

  [Fact]
  public void Loans_ShouldExposeAllLoans_ForStatistics()
  {
    // Arrange
    var manager = new LoanManager();
    var book = new Book("1", "A", "X", 2000);
    var member = new Member("M001", "Testmedlem", "test@example.com");

    // Act
    manager.Borrow(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Assert
    Assert.Single(manager.Loans);
  }

  [Fact]
  public void Borrow_ShouldThrowArgumentNullException_WhenBookIsNull()
  {
    // Arrange
    var manager = new LoanManager();
    var member = new Member("M001", "Testmedlem", "test@example.com");

    // Act & Assert
    Assert.Throws<ArgumentNullException>(() =>
      manager.Borrow(null!, member, DateTime.Today, DateTime.Today.AddDays(14)));
  }

  [Fact]
  public void Borrow_ShouldThrowInvalidOperationException_WhenBookIsNotAvailable()
  {
    // Arrange
    var manager = new LoanManager();
    var book = new Book("1", "A", "X", 2000);
    var member = new Member("M001", "Testmedlem", "test@example.com");

    // Act
    book.MarkAsBorrowed();

    // Assert
    Assert.Throws<InvalidOperationException>(() =>
      manager.Borrow(book, member, DateTime.Today, DateTime.Today.AddDays(14)));
  }

  [Fact]
  public void Borrow_ShouldThrowArgumentException_WhenDueDateIsNotAfterLoanDate()
  {
    // Arrange
    var manager = new LoanManager();
    var book = new Book("1", "A", "X", 2000);
    var member = new Member("M001", "Testmedlem", "test@example.com");

    // Act & Assert
    Assert.Throws<ArgumentException>(() =>
      manager.Borrow(book, member, DateTime.Today, DateTime.Today));
  }
}