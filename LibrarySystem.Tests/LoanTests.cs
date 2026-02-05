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

  [Fact]
  public void IsReturned_ShouldBeTrue_WhenReturnDateIsSet()
  {
    // Arrange 
    var book = new Book("123", "Testbok", "Testförfattare", 2020);
    var member = new Member("M001", "Testmedlem", "test@example.com");
    var loan = new Loan(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Act 
    loan.MarkReturned(DateTime.UtcNow);

    // Assert
    Assert.True(loan.IsReturned);
  }

  [Fact]
  public void Constructor_ShouldSetBookAndMember()
  {
    // Arrange 
    var book = new Book("123", "Testbok", "Testförfattare", 2020);
    var member = new Member("M001", "Testmedlem", "test@example.com");

    // Act
    var loan = new Loan(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Assert
    Assert.Equal(book, loan.Book);
    Assert.Equal(member, loan.Member);
  }

  [Fact]
  public void Constructor_ShouldSetLoanDateAndDueDate()
  {
    // Arrange 
    var book = new Book("123", "Testbok", "Testförfattare", 2020);
    var member = new Member("M001", "Testmedlem", "test@example.com");
    var loanDate = DateTime.UtcNow;
    var dueDate = loanDate.AddDays(14);

    // Act
    var loan = new Loan(book, member, loanDate, dueDate);

    // Assert
    Assert.Equal(loanDate, loan.LoanDate);
    Assert.Equal(dueDate, loan.DueDate);
  }

  [Fact]
  public void IsOverdue_ShouldBeFalse_WhenDueDateIsInTheFuture()
  {
    // Arrange 
    var book = new Book("123", "Testbok", "Testförfattare", 2020);
    var member = new Member("M001", "Testmedlem", "test@example.com");
    var loan = new Loan(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

    // Act & Assert
    Assert.False(loan.IsOverdue);
  }

  [Fact]
  public void IsOverdue_ShouldBeTrue_WhenDueDateHasPassed_AndLoanIsNotReturned()
  {
    // Arrange 
    var book = new Book("123", "Testbok", "Testförfattare", 2020);
    var member = new Member("M001", "Testmedlem", "test@example.com");
    var loan = new Loan(book, member, DateTime.UtcNow.AddDays(-20), DateTime.UtcNow.AddDays(-1));

    // Act & Assert
    Assert.True(loan.IsOverdue);
  }

  [Fact]
  public void IsOverdue_ShouldBeFalse_WhenLoanIsReturned_EvenIfDueDateHasPassed()
  {
    // Arrange 
    var book = new Book("123", "Testbok", "Testförfattare", 2020);
    var member = new Member("M001", "Testmedlem", "test@example.com");
    var loan = new Loan(book, member, DateTime.UtcNow.AddDays(-20), DateTime.UtcNow.AddDays(-1));

    loan.MarkReturned(DateTime.UtcNow);

    // Act & Assert
    Assert.False(loan.IsOverdue);
  }
}