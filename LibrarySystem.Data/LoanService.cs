using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Services;

public class LoanService
{
  private readonly LibraryContext _context;

  public LoanService(LibraryContext context)
  {
    _context = context;
  }

  public async Task BorrowAsync(int bookId, int memberId)
  {
    var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
    if (book is null)
      throw new InvalidOperationException($"Book with id {bookId} not found.");

    var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == memberId);
    if (member is null)
      throw new InvalidOperationException($"Member with id {memberId} not found.");

    if (!book.IsAvailable)
      throw new InvalidOperationException($"Book with id {bookId} is not available.");

    var loanDate = DateTime.UtcNow;
    var dueDate = loanDate.AddDays(14);

    var loan = new Loan(book, member, loanDate, dueDate);

    book.MarkAsBorrowed();

    _context.Loans.Add(loan);
    await _context.SaveChangesAsync();
  }

  public async Task ReturnAsync(int loanId)
  {
    var loan = await _context.Loans
      .Include(l => l.Book)
      .FirstOrDefaultAsync(l => l.Id == loanId);

    if (loan is null)
      throw new InvalidOperationException($"Loan with id {loanId} not found.");

    if (loan.ReturnDate is not null)
      throw new InvalidOperationException($"Loan is already returned.");

    loan.MarkReturned(DateTime.UtcNow);
    loan.Book.MarkAsReturned();

    await _context.SaveChangesAsync();
  }
}