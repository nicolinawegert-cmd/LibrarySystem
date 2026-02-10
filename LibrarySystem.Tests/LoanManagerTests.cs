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
    Assert.Equal(1, manager.ActiveLoans.Count);
    Assert.Equal(book, manager.ActiveLoans[0].Book);
    Assert.Equal(member, manager.ActiveLoans[0].Member);
  }
}