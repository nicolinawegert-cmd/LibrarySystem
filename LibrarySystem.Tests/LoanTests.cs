using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests;

public class LoanTests
{
  [Fact]
  public void IsReturned_ShouldBeFalse_WhenReturnDateIsNull()
  {
    // Arrange 
    var book = new Book("123", "Testbok", "Testförfattare", 2020);
    var member = new Member("M001", "Testmedlem", "test@example.com");
    var loan = new Loan(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Act & Assert
    Assert.False(loan.IsReturned);
  }
}