using LibrarySystem.Data.Services;
using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibrarySystem.Data.Tests;

public class LoanServiceTests
{
  [Fact]
  public async Task BorrowAsync_ShouldCreateLoan_AndMarkBookUnavailable()
  {
    // Arrange
    using var context = TestDb.CreateContext(nameof(BorrowAsync_ShouldCreateLoan_AndMarkBookUnavailable));

    var book = new Book("111", "Book", "Author", 2020);
    var member = new Member("M1", "John Doe", "john.doe@example.com");

    context.Books.Add(book);
    context.Members.Add(member);
    await context.SaveChangesAsync();

    var service = new LoanService(context);

    // Act
    await service.BorrowAsync(book.Id, member.Id);

    // Assert: book is now unavailable
    var savedBook = await context.Books.FirstAsync(b => b.Id == book.Id);
    Assert.False(savedBook.IsAvailable);

    // Assert: exacly one loan created linked to both
    var loans = await context.Loans.ToListAsync();
    Assert.Single(loans);

    var loan = loans[0];
    Assert.Equal(book.Id, loan.BookId);
    Assert.Equal(member.Id, loan.MemberId);
    Assert.Null(loan.ReturnDate);
    Assert.False(loan.IsReturned);
  }

  [Fact]
  public async Task BorrowAsync_ShouldThrow_WhenBookNotAvailable()
  {
    // Arrange
    using var context = TestDb.CreateContext(nameof(BorrowAsync_ShouldThrow_WhenBookNotAvailable));

    var book = new Book("111", "Book", "Author", 2020);
    var member1 = new Member("M1", "John Doe", "john.doe@example.com");

    context.Books.Add(book);
    context.Members.Add(member1);
    await context.SaveChangesAsync();

    book.MarkAsBorrowed();
    await context.SaveChangesAsync();

    var service = new LoanService(context);

    // Act & Assert
    await Assert.ThrowsAsync<InvalidOperationException>(() => service.BorrowAsync(book.Id, member1.Id));
  }

  [Fact]
  public async Task BorrowAsync_ShouldThrow_WhenBookNotFound()
  {
    // Arrange
    using var context = TestDb.CreateContext(nameof(BorrowAsync_ShouldThrow_WhenBookNotFound));

    var member = new Member("M1", "John Doe", "john.doe@example.com");
    context.Members.Add(member);
    await context.SaveChangesAsync();

    var service = new LoanService(context);

    // Act & Assert
    await Assert.ThrowsAsync<InvalidOperationException>(
        () => service.BorrowAsync(9999, member.Id));
  }

  [Fact]
  public async Task BorrowAsync_ShouldThrow_WhenMemberNotFound()
  {
    // Arrange
    using var context = TestDb.CreateContext(nameof(BorrowAsync_ShouldThrow_WhenMemberNotFound));

    var book = new Book("111", "Book", "Author", 2020);
    context.Books.Add(book);
    await context.SaveChangesAsync();

    var service = new LoanService(context);

    // Act & Assert
    await Assert.ThrowsAsync<InvalidOperationException>(
        () => service.BorrowAsync(book.Id, 9999));
  }

  [Fact]
  public async Task ReturnAsync_ShouldMarkLoanReturned_AndMarkBookAvailable()
  {
    // Arrange
    using var context = TestDb.CreateContext(nameof(ReturnAsync_ShouldMarkLoanReturned_AndMarkBookAvailable));

    var book = new Book("111", "Book", "Author", 2020);
    var member = new Member("M1", "John Doe", "john.doe@example.com");

    context.Books.Add(book);
    context.Members.Add(member);
    await context.SaveChangesAsync();

    var service = new LoanService(context);
    await service.BorrowAsync(book.Id, member.Id);

    var loan = await context.Loans.FirstAsync();

    // Act
    await service.ReturnAsync(loan.Id);

    // Assert
    var savedLoan = await context.Loans.FirstAsync(l => l.Id == loan.Id);
    var savedBook = await context.Books.FirstAsync(b => b.Id == book.Id);

    Assert.NotNull(savedLoan.ReturnDate);
    Assert.True(savedLoan.IsReturned);
    Assert.True(savedBook.IsAvailable);
  }

  [Fact]
  public async Task ReturnAsync_ShouldThrow_WhenLoanAlreadyReturned()
  {
    // Arrange
    using var context = TestDb.CreateContext(nameof(ReturnAsync_ShouldThrow_WhenLoanAlreadyReturned));

    var book = new Book("111", "Book", "Author", 2020);
    var member = new Member("M1", "John Doe", "john.doe@example.com");

    context.Books.Add(book);
    context.Members.Add(member);
    await context.SaveChangesAsync();

    var service = new LoanService(context);
    await service.BorrowAsync(book.Id, member.Id);

    var loan = await context.Loans.FirstAsync();
    await service.ReturnAsync(loan.Id);

    // Act & Assert
    await Assert.ThrowsAsync<InvalidOperationException>(
        () => service.ReturnAsync(loan.Id));
  }

  [Fact]
  public async Task ReturnAsync_ShouldThrow_WhenLoanNotFound()
  {
    // Arrange
    using var context = TestDb.CreateContext(nameof(ReturnAsync_ShouldThrow_WhenLoanNotFound));
    var service = new LoanService(context);

    // Act & Assert
    await Assert.ThrowsAsync<InvalidOperationException>(
        () => service.ReturnAsync(9999));
  }

}