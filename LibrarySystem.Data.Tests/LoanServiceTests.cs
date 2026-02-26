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
    var savedBook = await context.Books.FindAsync(b => b.Id == book.Id);
    Assert.False(savedBook.IsAvailable);

    // Assert: exacly one loan created linked to both
    var loans = await context.Loans.ToListAsync();
    Assert.Single(loans);
    Assert.Equal(book.Id, loans[0].BookId);
    Assert.Equal(member.Id, loans[0].MemberId);
    Assert.NotNull(loans[0].ReturnDate);
  }
}